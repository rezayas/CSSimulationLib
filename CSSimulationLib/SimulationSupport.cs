using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimulationLib
{
    static class SimulationSupport
    {
        private static StreamWriter _traceFile;
        private static bool _traceOn = false; // shoudl the simulation be traced

        // initialize the simulation
        public static void Initialize(bool traceOn)
        {
            // reset the calender
            SimulationEventCalendar.Reset();
            if (traceOn == true) 
            {
                _traceOn = true;
                _traceFile = new StreamWriter("Trace.txt");
            }
        }

        // simulate
        public static void Simulate(long numOfReplications, double simulationLength)
        {            
            SimulationEvent nextEvent;
            bool toContinue = true;

            for (long i = 1; i <= numOfReplications; ++i)
            {
                while (toContinue)
                {
                    // get the next event
                    nextEvent = SimulationEventCalendar.GetNextEvent();
                    if (nextEvent == null)
                        toContinue = false;
                    else
                    {
                        // advance time
                        SimulationEventCalendar.CurrentTime = nextEvent.EventTime;
                        // if simulation should be ended
                        if (SimulationEventCalendar.CurrentTime > simulationLength)
                            if (_traceOn == true)
                            {
                                _traceFile.WriteLine("Simulation replication stopped.");
                                toContinue = false;
                            }
                        if (_traceOn == true) _traceFile.WriteLine(" At time = " + SimulationEventCalendar.CurrentTime.ToString("0.000"));
                        // execute next event
                        nextEvent.EventProcess();
                    }
                }
            }
        }

        // finalize
        public static void FinalizeSimulation()
        {
            if (_traceOn == true)
                _traceFile.Close();
        }

    }
}
