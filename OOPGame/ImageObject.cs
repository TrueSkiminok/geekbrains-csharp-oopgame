/*
Антонов Никита
Задание 

*/

using System;
using System.Drawing;

namespace OOPGame
{
    /// <summary>
    /// Объект, который отрисовывается готовой картинкой, передаваемой на вход конструктору
    /// </summary>
    class ImageObject : BaseObject
    {
        Image image;

        /// <summary>
        /// Конструктор объекта-картинки
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        /// <param name="image">Картинка, визуализирующая объект</param>
        public ImageObject(Point pos, Point dir, Size size, Image image) : base(pos, dir, size)
        {
            this.image = image;
        }

        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (обращаемся к статическому классу Game)
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        // Переопределяем движение объекта
        /// <summary>
        /// Перевод объекта в следующее состояние и положение
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
