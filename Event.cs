using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Console
{
    enum Type {  low, high };
    class Event
    {
        int Type;
        float Vs;
        float duration;
        float timeInProgramm;
        float beginTime;
        float next;

        public Event()
        {
            Vs = 0;
            duration = 0;
            timeInProgramm = 0;
            beginTime = 0;
        }

        public Event(float Vs, float duration)
        {
            this.Vs = Vs;
            this.duration = duration;

        }

        

        public Event(float Vs, float duration, float beginTime, int Type)
        {
            this.Vs = Vs;
            this.duration = duration;
            this.beginTime = beginTime;
            this.Type = Type;

        }
        public int EventType
        {
            get {return Type; }
            set {this.Type=value; }
        }

        public float nextV
        {
            get { return next; }
            set { next = value; }
        }

  

        public float  StreamVelocity
            {
            get { return Vs; }
            set {this.Vs = value; }
            }

        public float Duration
        {
            get { return duration; }
            set { this.duration = value; }
        }

        public float timeFromStart
        {
            get { return timeInProgramm; }
            set { this.timeInProgramm = value; }
        }

        public float BeginingTime
        {
            get { return beginTime; }
            set { this.beginTime = value; }
        }

    }
}
