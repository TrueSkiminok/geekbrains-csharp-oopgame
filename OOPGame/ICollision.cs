using System.Drawing;

namespace OOPGame
{
    /// <summary>
    /// Интерфейс для реализации столкновений
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
