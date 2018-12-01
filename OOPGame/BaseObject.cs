/*
Антонов Никита
Задание 2. Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в наследниках.

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
            if (pos.X > 1000)
                throw new GameObjectException("Координата по оси X больше 1000", GameObjectExceptionTypes.WrongCoordinates);
            if (pos.Y > 1000)
                throw new GameObjectException("Координата по оси Y больше 1000", GameObjectExceptionTypes.WrongCoordinates);
            if (size.Width > 100)
                throw new GameObjectException("Слишком широкий объект, максимальная ширина 100",
                                              GameObjectExceptionTypes.WrongCoordinates);
            if (size.Height > 100)
                throw new GameObjectException("Слишком высокий объект, максимальная высота 100",
                                              GameObjectExceptionTypes.WrongCoordinates);
            if (dir.X > 50 || dir.Y > 50)
                throw new GameObjectException("Слишком высокая скорость", GameObjectExceptionTypes.OverSpeed);
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Добавление фигуры в буфер для дальнейшей отрисовки (абстрактный)
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Перевод объекта в следующее состояние и положение, движение (абстрактный)
        /// </summary>
        public abstract void Update();

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

        /// <summary>
        /// Установка конкретных координат объекта
        /// </summary>
        /// <param name="x">Координата по горизонтальной оси, если на вход подается отрицательное число, то остается прежней</param>
        /// <param name="y">Координата по вертикальной оси, если на вход подается отрицательное число, то остается прежней</param>
        public void SetPosition(int x, int y)
        {
            if (x >= 0) Pos.X = x;
            if (y >= 0) Pos.Y = y;
        }
    }
}
