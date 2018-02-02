using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib
{
    public class SimulationAction
    {
        public enum enumOnOffSwitchSetting
        {
            Predetermined = 1,
            Periodic = 2,
            ThresholdBased = 3,
            IntervalBased = 4,
            Dynamic = 5
        }
        public enum enumSwitchValue
        {
            Off = 0,
            On = 1,
        }
        public enum enumActionType : int
        {
            Default = 1,
            Additive = 2,
            MutuallyExclusive = 3,
        }

        // Fields
        int _index;
        int _ID;
        string _name;
        private enumActionType _type;
        enumOnOffSwitchSetting _onOffSwitchSetting = enumOnOffSwitchSetting.Predetermined;
        enumSwitchValue _predeterminedSwitchValue = enumSwitchValue.Off;

        double _fixedCost;
        double _costPerUnitOfTime;
        double _penaltyForSwitchingFromOnToOff;

        long _timeIndexBecomeAvailable;
        long _timeIndexBecomeUnavailable;
        bool _hasResourceCondition = false;
        int _IDOfTheResourceRequiredToBeAvailable;

        int _numOfSwitchesOccured;
        bool _remainsOnOnceSwitchedOn;
        private int _numOfDecisionPeriodsOverWhichThisInterventionWasUsed = 0;

        // Instantiation
        public SimulationAction(int indext, int ID, string name, enumActionType type)
        {
            _index = indext;
            _ID = ID;
            _name = name;
            _type = type;
            _hasResourceCondition = false;
        }

        // Properties   
        public int Index
        {
            get { return _index; }
        }
        public int ID
        {
            get { return _ID; }
        }
        public string Name
        {
            get { return _name; }
        }
        public enumActionType Type
        {
            get { return _type; }
        }
        public enumOnOffSwitchSetting OnOffSwitchSetting
        {
            get { return _onOffSwitchSetting; }
            set { _onOffSwitchSetting = value; }
        }
        public enumSwitchValue PredeterminedSwitchValue
        {
            get { return _predeterminedSwitchValue; }
            set { _predeterminedSwitchValue = value; }
        }
        public int NumOfSwitchesOccured
        {
            get { return _numOfSwitchesOccured; }
            set { _numOfSwitchesOccured = value; }
        }
        public bool RemainsOnOnceSwitchedOn
        {
            get { return _remainsOnOnceSwitchedOn; }
        }
        public double FixedCost
        {
            get { return _fixedCost; }
        }
        public double CostPerUnitOfTime
        {
            get { return _costPerUnitOfTime; }
        }
        public double PenaltyForSwitchingFromOnToOff
        {
            get { return _penaltyForSwitchingFromOnToOff; }
        }
        public long TimeIndexBecomeAvailable
        {
            get { return _timeIndexBecomeAvailable; }
        }
        public long TimeIndexBecomeUnavailable
        {
            get { return _timeIndexBecomeUnavailable; }
        }
        public bool HasResourceCondition
        {
            get { return _hasResourceCondition; }
        }
        public int IDOfTheResourceRequiredToBeAvailable
        {
            get { return _IDOfTheResourceRequiredToBeAvailable; }
        }
        public int NumOfDecisionPeriodsOverWhichThisInterventionWasUsed
        {
            get { return _numOfDecisionPeriodsOverWhichThisInterventionWasUsed; }
            set { _numOfDecisionPeriodsOverWhichThisInterventionWasUsed = value; }
        }

        // Methods
        // set up the time availability
        public void SetupAvailability(enumOnOffSwitchSetting onOffSwitchSetting, long timeIndexBecomeAvailable, long timeIndexBecomeUnavailable)
        {
            _onOffSwitchSetting = onOffSwitchSetting;
            _timeIndexBecomeAvailable = timeIndexBecomeAvailable;
            _timeIndexBecomeUnavailable = timeIndexBecomeUnavailable;
        }
        // set up the resource availability
        public void SetupResourceRequirement(int IDOfTheResourceRequiredToBeAvailable)
        {
            _hasResourceCondition = true;
            _IDOfTheResourceRequiredToBeAvailable = IDOfTheResourceRequiredToBeAvailable;

        }
        // set up cost
        public void SetUpCost(double fixedCost, double costPerUnitOfTime, double penaltyForSwitchingFromOnToOff)
        {
            _fixedCost = fixedCost;
            _costPerUnitOfTime = costPerUnitOfTime;
            _penaltyForSwitchingFromOnToOff = penaltyForSwitchingFromOnToOff;
        }

        // set up dynamic employment
        public void SetupDynamicEmployment(bool remainsOnOnceSwitchedOn)
        {
            _remainsOnOnceSwitchedOn = remainsOnOnceSwitchedOn;
        }

        // support methods
        public static enumActionType ConvertToActionType(string strInterventionType)
        {
            enumActionType interventionType = enumActionType.Default;
            switch (strInterventionType)
            {
                case "Default":
                    interventionType = enumActionType.Default;
                    break;
                case "Additive":
                    interventionType = enumActionType.Additive;
                    break;
                case "Mutually Exclusive":
                    interventionType = enumActionType.MutuallyExclusive;
                    break;
            }
            return interventionType;
        }
        public static enumOnOffSwitchSetting ConvertToOnOffSwitchSettings(string strOnOffSwitchSetting)
        {
            enumOnOffSwitchSetting onOffSwitchSetting = enumOnOffSwitchSetting.Predetermined;
            switch (strOnOffSwitchSetting)
            {
                case "Predetermined":
                    onOffSwitchSetting = enumOnOffSwitchSetting.Predetermined;
                    break;
                case "Periodic":
                    onOffSwitchSetting = enumOnOffSwitchSetting.Periodic;
                    break;
                case "Threshold-Based":
                    onOffSwitchSetting = enumOnOffSwitchSetting.ThresholdBased;
                    break;
                case "Interval-Based":
                    onOffSwitchSetting = enumOnOffSwitchSetting.IntervalBased;
                    break;
                case "Dynamic":
                    onOffSwitchSetting = enumOnOffSwitchSetting.Dynamic;
                    break;
            }
            return onOffSwitchSetting;
        }
        public static enumSwitchValue ConvertToSwitchValue(string value)
        {
            enumSwitchValue switchValue = enumSwitchValue.On;

            switch (value)
            {
                case "On":
                    switchValue = enumSwitchValue.On;
                    break;
                case "Off":
                    switchValue = enumSwitchValue.Off;
                    break;
            }
            return switchValue;
        }
    }
}
