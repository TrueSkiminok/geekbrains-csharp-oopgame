/*
Антонов Никита
Задание 5.  *Создать собственное исключение GameObjectException,
            которое появляется при попытке создать объект с неправильными
            характеристиками (например, отрицательные размеры,
            слишком большая скорость или позиция).

*/

namespace OOPGame
{
    enum GameObjectExceptionTypes
    {
        NegativeSize,
        OverSpeed,
        WrongCoordinates
    }
}
