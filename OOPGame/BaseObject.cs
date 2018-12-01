/*
Антонов Никита
Задание 

*/

using System;
using System.Drawing;


namespace OOPGame
{
    /// <summary>
    /// Класс-родитель для всех визуализируемых объектов игры
    /// </summary>
    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        /// <summary>
        /// Конструктор базового объекта
        /// </summary>
        /// <param name="pos">Точка позиционирования</param>
        /// <param name="dir">Направление движения</param>
        /// <param name="size">Размер фигуры</param>
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (абстрактный)
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Перевод объекта в следующее состояние и положение
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }

        // Так как переданный объект тоже должен будет реализовывать интерфейс ICollision, мы 
        // можем использовать его свойство Rect и метод IntersectsWith для обнаружения пересечения с
        // нашим объектом (а можно наоборот)

        /// <summary>
        /// Обнаружение столкновений с другими объектами
        /// </summary>
        /// <param name="o">Объекст, столкновение с которым проверяем</param>
        /// <returns></returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        /// <summary>
        /// Границы объекта
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);

    }

}
