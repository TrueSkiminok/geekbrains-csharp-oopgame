/*
Антонов Никита
Задание 1. Добавить в программу коллекцию астероидов. Как только она заканчивается (все астероиды сбиты),
формируется новая коллекция, в которой на 1 астероид больше.

*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace OOPGame
{
    // Создаем шаблон приложения, где подключаем модули
    /// <summary>
    /// Класс, реализующий игру
    /// </summary>
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
        private static List<Bullet> _bullets = new List<Bullet>();
        private static Asteroid[] _asteroids;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
        private static Medkit _medkit; //добавляем аптечку, не более 1 в каждый момент времени
        private static Random rnd = new Random(); // вынес рандомайзер на уровень класса, чтобы он был доступен во всех методах
        private static Action<string> log; // Обобщенный делегат типа "действие" для вызова логирования
        private static int score; //Текущий счет

        // Свойства
        // Ширина и высота игрового поля
        /// <summary>
        /// Ширинаигрового поля
        /// </summary>
        public static int Width { get; set; }
        /// <summary>
        /// Высота игрового поля
        /// </summary>
        public static int Height { get; set; }

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        static Game()
        {
            log += WriteLogEntryToConsole; // подключаем вывод лога в консоль
            log += WriteLogEntryToFile; // подключаем вывод лога в файл
        }
        /// <summary>
        /// Инициализация вывода графики в форму
        /// </summary>
        /// <param name="form">Форма, в которой будем рисовать</param>
        public static void Init(Form form)
        {
            if (form.Width > 1000 || form.Width < 0 || form.Height > 1000 || form.Height < 0)
            {
                throw new ArgumentOutOfRangeException("form.Width или form.Height",
                "Высота или ширина больше 1000 или принимает отрицательное значение");
            }



            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            // Наполняем сцену объектами
            Load();

            //Подключаю обработку событий нажатия на клавишу
            form.KeyDown += Form_KeyDown;

            // Вызов обработки кадра по таймеру
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Создание набора элементов, которые мы хотим отрисовать
        /// </summary>
        public static void Load()
        {

            _objs = new BaseObject[30];
            // Автоматическая генерация пули больше не нужна, корабль умеет стрелять.
            //_bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i] = NewAsteroid();
            }
            _medkit = NewMedkit();

            ////Мы решили создать 30 объектов на экране
            //_objs = new BaseObject[30];
            //Random rnd = new Random();

            //// Кометы - одна пятая всех объектов
            //for (int i = 0; i < _objs.Length / 5; i++)
            //    _objs[i] = new ImageObject(new Point(600, i * 60), new Point(-i - 1, i + 1),
            //                               new Size(30, 30), Image.FromFile(@"img\comet.png"));

            //// Обычные звезды - одна пятая всех объектов
            //for (int i = _objs.Length / 5; i < _objs.Length / 5 * 2; i++)
            //    _objs[i] = new Star(new Point(400, i * 90 - 550), new Point(-i, 0), new Size(5, 5));

            //// Круглые звезды - одна пятая всех объектов
            //for (int i = _objs.Length / 5 * 2; i < _objs.Length / 5 * 3; i++)
            //    _objs[i] = new StationaryCircle(new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
            //                                    new Point(-i, 0), new Size(13, 13), Brushes.Yellow);

            //// Насыщенные голубые звезды  - две пятых всех объектов
            //for (int i = _objs.Length / 5 * 3; i < _objs.Length; i++)
            //    _objs[i] = new ThickStar(new Point(200, i * 50 - 970), new Point(-i, 0),
            //                             new Size(9, 9), Pens.Aquamarine);
        }

        /// <summary>
        /// Последовательная отрисовка всех объектов созданных в игре
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in _objs)
                obj.Draw();

            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }

            foreach (Bullet b in _bullets)
            {
                b.Draw();
            }

            _medkit?.Draw();
            _ship?.Draw();

            if (_ship != null)
                Buffer.Graphics.DrawString("Energy: " + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            // Отрисовка счета
            Buffer.Graphics.DrawString("Score: " + score, SystemFonts.DefaultFont, Brushes.Aquamarine, 0, 15);

            Buffer.Render();

        }

        /// <summary>
        /// Перевод всех объектов игры к следующему положению и состоянию, в соответсвии с их внутренней логикой
        /// Проверка на попадание объектов в друг-друга
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].OutOfZone(Width, Height))
                {
                    _bullets.RemoveAt(i);
                    i--;
                }
            }
            foreach (Bullet b in _bullets) b.Update();
            _medkit?.Update();

            if (_ship.Collision(_medkit))
            {
                log?.Invoke($"Найдена аптечка. Энергия корабля будет восстановлена на {_medkit.Power} единиц");
                _ship.Heal(_medkit.Power);
                _medkit = NewMedkit();
            }

            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                for (int j = 0; j < _bullets.Count; j++)
                    if (_asteroids[i] != null && _bullets[j].Collision(_asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        log?.Invoke($"Пуля попала в астероид, астероид уничтожен, вы заработали {_asteroids[i].Power} очков");
                        score += _asteroids[i].Power;
                        _asteroids[i] = null;
                        _bullets.RemoveAt(j);
                        j--;
                    }

                if (_asteroids[i] == null || !_ship.Collision(_asteroids[i])) continue;

                int damage = rnd.Next(1, 10);
                _ship.EnergyLow(damage);
                System.Media.SystemSounds.Asterisk.Play();
                log?.Invoke($"Корабль столкнулся с Астероидом и получил {damage} урона.");
                if (_ship.Energy <= 0)
                {
                    _ship?.Die();
                    log?.Invoke($"Корабль уничтожен");
                    // Вероятно тут нужно будет добавить конец игры
                }
            }
        }

        /// <summary>
        /// Обработка очередного кадра игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Обработчик нажатий на клавиши
        /// </summary>
        /// <param name="sender">источник события</param>
        /// <param name="e">параметры события ( нажатой клавиша)</param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(10, 0), new Size(4, 1)));
                log?.Invoke($"Произведен выстрел по линии Y = {_ship.Rect.Y + 4}");
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        /// <summary>
        /// Вывод сообщения в консоль
        /// </summary>
        /// <param name="_message">тело сообщения</param>
        private static void WriteLogEntryToConsole(string _message)
        {
            string message = $"{DateTime.Now.ToLongTimeString()} >>> {_message}";
            Console.WriteLine(message);
        }

        /// <summary>
        /// /// Вывод сообщения в файл
        /// </summary>
        /// <param name="_message">тело сообщения</param>
        private static void WriteLogEntryToFile(string _message)
        {
            string message = $"{DateTime.Now.ToLongTimeString()} >>> {_message}";
            using (var r = new StreamWriter($"Asteroids_log.txt", true))
            {
                r.WriteLine(message);
            }
        }

        /// <summary>
        /// Создание новой аптечки на рандомной линии
        /// </summary>
        /// <returns>Объект аптечка типа Medkit</returns>
        private static Medkit NewMedkit()
        {
            return new Medkit(new Point(800, rnd.Next(0, Game.Height)), new Point(-5, 0), new Size(20, 20), 10);
        }

        /// <summary>
        /// Создание нового Астероида на рандомной линии
        /// </summary>
        /// <returns>Объект астероид типа Asteroid</returns>
        private static Asteroid NewAsteroid()
        {
            int r = rnd.Next(5, 50);
            return new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
        }
    }
}
