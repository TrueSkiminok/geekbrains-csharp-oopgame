/*
 * Антонов Никита
 * Домашнее задание по курсу "Продвинутый курс C#"
 * Урок № 2
*/

using System;
using System.Windows.Forms;

namespace OOPGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Form form = new Form();
            //form.Width = 800;
            //form.Height = 600;

            // Задаем размеры формы равные основному экрану.
            Form form = new Form
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };
            Game.Init(form);
            form.Show();
            Game.Load();
            Game.Draw();

            Application.Run(form);


        }
    }
}
