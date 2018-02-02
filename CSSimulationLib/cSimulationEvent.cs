using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib
{
    public abstract class SimulationEvent
    {
        double _eventTime;
        int _eventPriority; // low eventPriority means higher priority

        // instantiation
        public SimulationEvent(double eventTime, int eventPriority)
        {
            _eventTime = eventTime;
            _eventPriority = eventPriority;
        }
        public SimulationEvent(double eventTime)
        {
            _eventTime = eventTime;            
        }

        public double EventTime
        {
            get { return _eventTime; }
        }
        public int EventPriority
        {
            get { return _eventPriority; }
            set { _eventPriority = value; }
        }

        public virtual void EventProcess()
        {            
        }

    }
}
