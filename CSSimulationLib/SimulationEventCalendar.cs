using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib
{
    static class SimulationEventCalendar
    {
        static private double _tNow;
        static private List<SimulationEvent> _colEvents = new List<SimulationEvent>();

        // Properties
        static public double CurrentTime
        {
            get { return _tNow; }
            set
            {
                if (value >= _tNow)
                    _tNow = value;
                else
                {
                    Console.WriteLine("Error: trying to set an event time less than current time.");                    
                }
            }
        }
        static public int NumOfEventsInCalendar
        {
            get { return _colEvents.Count; }
        }

        // Methods
        // add an event to calendar
        static public void ScheduleEvent(SimulationEvent addedEvent)
        {
            // if calendar is empty
            if (_colEvents.Count == 0) // no event in the calendar
            {
                _colEvents.Add(addedEvent);
                return;
            }            
            
            int numOfEvents = _colEvents.Count;
            double lastEventTime = _colEvents[numOfEvents - 1].EventTime;
            int lastEventPriority = _colEvents[numOfEvents - 1].EventPriority;

            // if this event should be scheduled last in the calendar
            if (lastEventTime < addedEvent.EventTime ||
                    (lastEventTime == addedEvent.EventTime) && (lastEventPriority <= addedEvent.EventPriority) )
                // add this event after last event
                _colEvents.Add(addedEvent);

            else // keep events ordered in eventCalendar by eventTime and secondarily by priority
            {                
                // search calendar 
                int i = 0;
                while (i < numOfEvents)
                {
                    // if event i has greater time, insert the event here
                    if (_colEvents[i].EventTime > addedEvent.EventTime)
                    {
                        _colEvents.Insert(i, addedEvent);
                        return;
                    }
                    else // if event i has the same time as the new event
                    {
                        if (_colEvents[i].EventPriority > addedEvent.EventPriority)
                        {
                            _colEvents.Insert(i, addedEvent);
                            return;
                        }
                    }
                }
            }
        }

        // get next event
        static public SimulationEvent GetNextEvent()
        {
            if (_colEvents.Count == 0)
            {
                //MessageBox.Show("Error: no more events in calendar", "Error!",MessageBoxButtons.OK);
                return null;
            }
            // return and remove the first event in the list
            return RemoveAndReturnAnEvent(0);
        }

        // remove an event from the event calender
        static private SimulationEvent RemoveAndReturnAnEvent(int index)
        {
            // get the event
            SimulationEvent thisEvent = (SimulationEvent)_colEvents[index];
            // remove the event
            _colEvents.RemoveAt(index);
            // return the event
            return thisEvent;
        }
        
        // delete events of particular type from event calendar
        static public void DeleteEventOfType(SimulationEvent eventType)
        {
            int eventIndex = 0;
            foreach (SimulationEvent thisEvent in _colEvents)
            {
                if (thisEvent.ToString() == eventType.ToString())
                    _colEvents.RemoveAt(eventIndex);
                ++eventIndex;
            }
        }

        // clear calendar
        static public void Reset()
        {
            _colEvents.Clear();
            _tNow = 0;
        }
    }
}
