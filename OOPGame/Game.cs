/*
Антонов Никита
Задание 3.  Сделать так, чтобы при столкновениях пули с астероидом
            они регенерировались в разных концах экрана.
Задание 4.  Сделать проверку на задание размера экрана в классе Game.
            Если высота или ширина (Width, Height) больше 1000 или принимает
            отрицательное значение, выбросить исключение ArgumentOutOfRangeException().

*/

using System;
using System.Drawing;
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
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));

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
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }


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

            _bullet?.Draw();
            _ship?.Draw();

            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Render();

        }

        /// <summary>
        /// Перевод всех объектов игры к следующему положению и состоянию, в соответсвии с их внутренней логикой
        /// Проверка на попадание объектов в друг-друга
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();

            _bullet?.Update();

            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;

                _asteroids[i].Update();

                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullet = null;
                    continue;
                }

                if (!_ship.Collision(_asteroids[i])) continue;

                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();

                if (_ship.Energy <= 0) _ship?.Die();
            }

            // Обработка пули, вышедшей за край экрана
            if (_bullet != null && _bullet.OutOfZone(Width, Height)) _bullet = null;
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
            if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

    }
}
