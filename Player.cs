using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Console
{
    class Player
    {

        Buffer Buff1 = new Buffer();
        Stream Stream1 = new Stream();
        int vp; //Playing velocity [b/s]
        int R; //Resolution of image per second [b]
        Event Event1 = new Event();
        int timeSimulation;

        public Buffer Get_Buffer
        {
            get { return Buff1; }
            set { Buff1 = value; }
        }

        public Stream Get_Stream
        {
            get { return Stream1; }
            set { Stream1 = value; }
        }

        public int Get_vp
        {
            get { return vp; }
            set { vp = value; }
        }

        public int get_R
        {
            get { return R; }
            set { R = value; }
        }

        public Player ()
        {
            R = 5; //10 klatek na sekundę
            vp = 50;
            timeSimulation = 60;
        }

        public Player(int R, int vp, int timeSimulation)
        {
            this.R = R;
            this.vp = vp;
            this.timeSimulation = timeSimulation;
        }

        public void run()
        {
            Random rnd =new Random();

            float currentTime = 0;
            float timeEvent = 0;
            bool AutoGenEvent = false;
            Event E = new Event();
            float temp;
            float velocity=0;
            string pathBuffSize = "E:/Piotr/Programowanie/C#/AISDE2/Lab2Console/Lab2Console/PlotBuffSize.txt";
            System.IO.StreamWriter fileY = new System.IO.StreamWriter(pathBuffSize);

            while (currentTime < timeSimulation)
            {
                //fileY.WriteLine(Buff1.BufSize);
                if(!AutoGenEvent)
                {
                    E = Stream1.GenerationEvent(rnd);
                    timeEvent = E.Duration;
                }

                if (Buff1.BufSize > 0)
                {
                   
                    temp = play(E,ref currentTime);
                   // currentTime += temp;

                    if (temp <timeEvent)
                    {
                        AutoGenEvent = true;
                        velocity = E.StreamVelocity;
                        E = Stream1.GenerationEvent(rnd);
                        E.StreamVelocity = velocity;
                        timeEvent -= temp;
                        E.Duration = timeEvent;

                        //New Event would have time equal to length of this short, velocity stays the same as it was
                    }
                    else
                        AutoGenEvent = false;
                    //fileY.Write("Play");
                    fileY.WriteLine(Buff1.BufSize);
                }
                else if (Buff1.BufSize <= 0)
                {
                    while (Buff1.BufSize < Buff1.Bmax)
                    {
                        E = Stream1.GenerationEvent(rnd);
                        timeEvent = E.Duration;
                        temp = Buff1.load(E, ref currentTime);
                        
                        if (temp < timeEvent)
                        {
                            AutoGenEvent = true;
                            velocity = E.StreamVelocity;                         
                            E = Stream1.GenerationEvent(rnd);
                            E.StreamVelocity = velocity;
                            timeEvent -= temp;
                            E.Duration = timeEvent;
                            //E.Duration = E.Duration - temp;
                            //velocity stays the same
                        }
                        else
                            AutoGenEvent = false;
                        fileY.WriteLine(Buff1.BufSize);
                        //fileY.Write("Load");
                    }

                }
            }
            fileY.Close();
        }

        public float play(Event eventE,ref float  currentTime)
        {
            float Default = Buff1.BufSize;
            float time = 0;
            Buff1.BufSize = Buff1.BufSize - (vp * eventE.Duration //Calculating actual Buffer size
                    - eventE.Duration * eventE.StreamVelocity);
            if (eventE.StreamVelocity < vp)
            {
                if (Buff1.BufSize < 0)
                {
                    Buff1.BufSize = Default;
                    time = -Buff1.BufSize / (vp - eventE.StreamVelocity);
                    Buff1.BufSize = 0;
                    time = eventE.Duration - time;
                }
                time = eventE.Duration;
            }
            else if (eventE.StreamVelocity == vp)
            {
                return eventE.Duration;
            }
            else //(eventE.StreamVelocity > vp)
            {
                if(Buff1.BufSize > Buff1.Capacity)
                {
                    Buff1.BufSize = Buff1.Capacity;
                }
                time = eventE.Duration;
                //else - Buff1.BufSize calculated correctly, time = eventE.Duration;
            }

            currentTime += time;
            eventE.timeFromStart = currentTime;
            eventE.Duration = time;
            Buff1.EventsToPrint.Add(eventE);
            return time;
        }

    }
}
