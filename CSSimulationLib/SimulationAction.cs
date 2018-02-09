using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib
{
    public class SimulationAction
    {        
        public enum EnumActionType : int
        {
            Default = 1,    // represents the "no action" alternative that is always on
            Additive = 2,
        }

        int _index; // 0, 1, 2, ...
        string _name;
        private EnumActionType _actionType = EnumActionType.Default;
        SimulationDecisionRule _decisionRule;
        
        // cost parameters
        double _fixedCost;  // fixed cost to switch on
        double _costPerUnitOfTime;
        double _penaltyForSwitchingFromOnToOff;
        
        // availability
        protected long _timeIndexBecomeAvailable;
        protected long _timeIndexBecomeUnavailable;
        protected bool _hasResourceCondition = false;
        protected int _IDOfTheResourceRequiredToBeAvailable;
        protected long _nOfTimeIndeciesDelayedToGoIntoEffectOnceTurnedOn = 0;

        // statistics
        protected int _numOfSwitchesOccured;
        protected int _numOfDecisionPeriodsOverWhichThisInterventionWasUsed = 0;

        // Instantiation
        public SimulationAction(
            int indext, 
            string name, 
            EnumActionType actionType,
            long timeIndexBecomeAvailable, 
            long timeIndexBecomeUnavailable, 
            long nOfTimeIndeciesDelayedToGoIntoEffectOnceTurnedOn,
            ref SimulationDecisionRule decisionRule,
            int IDOfTheResourceRequiredToBeAvailable = -1)
        {
            _index = indext;
            _name = name;
            _actionType = actionType;
            _timeIndexBecomeAvailable = timeIndexBecomeAvailable;
            _timeIndexBecomeUnavailable = timeIndexBecomeUnavailable;
            _nOfTimeIndeciesDelayedToGoIntoEffectOnceTurnedOn = nOfTimeIndeciesDelayedToGoIntoEffectOnceTurnedOn;
            _decisionRule = decisionRule;
            _IDOfTheResourceRequiredToBeAvailable = IDOfTheResourceRequiredToBeAvailable;
            if (_IDOfTheResourceRequiredToBeAvailable >= 0) { _hasResourceCondition = false; }

        }

        // Properties   
        public int Index
        {
            get { return _index; }
        }
        public string Name
        {
            get { return _name; }
        }
        public EnumActionType ActionType
        {
            get { return _actionType; }
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

        public int NumOfSwitchesOccured
        {
            get { return _numOfSwitchesOccured; }
        }
        public int NumOfDecisionPeriodsOverWhichThisInterventionWasUsed
        {
            get { return _numOfDecisionPeriodsOverWhichThisInterventionWasUsed; }
        }

        // set up cost
        public void SetUpCost(double fixedCost, double costPerUnitOfTime, double penaltyForSwitchingFromOnToOff)
        {
            _fixedCost = fixedCost;
            _costPerUnitOfTime = costPerUnitOfTime;
            _penaltyForSwitchingFromOnToOff = penaltyForSwitchingFromOnToOff;
        }

        // support methods
        public static EnumActionType ConvertToActionType(string strInterventionType)
        {
            EnumActionType interventionType = EnumActionType.Default;
            switch (strInterventionType)
            {
                case "Default":
                    interventionType = EnumActionType.Default;
                    break;
                case "Additive":
                    interventionType = EnumActionType.Additive;
                    break;
            }
            return interventionType;
        }
        public static EnumSwitchStatus ConvertToSwitchValue(string value)
        {
            EnumSwitchStatus switchValue = EnumSwitchStatus.On;

            switch (value)
            {
                case "On":
                    switchValue = EnumSwitchStatus.On;
                    break;
                case "Off":
                    switchValue = EnumSwitchStatus.Off;
                    break;
            }
            return switchValue;
        }
    }

}
