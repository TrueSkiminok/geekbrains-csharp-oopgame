/*
Антонов Никита
Урок 3
Задание 3. Разработать аптечки, которые добавляют энергию.
*/

using System;
using System.Drawing;

namespace OOPGame
{
    class Medkit : BaseObject
    {
        /// <summary>
        /// Свойство "Сила"
        /// </summary>
        public int Power { get; }

        /// <summary>
        /// Конструктор объекта аптечка
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        public Medkit(Point pos, Point dir, Size size, int power) : base(pos, dir, size)
        {
            Power = power;
        }

        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (обращаемся к статическому классу Game).
        /// Аптечка - красный крест в желтом круге
        /// </summary>
        public override void Draw()
        {
            //Game.Buffer.Graphics.FillEllipse(Brushes.Aquamarine, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawEllipse(Pens.Yellow, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.FillRectangle(Brushes.Red, Pos.X + Size.Width / 2 - 2, Pos.Y + 2, 4, Size.Height - 4);
            Game.Buffer.Graphics.FillRectangle(Brushes.Red, Pos.X + 2, Pos.Y + Size.Height / 2 - 2, Size.Width - 4, 4);
        }

        /// <summary>
        /// Движение аптечки (аналогично астероиду)
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }


    }
}
