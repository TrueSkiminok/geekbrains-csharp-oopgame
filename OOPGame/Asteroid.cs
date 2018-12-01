/*
Антонов Никита
Методичка 2

*/

using System;
using System.Drawing;

namespace OOPGame
{
    // Создаем класс Asteroid, так как мы теперь не можем создавать объекты абстрактного класса BaseObject
    /// <summary>
    /// Объект - Астероид
    /// </summary>
    class Asteroid : BaseObject, ICloneable
    {
        /// <summary>
        /// Свойство "Сила"
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Конструктор объекта астероид
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        /// <summary>
        /// Релизация интерфейса ICloneable, создание независимой копии объекта Asteroid
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            // Создаем копию нашего астероида
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            // Не забываем скопировать новому астероиду Power нашего астероида
            asteroid.Power = Power;
            return asteroid;
        }

        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (обращаемся к статическому классу Game).
        /// Астероид
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }
}
