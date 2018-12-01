/*
Антонов Никита
Задание 

*/

using System;
using System.Drawing;

namespace OOPGame
{
    /// <summary>
    /// Объект - толстая звезда произвольного цвета
    /// </summary>
    class ThickStar : BaseObject
    {
        Pen coloredPen;

        /// <summary>
        /// Конструктор толстой звезды произвольного цвета
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        /// <param name="coloredPen">Цвет звезды</param>
        public ThickStar(Point pos, Point dir, Size size, Pen coloredPen) : base(pos, dir, size)
        {
            this.coloredPen = coloredPen; 
        }

        // Изменяем отрисовку толстых цветных звезд относительно базового объекта
        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (обращаемся к статическому классу Game).
        /// 4 линии крест-некрест
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(coloredPen, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(coloredPen, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(coloredPen, Pos.X + Size.Width / 2, Pos.Y + 1, Pos.X + Size.Width / 2, Pos.Y + Size.Height - 1);
            Game.Buffer.Graphics.DrawLine(coloredPen, Pos.X + 1, Pos.Y + Size.Height / 2, Pos.X + Size.Width - 1, Pos.Y + Size.Height / 2);
        }

        /// <summary>
        /// Движение звезды (по оси X)
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }


    }

}
