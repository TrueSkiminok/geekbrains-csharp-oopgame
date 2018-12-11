/*
Антонов Никита
Задание 5.  *Создать собственное исключение GameObjectException,
            которое появляется при попытке создать объект с неправильными
            характеристиками (например, отрицательные размеры,
            слишком большая скорость или позиция).

*/

using System;

namespace OOPGame
{
    /// <summary>
    /// Собственное исключение для игры
    /// </summary>
    class GameObjectException : Exception
    {
        /// <summary>
        /// Свойство - типа перечисление видов исключений
        /// </summary>
        public GameObjectExceptionTypes ErrorType;

        /// <summary>
        /// Конструктор собственного исключения для игры
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="errorType">Тип исключения из перечисления GameObjectExceptionTypes</param>
        public GameObjectException(string msg, GameObjectExceptionTypes errorType) : base(msg)
        {
            ErrorType = errorType;
        }
    }
}
