using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Console
{
    class Program
    {
        static void Main(string[] args)
        {
            float suma=0;
            Player MyPlayer = new Player(5, 35, 300);
            MyPlayer.run();

            string pathTime = "E:/Piotr/Programowanie/C#/AISDE2/Lab2Console/Lab2Console/PlotDataTime.txt";
            string pathStreamVelocity = "E:/Piotr/Programowanie/C#/AISDE2/Lab2Console/Lab2Console/PlotStreamVelocity.txt";
            //string path = 
            System.IO.StreamWriter fileX = new System.IO.StreamWriter(pathTime);
            System.IO.StreamWriter fileY = new System.IO.StreamWriter(pathStreamVelocity);
            foreach (Event E in MyPlayer.Get_Buffer.EventsToPrint)
            {
                Console.WriteLine($"{E.Duration} , {E.StreamVelocity}");
                suma += E.Duration;
                fileX.WriteLine(E.Duration);
                fileY.WriteLine(E.StreamVelocity);
            }
            fileX.Close();
            fileY.Close();
            Console.WriteLine(suma);
            Console.ReadKey();

        }

    }
}
