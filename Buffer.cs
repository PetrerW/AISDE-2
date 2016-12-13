using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Console
{
    class Buffer
    {
        public List<int> BufferSize; //Current buffer size in each cell
        public Event EventObj = new Event();
        public List<Event> ToPrintEvents = new List<Event>();
        float size;
        float bmax = 1500; //30s value
        float capacity = 3000;

        public List<Event> EventsToPrint
        {
            get { return ToPrintEvents; }
            set { ToPrintEvents = value; }
        }

        public List<int> GetBufferSize
        {
            get { return BufferSize; }
            set { BufferSize = value;  }
        }

        public float Capacity
        {
            get { return capacity; }
            set { capacity = value; }

        }

        public float Bmax
        {
            get { return bmax; }
            set { bmax = value; }

        }

        public float BufSize
        {
            get { return size; }
            set { size = value; }

        }
        public Event GetEvent
        {
            get { return EventObj; }
            set { this.EventObj = value; }

        }

        public Buffer()
        {
            BufferSize = new List<int>();
            size = 0;
        }

        public float load(Event eventE, ref float nowTime)
        { /*
            float TimeLoad = 0;

            while (size < bmax)
            {
                EventObj = (stream.GenerationEvent());
                ToPrintEvents.Add(EventObj);
                size = size + EventObj.StreamVelocity * EventObj.Duration;
                TimeLoad += EventObj.Duration;
                if(size>capacity)
                {
                    size = capacity; //Such is life
                    break;
                }
                EventObj.timeFromStart += TimeLoad;
            }
            return TimeLoad;
            */
            float TimeLoad = 0;

            EventObj = eventE;
            size = size + EventObj.StreamVelocity * EventObj.Duration; //Checking if buffer size would exceed bmax
            TimeLoad += EventObj.Duration;
            if (size > bmax) //exceeded
            {
                size = size - EventObj.StreamVelocity * EventObj.Duration; //Coming back with size to beginning value
                TimeLoad -= EventObj.Duration; 
                TimeLoad += (bmax - size) / eventE.StreamVelocity;
                size += eventE.StreamVelocity * TimeLoad;
            }
            nowTime += TimeLoad;
            EventObj.timeFromStart = nowTime;
            EventObj.Duration = TimeLoad;
            ToPrintEvents.Add(EventObj);
            return TimeLoad;
        } 
    }
}
