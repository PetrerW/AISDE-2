﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Console
{
    class Stream
    {
        List<Event> bag;
        Random rnd;
        //vs[0] - velocity in 1st second, vs[n] - velocity in n+1 second

        public List<Event> Get_bag
        {
            get { return this.bag; }
            set { this.bag = value; }
        }

  /*      public Stream(int n)
        {
            vs = new List<int>(n);
            if(n ==20) 
                ArbitraryValues(); //Setting Arbitrary Values

        }*/

  /*      public void ArbitraryValues()
        {
            //Size will be equal to 20
            for (int i = 0; i < 4; i++)
                vs.Add(150);
            for (int i = 4; i < 9; i++)
                vs.Add(100);
            for (int i = 9; i < 13; i++)
                vs.Add(50);
            for (int i = 13; i < 17; i++)
                vs.Add(30);
            for (int i = 17; i < 20; i++)
                vs.Add(0);
        }*/

        public Event GenerationEvent(Random rnd)
        {
            
            float velocity = rnd.Next(20, 81);


           float time = (float)rnd.NextDouble();

              time= (float) ((1 / -0.9) * Math.Log(1 - time) * (2) + 5);
            Event tmp = new Event(velocity, time);

            return tmp;
        }
    }
}
