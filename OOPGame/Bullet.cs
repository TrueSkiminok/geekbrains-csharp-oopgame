﻿/*
Антонов Никита
Методичка 2

*/

using System;
using System.Drawing;

namespace OOPGame
{
    /// <summary>
    /// Объект - пуля
    /// </summary>
    class Bullet : BaseObject
    {
        /// <summary>
        /// Конструктор объекта пуля
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (обращаемся к статическому классу Game).
        /// Пуля
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Движение пули
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }

        /// <summary>
        /// Проверка выхода за край игрового поля
        /// </summary>
        /// <param name="width">Ширина игрового поля</param>
        /// <param name="height">Высота игрового поля</param>
        /// <returns></returns>
        public bool OutOfZone(int width, int height)
        {
            return (Pos.X > width || Pos.Y > height);
        }
    }
}
