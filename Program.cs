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
            string durationString, velocityString;
            

            Player MyPlayer = new Player(5, 90, 80, 55, 400);
            MyPlayer.run();

            //string pathTime = "C:/Users/Daniel/Documents/Visual Studio 2015/Projects/Lab2Console/Lab2Console/PlotDataTime.txt";
            string pathTime = "E:/Piotr/Programowanie/C#/AISDE2/Stare/Lab2Console/Lab2Console/PlotDataTime.txt";
            //string pathStreamVelocity = "C:/Users/Daniel/Documents/Visual Studio 2015/Projects/Lab2Console/Lab2Console/PlotStreamVelocity.txt";
            string pathStreamVelocity = "E:/Piotr/Programowanie/C#/AISDE2/Stare/Lab2Console/Lab2Console/PlotStreamVelocity.txt";
            //string pathTimefromstart = "C:/Users/Daniel/Documents/Visual Studio 2015/Projects/Lab2Console/Lab2Console/current.txt";
            string pathTimefromstart = "E:/Piotr/Programowanie/C#/AISDE2/Stare/Lab2Console/Lab2Console/current.txt";
            //SCIEZKA DANIELA = 
            string pathVpList = "E:/Piotr/Programowanie/C#/AISDE2/Stare/Lab2Console/Lab2Console/vpList.txt";

            System.IO.StreamWriter fileX = new System.IO.StreamWriter(pathTime);
            System.IO.StreamWriter fileY = new System.IO.StreamWriter(pathStreamVelocity);
            System.IO.StreamWriter fileZ = new System.IO.StreamWriter(pathTimefromstart);
            System.IO.StreamWriter fileH = new System.IO.StreamWriter(pathVpList);
            foreach (Event E in MyPlayer.Get_Buffer.EventsToPrint)
            {
                Console.WriteLine($"{E.Duration} , {E.StreamVelocity}");
                suma += E.Duration;
                durationString = E.Duration.ToString();
                /*      for (int i=0;i<durationString.Length;i++)
                      {
                          if(durationString[i]==',')
                          {
                              durationString.Remove(0, i);

                              durationString.Insert(i, ".");
                              durationString = 'M' + durationString.Substring(i+1);
                          }
                      }*/

                /*  fileX.WriteLine(E.Duration);
                  fileX.WriteLine(E.Duration);*/
                fileX.WriteLine($"{(int)E.Duration}.{(int)((E.Duration - (int)E.Duration) * 100)}");
                fileX.WriteLine($"{(int)E.Duration}.{(int)((E.Duration - (int)E.Duration) * 100)}");

                fileZ.WriteLine($"{(int)E.BeginingTime}.{(int)((E.BeginingTime - (int)E.BeginingTime) * 100)}");
                fileZ.WriteLine($"{(int)E.BeginingTime}.{(int)((E.BeginingTime - (int)E.BeginingTime) * 100)}");


                fileY.WriteLine(E.StreamVelocity);
                fileY.WriteLine(E.StreamVelocity);
            }

            foreach (int a in MyPlayer.Get_Buffer.vpList)
            {
                fileH.WriteLine($"{a}");
                fileH.WriteLine($"{a}");
            }

            fileX.Close();
            fileY.Close();
            fileZ.Close();
            fileH.Close();

            Console.WriteLine(suma);
            Console.ReadKey();

        }

    }
}
