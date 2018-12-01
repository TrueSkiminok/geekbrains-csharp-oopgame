/*
Антонов Никита
Задание 

*/

using System;
using System.Drawing;

namespace OOPGame
{
    /// <summary>
    /// Объект - круглая звезда произвольного цвета
    /// </summary>
    class StationaryCircle : BaseObject
    {
        Brush coloredBrush;

        /// <summary>
        /// Конструктор круглойзвезды произвольного цвета
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        /// <param name="coloredBrush">Цвет звезды</param>
        public StationaryCircle(Point pos, Point dir, Size size, Brush coloredBrush) : base(pos, dir, size)
        {
            this.coloredBrush = coloredBrush; 
        }

        // Изменяем отрисовку круглых цветных звезд относительно базового объекта
        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (обращаемся к статическому классу Game).
        /// Заполненный круг
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(coloredBrush, Pos.X, Pos.Y, Size.Width, Size.Height);
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
