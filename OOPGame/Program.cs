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
            Form form = new Form();
            form.Width = 800;
            form.Height = 600;
            Game.Init(form);
            form.Show();
            Game.Draw();
            
            Application.Run(form);

        }
    }
}
