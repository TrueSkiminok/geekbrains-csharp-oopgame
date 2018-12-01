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
    class GameObjectException : Exception
    {
        public GameObjectExceptionTypes ErrorType;
        public GameObjectException(string msg, GameObjectExceptionTypes errorType) : base(msg)
        {
            ErrorType = errorType;
        }
    }
}
