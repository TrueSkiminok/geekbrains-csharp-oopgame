/*
Антонов Никита
Задание 

*/

using System;
using System.Drawing;

namespace OOPGame
{
    class Star : BaseObject
    {
        /// <summary>
        /// Конструктор обычной звезды
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        // Изменяем отрисовку звезд относительно базового объекта
        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (обращаемся к статическому классу Game).
        /// Две линии крест-накрест
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        // Переопределяем движение звезд
        /// <summary>
        /// Движение звезды (по оси X)
        /// </summary>
        public override void Update()
        {
            // Изменил знак относительно методички с - на + , потому что минус здесь
            // и минус в Load приводил к движению вправо и звезда терялась за краем экрана,
            // т.к. ниже проверяется левая граница а не правая
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }


    }

}
