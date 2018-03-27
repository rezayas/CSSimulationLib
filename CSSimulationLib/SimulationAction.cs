using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib
{
    public enum EnumActionType : int
    {
        Default = 1,    // represents the "no action" alternative that is always on
        Additive = 2,   // represents actions that could be added to the "default" interventions
    }

    public class SimulationAction
    {

        public int Index{ get; set; }   // 0, 1, 2, ...
        public string Name { get; set; }
        public EnumActionType ActionType { get; set; }  // default or additive
        public SimulationDecisionRule DecisionRule { get; set; }   // pointer to the decision rule that guides the employment of this action

        // costs
        public double FixedCost { get; set; }          // fixed cost to switch on
        public double CostPerDecisionPeriod { get; set; }  // cost of using during a decision period
        public double PenaltyForSwitchingFromOnToOff { get; set; }

        // availability
        public long TIndexBecomesAvailable { get; set; }
        public long TIndexBecomesUnavailable { get; set; }
        public bool RemainOnOnceTurnedOn { get; set; }

        // usage statistics
        public bool IfHasBeenTrunedOnBefore { get; set; } = false;
        public int NumOfSwitchesOccured { get; set; }
        public int NumOfDecisionPeriodsOverWhichThisInterventionWasUsed { get; set; }

        // Instantiation
        public SimulationAction(
            int indext, 
            string name,
            EnumActionType actionType,
            long timeIndexBecomesAvailable, 
            long timeIndexBecomesUnavailable,             
            ref SimulationDecisionRule decisionRule)
        {
            Index = indext;
            Name = name;
            ActionType = actionType;
            TIndexBecomesAvailable = timeIndexBecomesAvailable;
            TIndexBecomesUnavailable = timeIndexBecomesUnavailable;
            DecisionRule = decisionRule;
        }

        // set up cost
        public void SetUpCost(double fixedCost, double costPerDecisionPeriod, double penaltyForSwitchingFromOnToOff)
        {
            FixedCost = fixedCost;
            CostPerDecisionPeriod = costPerDecisionPeriod;
            PenaltyForSwitchingFromOnToOff= penaltyForSwitchingFromOnToOff;
        }

    }

}
