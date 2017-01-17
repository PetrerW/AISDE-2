using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Console
{
    class Player
    {

        Buffer Buff1;
        Stream Stream1 = new Stream();
        int[] vp=new int [3]; //Playing velocity [b/s]
        int vp_current;
        int R; //Resolution of image per second [b]
        //Event Event1 = new Event();
        List<Event> Event1 = new List<Event>();
        int timeSimulation;
        float currentTime = 0;

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

        public int[] Get_vp
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
            vp[2] = 50;
            vp[1] = 40;
            vp[0] = 30;
            timeSimulation = 60;
            Buff1 = new Buffer(vp[2]); //high vp
        }

        public Player(int R, int vpHigh,int vpMedium,
            int vpLow, int timeSimulation)
        {
            this.R = R;
            this.vp[0] = vpLow;
            this.vp[1] = vpMedium;
            this.vp[2] = vpHigh;
            this.timeSimulation = timeSimulation;
            Buff1 = new Buffer(vp[2]); //high vp
        }

        public void run()
        {
            Random rnd =new Random();

           
            float timeEvent = 0;
            bool AutoGenEvent = false;
            List< Event> E = new List< Event>();
            float temp;
            float velocity=0;
            //string pathBuffSize = "C:/Users/Daniel/Documents/Visual Studio 2015/Projects/Lab2Console/Lab2Console/PlotBuffSize.txt";
            string pathBuffSize = "E:/Piotr/Programowanie/C#/AISDE2/Stare/Lab2Console/Lab2Console/PlotBuffSize.txt";
            System.IO.StreamWriter fileY = new System.IO.StreamWriter(pathBuffSize);

            while (currentTime < timeSimulation)
            {
                //fileY.WriteLine(Buff1.BufSize);
                if(!AutoGenEvent)
                {
                    E.Add( Stream1.GenerationEvent(rnd,ref currentTime));
                    timeEvent = E[0].Duration;
                }

                if (Buff1.BufSize > 0)
                {
                   
                    temp = play(E,ref currentTime);
                    fileY.WriteLine($"{ (int)Buff1.BufSize}.{(int)((Buff1.BufSize - (int)Buff1.BufSize)*100)}");
                    // currentTime += temp;

                    if (temp < timeEvent) //Event has been cut before 
                    {
                        AutoGenEvent = true;
                        velocity = E[0].StreamVelocity;
                        E.RemoveAt(0);
                        E.Add(Stream1.GenerationEvent(rnd,ref currentTime));
                        E[0].StreamVelocity = velocity;
                        timeEvent -= temp;
                        E[0].Duration = timeEvent;

                        //New Event would have time equal to length of this short, velocity stays the same as it was
                    }
                    else
                    {
                        AutoGenEvent = false;
                        E.RemoveAt(0);
                    }
                    //fileY.Write("Play");
                  
                }
                else if (Buff1.BufSize <= 0)
                {
                    while (Buff1.BufSize < Buff1.Bmax)
                    {
                        if (E.Count > 0)
                        {
                            E.RemoveAt(0);
                        }
                        E.Add (Stream1.GenerationEvent(rnd,ref currentTime));
                        timeEvent = E[0].Duration;
                        temp = Buff1.load(E, ref currentTime);
                        fileY.WriteLine($"{ (int)Buff1.BufSize}.{(int)((Buff1.BufSize-(int)Buff1.BufSize)*100)}");

                        if (temp < timeEvent)
                        {
                            AutoGenEvent = true;
                            velocity = E[0].StreamVelocity;
                            E.RemoveAt(0);
                            E.Add(Stream1.GenerationEvent(rnd, ref currentTime));
                            E[0].StreamVelocity = velocity;
                            timeEvent -= temp;
                            E[0].Duration = timeEvent;
                            //E.Duration = E.Duration - temp;
                            //velocity stays the same
                        }
                        else
                        {
                            AutoGenEvent = false;
                            E.RemoveAt(0);
                        }
                       
                        //fileY.Write("Load");
                    }

                }
            }
            fileY.Close();
        }

        public float play(List<Event> eventE,ref float  currentTime)
        {
            float Default = Buff1.BufSize;
            float time = 0;
            int ground = 0;

            if (Buff1.BufSize > vp[1] * 30)
            {
                vp_current = vp[2];
                ground = vp[1] * 30;

            }
            else if (Buff1.BufSize > vp[0] * 30)
            {
                vp_current = vp[1];
                ground = vp[0] * 30;
            }
            else
            {
                vp_current = vp[0];
                ground = 0;
            }

            Buff1.BufSize = Buff1.BufSize - (vp_current * eventE[0].Duration //Calculating actual Buffer size
                    - eventE[0].Duration * eventE[0].StreamVelocity);
            if (eventE[0].StreamVelocity < vp_current)
            {
                if (Buff1.BufSize < ground)
                {
                    Buff1.BufSize = Default;
                    time = (ground-Buff1.BufSize) / (-vp_current + eventE[0].StreamVelocity);
                    Buff1.BufSize = ground;
                    time = eventE[0].Duration - time;
                    
                }
                else
                    time = eventE[0].Duration;
            }
            else if (eventE[0].StreamVelocity == vp_current)
            {
                return eventE[0].Duration;
            }
            else //(eventE.StreamVelocity > vp_current)
            {
                if(Buff1.BufSize > Buff1.Capacity)
                {
                    Buff1.BufSize = Buff1.Capacity;
                }
                time = eventE[0].Duration;
                //else - Buff1.BufSize calculated correctly, time = eventE.Duration;
            }
            
            currentTime += time;
            eventE[0].timeFromStart = currentTime;
            eventE[0].Duration = time;
            Buff1.EventsToPrint.Add(eventE[0]);
            Buff1.vpList.Add(vp_current);

            return time;
        }

    }
}
