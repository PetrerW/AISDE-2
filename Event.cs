using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Console
{
    class Event
    {
        float Vs;
        float duration;
        float timeInProgramm;

        public Event()
        {
            Vs = 0;
            duration = 0;
            timeInProgramm = 0;
        }

        public Event(float Vs, float duration)
        {
            this.Vs = Vs;
            this.duration = duration;

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


    }
}
