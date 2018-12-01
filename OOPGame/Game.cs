/*
Антонов Никита
Задание 

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

        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
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
            //Мы решили создать 30 объектов на экране
            _objs = new BaseObject[30];

            // Кометы - одна пятая всех объектов
            for (int i = 0; i < _objs.Length / 5; i++)
                _objs[i] = new ImageObject(new Point(600, i * 60), new Point(-i - 1, i + 1),
                                           new Size(30, 30), Image.FromFile(@"img\comet.png"));

            // Обычные звезды - две пятых всех объектов
            for (int i = _objs.Length / 5; i < _objs.Length / 5 * 3; i++)
                _objs[i] = new Star(new Point(400, i * 55 - 540), new Point(-i, 0), new Size(5, 5));

            // Насыщенные голубые звезды  - две пятых всех объектов
            for (int i = _objs.Length / 5 * 3; i < _objs.Length; i++)
                _objs[i] = new ThickStar(new Point(200, i * 50 - 970), new Point(-i, 0),
                                         new Size(7, 7), Pens.Aquamarine);
        }

        /// <summary>
        /// Последовательная отрисовка всех объектов созданных в игре
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// Перевод всех объектов игры к следующему положению и состоянию, в соответсвии с их внутренней логикой
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
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
    }
}
