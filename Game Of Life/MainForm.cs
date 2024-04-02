using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Game_Of_Life
{
    public partial class MainForm : Form
    {
        private List<string> History = new List<string>();
        bool red = false;
        int born = 0; // Родилось клеток
        int dead = 0; // Умерло клеток
        int alive = 0; // Живых клеток
        int MinNeighbors = 2; // минимальное количество соседей для выживания
        int MaxNeighbors = 3; // максимальное количество соседей для выживания
        int NeighborsBornNew = 3; // количество соседей, при котором в мертвой клетке зарождается жизнь
        bool Changed = false; //Признак того, что текущее состояние будет изменено на следующем шаге
        private static bool paused = true; // Флаг паузы задачи пошаговой трансформации
        private static bool OneStep = false; // Флаг признака одного шага трансформации
        private static bool seedcomplete = false; // Флаг признака начального заполнения при старте программы
        private MainForm Instance; // Аксессор для экземпляра формы
        private static int FieldHeight = 0; // Высота поля
        private static int FieldWidth = 0; // Ширина поля
        private static int movenumber = 0; // Номер хода
        private int InitialDensityPercent = 0; // Плотность живых клеток при начальном заполнении
        private bool[,] IsAlive = new bool[FieldHeight, FieldWidth]; // Двумерный массив текущего состояния
        private bool[,] Team = new bool[FieldHeight, FieldWidth];
        private bool[,] NextState = new bool[FieldHeight, FieldWidth]; // Двумерный массив состояния на следующем ходу
        DirectBitmap CurrentBitMap; // ОБъект картинки, изображающей поле
        private static CancellationTokenSource CTS; // Источник сигнала об окончании работы для асинхронных задач обработки трансформации игрового поля
        Point lastPoint = Point.Empty; // Объект для запоминания последней отрисованной точки при рисовании мышкой
        bool isMouseDown = new Boolean(); // Переменная которая хранит признак нажатой кнопки мыши (для рисования)
        bool isBusy = false; // Переменная хранит признак выполняющихся вычислений
        public MainForm() // Запуск приложения
        {
            InitializeComponent();
            Instance = this;
            FieldHeight = pic.Height;
            FieldWidth = pic.Width;
            CurrentBitMap = new DirectBitmap(FieldWidth, FieldHeight);
            Task.Run(() => // запускаем асинхронную задачу пошаговой трансформации поля в фоновом потоке
            {
                while (true)
                {
                    while (paused) Task.Delay(100); // если флаг паузы выставлен в true, ждем и проверяем через каждые 100мс
                    DrawBitmap();
                    Transform();
                    SaveHistory();
                    movenumber++;
                    if (OneStep)// проверяем признак пошагового выполнения
                    {
                        OneStep = false; // снимаем флаг пошагового выполнения
                        Pause();
                    }
                }
            });
        }
        private void ShowBitMap() // Выводим картинку
        {
            Size newSize = new Size((int)(CurrentBitMap.Width), (int)(CurrentBitMap.Height));
            Bitmap tmpbitmap = new Bitmap(CurrentBitMap.Bitmap, newSize);
            pic.Width = tmpbitmap.Width;
            pic.Height = tmpbitmap.Height;
            pic.Image = tmpbitmap;
            pic.Refresh();
            panel1.VerticalScroll.Visible = (pic.Height > panel1.Height);
            panel1.HorizontalScroll.Visible = (pic.Width > panel1.Width);
            Refresh();
        }
        private void InitialSeed() // Заполнение поля (задание начальных условий)
        {
            int alive = 0;
            if (IsAlive.Length == 0) IsAlive = new bool[FieldHeight, FieldWidth];
            if (InitialDensityPercent > 0)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                for (int y = 0; y < FieldHeight; y++)
                {
                    for (int x = 0; x < FieldWidth; x++)
                    {
                        IsAlive[y, x] = (rnd.Next(100) <= InitialDensityPercent);
                        if (IsAlive[y, x])
                        {
                            if (rnd.Next(100) <= 50) Team[y, x] = true; else Team[y, x] = false;
                            alive++;
                        }
                    }
                }
            } else { // если процент случайного заполнения = 0, рисуем пустой квадрат
                IsAlive = new bool[FieldHeight, FieldWidth];
                Team = new bool[FieldHeight, FieldWidth];
            }
            movenumber = 0;
            ShowCurrentStepInfo();
            DrawBitmap();
            if (InitialDensityPercent == 0)
            {
                RunButton.Enabled = false;
                RunOneStepButton.Enabled = false;
                ResetButton.Enabled = false;
                seedcomplete = false;
            }
            else
            {
                if (!seedcomplete)
                {
                    RunButton.Enabled = true;
                    RunOneStepButton.Enabled = true;
                    ResetButton.Enabled = true;
                    seedcomplete = true;
                }
            }
        }
        private void ShowCurrentStepInfo() // Выводим показания счетчиков и номер шага на форму
        {
            Instance.Invoke((MethodInvoker)delegate // делегируем отрисовку GUI основному потоку, в котором обрабатывается
            {
                movelabel.Text = $"Шаг {movenumber}";
                bornlabel.Text = $"Родилось: {born}";
                deadlabel.Text = $"Умерло: {dead}";
                alivelabel.Text = $"Осталось в живых: {alive}";
                Refresh();
            });
        }
        private void Transform() // Трансформация текущего состояния по правилам
        {
            born = 0;
            dead = 0;
            alive = 0;
            Changed = false;
            NextState = new bool[FieldHeight, FieldWidth];
            // обсчитывать трансформацию будем в два параллельных потока
            // создаем массив асинхронных задач, делим поле пополам
            int divider = (int)(FieldHeight / 2);
            CTS = new CancellationTokenSource();// сброс сигнала об остановке асинхронных задач
            var task1 = Task.Run(() => ProcessTransform(0, divider), CTS.Token);
            var task2 = Task.Run(() => ProcessTransform(divider, FieldHeight), CTS.Token);
            Task.WhenAll(task1, task2).Wait();// ожидаемся конца выполнения всех потоков
            Instance.Invoke((MethodInvoker)delegate// делегируем отрисовку GUI основному потоку, в котором обрабатывается весь интерфейс
            {
                ShowCurrentStepInfo();
            });
            if (Changed) Array.Copy(NextState, IsAlive, NextState.Length); // Есть изменения, копируем состояние следующего шага в текущий массив
        }
        private async Task<bool> ProcessTransform(int from, int to) // Задача для обсчета трансформации игрового поля для следующего шага в фоновом потоке
        {
            await Task.Run(() =>
            {
                //Random sdrnd = new Random(Guid.NewGuid().GetHashCode());// Инициализируем генератор случайных чисел
                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < FieldWidth; x++)
                    {
                        int ncount = GetNeighborsMask(y, x);// получаем количество соседей из байта состояния
                        bool state = IsAlive[y, x];// текущее состояние клетки: true = жива
                        bool team = GetNeighborsTeam(y, x);
                        if (state == true) // клетка жива на текущем шаге
                        {
                            alive++;
                            if (ncount > MaxNeighbors || ncount < MinNeighbors)// перенаселённость одиночество старость
                            {
                                dead++; // клетка умирает
                                alive--;
                                NextState[y, x] = false;
                            }
                            else NextState[y, x] = true; // продолжает жить
                        }
                        if(GetNeighborsTeam(y, x) != Team[y, x] || state == false) // клетка мертва на текущем шаге или клетки рядом другого цвета
                        {
                            if (!state && ncount == NeighborsBornNew) // в пустой (мёртвой) клетке, рядом с которой определенное количество живых клеток, зарождается жизнь
                            {
                                NextState[y, x] = true;
                                Team[y, x] = GetNeighborsTeam(y, x);
                                born++;
                            }
                            else // клетка остается мертвой
                            {
                                NextState[y, x] = false;
                            }
                        }
                        // Вычисляем есть ли разница между текущим и следующим шагом с помощью логического ИЛИ
                        // (достаточно одного изменения в массиве, чтобы итоговое значение было Changed == true)
                        Changed |= IsAlive[y, x] != NextState[y, x];
                    }
                }
            });
            return true;
        }
        private int GetNeighborsMask(int y, int x) // Находит соседей указанной клетки и возвращает их количество
        {
            int res = 0; // счётчик
            var Yinc = (y + 1) % FieldHeight;// инкрементированное значение координаты Y
            var Ydec = (FieldHeight + y - 1) % FieldHeight;// декрементированное значение координаты Y
            var Xinc = (x + 1) % FieldWidth;// инкрементированное значение координаты X
            var Xdec = (FieldWidth + x - 1) % FieldWidth; // декрементированное значение координаты X
            if (IsAlive[Yinc, Xdec]) res++; // проворяем клетку выше и левее
            if (IsAlive[Yinc, (x)]) res++; // проворяем клетку выше
            if (IsAlive[Yinc, Xinc]) res++; // проворяем клетку выше и правее
            if (IsAlive[(y), Xdec]) res++; // проворяем клетку левее
            if (IsAlive[(y), Xinc]) res++; // проворяем клетку правее
            if (IsAlive[Ydec, Xdec]) res++; // проворяем клетку ниже и левее
            if (IsAlive[Ydec, (x)]) res++; // проворяем клетку ниже
            if (IsAlive[Ydec, Xinc]) res++; // проворяем клетку ниже и правее
            return res;
        }
        private bool GetNeighborsTeam(int y, int x) // Находит соседей указанной клетки и возвращает их количество
        {
            int sin = 0; // кол-во синих
            int krasn = 0; // кол-во красных
            bool res; // результат
            var Yinc = (y + 1) % FieldHeight;// инкрементированное значение координаты Y
            var Ydec = (FieldHeight + y - 1) % FieldHeight;// декрементированное значение координаты Y
            var Xinc = (x + 1) % FieldWidth;// инкрементированное значение координаты X
            var Xdec = (FieldWidth + x - 1) % FieldWidth; // декрементированное значение координаты X
            if (IsAlive[Yinc, Xdec]) { if (Team[Yinc, Xdec]) krasn++; else sin++; } // проворяем клетку выше и левее
            if (IsAlive[Yinc, (x)]) { if (Team[Yinc, (x)]) krasn++; else sin++; } // проворяем клетку выше
            if (IsAlive[Yinc, Xinc]) { if (Team[Yinc, Xinc]) krasn++; else sin++; } // проворяем клетку выше и правее
            if (IsAlive[(y), Xdec]) { if (Team[(y), Xdec]) krasn++; else sin++; } // проворяем клетку левее
            if (IsAlive[(y), Xinc]) { if (Team[(y), Xinc]) krasn++; else sin++; } // проворяем клетку правее
            if (IsAlive[Ydec, Xdec]) { if (Team[Ydec, Xdec]) krasn++; else sin++; } // проворяем клетку ниже и левее
            if (IsAlive[Ydec, (x)]) { if (Team[Ydec, (x)]) krasn++; else sin++; } // проворяем клетку ниже
            if (IsAlive[Ydec, Xinc]) { if (Team[Ydec, Xinc]) krasn++; else sin++; } // проворяем клетку ниже и правее
            if (krasn > sin) res = true; // если красных больше - возвр. true
            else res = false; // если синих больше - возвр. false
            return res;
        }
        private void DrawBitmap() // Формируем картинку из объекта текущего состояния
        {
            Instance.Invoke((MethodInvoker)delegate //делаем инвок для синхронизации доступа в поток GUI
            {
                for (int y = 0; y < CurrentBitMap.Height; y++)
                {
                    for (int x = 0; x < CurrentBitMap.Width; x++) // устанавливаем цвет клетки
                    {

                        if (IsAlive[y, x] == true) // если живая
                        {
                            if(Team[y, x] == false) CurrentBitMap.SetPixel(x, y, Color.FromArgb(255, 0, 0, 255));
                            else CurrentBitMap.SetPixel(x, y, Color.FromArgb(255, 255, 0, 0));

                        }               
                        else
                        {
                            CurrentBitMap.SetPixel(x, y, Color.FromArgb(255, 255, 255, 255));
                        }
                    }
                }
                ShowBitMap();
            });
        }
        private void button1_Click(object sender, EventArgs e) { Play(); } // Нажатие на кнопку Пуск
        private void button2_Click(object sender, EventArgs e) { Pause(); } // нажатие на кнопку пауза
        private void Play() // Снимаем с паузы \ запускаем
        {
            Instance.Invoke((MethodInvoker)delegate// делегируем отрисовку GUI основному потоку, в котором обрабатывается весь интерфейс
            {           
                PauseButton.Enabled = !OneStep; //запускаем симуляцию, делаем недоступными кнопки "пуск" и "пошаговое выполнения" и ползунок
                RunButton.Enabled = false; 
                RunOneStepButton.Enabled = false;
                FillingPercentileTracker.Enabled = false;
                ResetButton.Enabled = false;
                paused = false;//снимаем флаг паузы
            });
        }
        private void Pause() // Ставим на паузу
        {
            Instance.Invoke((MethodInvoker)delegate// делегируем отрисовку GUI основному потоку, в котором обрабатывается весь интерфейс
            {
                PauseButton.Enabled = false; //останавливаем симуляцию, делаем доступными кнопки "пуск" и "пошаговое выполнения" и ползунок, блокируем кнопку "пауза"
                RunButton.Enabled = true;
                RunOneStepButton.Enabled = true;
                FillingPercentileTracker.Enabled = true;
                ResetButton.Enabled = true;
                paused = true; //устанавливаем флаг паузы
            });
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e) // Движение ползунка приводит к заполнению поля с указанным процентом живых клеток
        {
            //ClearGraph();
            InitialDensityPercent = FillingPercentileTracker.Value;
            seedpercent.Text = InitialDensityPercent.ToString();
            IsAlive = new bool[FieldHeight, FieldWidth];
            NextState = new bool[FieldHeight, FieldWidth];
            InitialSeed();
        }
        private void button3_Click_1(object sender, EventArgs e) // Нажатие кнопки пошагового выполнения
        {
            OneStep = true;
            Play();
        }
        private void nmin_ValueChanged(object sender, EventArgs e) // Изменение минимального для выживания количества соседей
        {
            MinNeighbors = (int)nmin.Value;
        }
        private void nmax_ValueChanged(object sender, EventArgs e) // Изменение максимеального для выживания количества соседей
        {
            MaxNeighbors = (int)nmax.Value;
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e) // Изменение количества клеток для рождения новой
        {
            NeighborsBornNew = (int)newborn.Value;
        }
        private void pic_MouseMove(object sender, MouseEventArgs e) // Двигаем мышкой с зажатой кнопкой, рисуем линию на картинке
        {
            if (!paused || isBusy) return; // рисовать будем только если нет увеличения и на паузе
            if (isMouseDown == true)
            {
                if (lastPoint != null)
                {
                    using (Graphics g = Graphics.FromImage(pic.Image))
                    {
                        if(red == false) g.DrawLine(new Pen(Color.FromArgb(255, 0, 0, 255), 1), lastPoint, e.Location); // 0 255 0
                        else g.DrawLine(new Pen(Color.FromArgb(255, 255, 0, 0), 1), lastPoint, e.Location); // 0 255 0
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    pic.Invalidate();
                    lastPoint = e.Location;
                }
            }
        }
        private void pic_MouseDown(object sender, MouseEventArgs e) // Нажата кнопка мыши над картинкой, начинаем рисовать
        {
            if (!paused || isBusy) return;// рисовать будем только если нет увеличения и на паузе
            lastPoint = e.Location;
            isMouseDown = true;
        }
        private void pic_MouseUp(object sender, MouseEventArgs e) // Кнопка мыши отпущена, заканчиваем отрисовку и копируем картинку в объект текущего состояния
        {
            if (!paused || isBusy) return; // рисовать будем только если нет увеличения и на паузе
            isBusy = true;
            Task.Run(() =>
            {
                Instance.Invoke((MethodInvoker)delegate
                {
                    var bm = (Bitmap)pic.Image;
                    isMouseDown = false;
                    lastPoint = Point.Empty;
                    for (int y = 0; y < pic.Height; y++)
                    {
                        for (int x = 0; x < pic.Width; x++)
                        {
                            IsAlive[y, x] = (bm.GetPixel(x, y) != Color.FromArgb(255,255,255,255)); // чёрный-белый
                            if (bm.GetPixel(x, y) == Color.FromArgb(255, 0, 0, 255)) Team[y, x] = false;
                            else Team[y, x] = true;
                        }
                    }
                    if (!RunButton.Enabled)
                    {
                        RunButton.Enabled = true;
                        RunOneStepButton.Enabled = true;
                        ResetButton.Enabled = true;
                    }
                });
                isBusy = false;
            });
        }
        private void MainForm_Shown(object sender, EventArgs e) // Окно программы показывается первый раз
        {
            InitialSeed();
        }
        private void ResetButton_Click(object sender, EventArgs e) // Нажатие на кнопку Сброс
        {
            InitialSeed();
            History.Clear();
        }

        private void SaveHistory() // сохранение истории и остановка игры
        {
            if (History.Count == 10000) History.RemoveAt(0); // ограничим отслеживание истории списком состояний длиной 10000 записей, чтобы не терять производительность
            byte[] tmparr;
            using (var memoryStream = new MemoryStream())
            {
                CurrentBitMap.Bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                tmparr = memoryStream.ToArray();
            }
            SHA256 shaM = new SHA256Managed(); // вычисляем хэш-функцию от текущего состояния игрового поля, длиной 256 бит = 32 байта
            var HashedbytesString = Convert.ToBase64String(shaM.ComputeHash(tmparr));
            if (!History.Any(rec => rec == HashedbytesString))
            {
                History.Add(HashedbytesString);// строки в истории еще нет => текущее состояние уникально, продолжаем
            }
            else // строк анайдена в списке => текущее состояние повторяет предыдущее на одном из шагов => гейм овер
            {
                var stepsbefore = History.Count - History.IndexOf(HashedbytesString);// сколько шагов назад было совпадение
                History.Clear();// чистим историю
                Instance.Invoke((MethodInvoker)delegate// делегируем отрисовку GUI основному потоку, в котором обрабатывается весь интерфейс
                {
                    CTS.Cancel();// подаем сигнал СТОП асинхронным задачам
                    button2_Click(null, null);// нажимаем кнопку "Пауза"
                    MessageBox.Show(Instance, string.Format("Конфигурация поколения {0} повторяет конфигурацию поколений назад: {1}", movenumber, stepsbefore), "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
        }
        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (red == false) { red = true; ColorButton.Text = "Красный"; }
            else { red = false; ColorButton.Text = "Синий"; }
        }
    }
}
