using System.Drawing;

namespace OOPGame
{
    /// <summary>
    /// Класс, описывающий корабль
    /// </summary>
    class Ship : BaseObject
    {
        /// <summary>
        /// Энергия (Жизни) корабля
        /// </summary>
        public int Energy { get; private set; } = 100;

        /// <summary>
        /// Уменьшение этергии корабля на опрелеленное значение
        /// </summary>
        /// <param name="n">количество энергии, которое будет отнято</param>
        public void EnergyLow(int n)
        {
            Energy -= n;
        }

        /// <summary>
        /// Конструктор корабля
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (обращаемся к статическому классу Game).
        /// Корабль
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Orange, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Движение корабля
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// Движение вверх (уменьшается координата по вертикали)
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        /// <summary>
        /// Движение вниз (увеличивается координата по вертикали)
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        /// <summary>
        /// Смерть корабля
        /// </summary>
        public void Die()
        {
        }
    }

}
