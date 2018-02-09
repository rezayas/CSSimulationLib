using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationLib
{
    public enum EnumDecisionRule
    {
        Predetermined = 1,      // always one or off
        Periodic = 2,           // employ at certain frequency
        ThresholdBased = 3,     // employ once a threshold is passed
        IntervalBased = 4,      // employ during a certain time interval
        Dynamic = 5             // guided by a dynamic policy 
    }
    public enum EnumSwitchStatus
    {
        Off = 0,
        On = 1,
    }

    public abstract class SimulationDecisionRule
    {
        public static EnumDecisionRule ConvertToOnOffSwitchSettings(string strOnOffSwitchSetting)
        {
            EnumDecisionRule onOffSwitchSetting = EnumDecisionRule.Predetermined;
            switch (strOnOffSwitchSetting)
            {
                case "Predetermined":
                    onOffSwitchSetting = EnumDecisionRule.Predetermined;
                    break;
                case "Periodic":
                    onOffSwitchSetting = EnumDecisionRule.Periodic;
                    break;
                case "Threshold-Based":
                    onOffSwitchSetting = EnumDecisionRule.ThresholdBased;
                    break;
                case "Interval-Based":
                    onOffSwitchSetting = EnumDecisionRule.IntervalBased;
                    break;
                case "Dynamic":
                    onOffSwitchSetting = EnumDecisionRule.Dynamic;
                    break;
            }
            return onOffSwitchSetting;
        }
    }

    // predetermined decision rule 
    public class Predetermined : SimulationDecisionRule
    {
        EnumSwitchStatus _predeterminedSwitchValue = EnumSwitchStatus.Off;

        public Predetermined(EnumSwitchStatus predeterminedSwitchValue)
        {
            _predeterminedSwitchValue = predeterminedSwitchValue;
        }

        public EnumSwitchStatus PredeterminedSwitchValue
        {
            get { return _predeterminedSwitchValue; }
        }
    }

    // periodic decision rule 
    public class Periodic : SimulationDecisionRule
    {
        private int _frequency_nOfDcisionPeriods = 0;
        private int _duration_nOfDcisionPeriods = 0;

        public Periodic(int frequency_nOfDcisionPeriods, int duration_nOfDcisionPeriods)
        {
            _frequency_nOfDcisionPeriods = frequency_nOfDcisionPeriods;
            _duration_nOfDcisionPeriods = duration_nOfDcisionPeriods;
        }
    }

    // threshold-based decision rule 
    public class ThresholdBased : SimulationDecisionRule
    {
        private double _threshold = 0;
        private int _duration_nOfTimeIndices= 0;

        public ThresholdBased(double threshold, int duration_nOfTimeIndices)
        {
            _threshold = threshold;
            _duration_nOfTimeIndices = duration_nOfTimeIndices;
        }
    }

    // interval-based decision rule 
    public class IntervalBased : SimulationDecisionRule
    {
        private int _timeIndexToTurnOn;
        private int _timeIndexToTurnOff;

        public IntervalBased(int timeIndexToTurnOn, int timeIndexToTurnOff)
        {
            _timeIndexToTurnOn = timeIndexToTurnOn;
            _timeIndexToTurnOff = timeIndexToTurnOff;
        }
    }

    // dynamic decision rule 
    public class Dynamic:SimulationDecisionRule
    {
        private bool _remainsOnOnceSwitchedOn = false;

        public Dynamic(bool remainsOnOnceSwitchedOn)
        {
            _remainsOnOnceSwitchedOn = remainsOnOnceSwitchedOn;
        }
    }


}
