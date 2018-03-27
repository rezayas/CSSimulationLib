using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ComputationLib;
using RandomVariateLib;

namespace SimulationLib
{
    class SimulationDecisionMaker
    {
        private List<SimulationAction> Actions { get; set; } = new List<SimulationAction>();

        public int NumOfActions { get; set; } = 0;
        public int NumOfActionsControlledDynamically { get; set; } = 0;
        private int[] CurrentActionCombination { get; set; } = new int[0];   // array of 0 and 1 to represent which action is on or off
        private int[] DefaultActionCombination { get; set; } = new int[0];   // if all other actions become unavailable, we will use this action combination 

        // Instantiation
        public SimulationDecisionMaker()
        {
        }

        // add a decision
        public void AddAnAction(SimulationAction action)
        {
            // add the actions
            Actions.Add(action);
            ++NumOfActions;

            // update the default and current action combination 
            if (action.ActionType == EnumActionType.Default)
            {
                DefaultActionCombination.Append(1);
                CurrentActionCombination.Append(1);
            }
            else
            {
                DefaultActionCombination.Append(0);
                CurrentActionCombination.Append(0);
            }

            // check if this action is controlled dynamically
            if (action.DecisionRule is Dynamic)
            {
                ++NumOfActionsControlledDynamically;
            }
        }
                
        // update the currect action combination       
        public void ChangeCurrentActionCombination(int[] newActionCombination, ref double resultingCost)
        {
            int thisActionIndex = 0;
            foreach (SimulationAction thisAction in Actions)
            {
                thisActionIndex = thisAction.Index;
                // calculate the number of switches        
                if (CurrentActionCombination[thisActionIndex] != newActionCombination[thisActionIndex])
                    thisAction.NumOfSwitchesOccured += 1;
                // calculate the fixed cost
                if (CurrentActionCombination[thisActionIndex] == 0 && newActionCombination[thisActionIndex] == 1)
                {
                    resultingCost += thisAction.FixedCost;
                    thisAction.NumOfSwitchesOccured += 1;
                }
                // calculate the penalty cost for switching from on to off
                if (thisAction.PenaltyForSwitchingFromOnToOff > 0)
                {
                    if (CurrentActionCombination[thisActionIndex] == 1 && newActionCombination[thisActionIndex] == 0)
                        resultingCost += thisAction.PenaltyForSwitchingFromOnToOff;
                }
                // update the new action
                CurrentActionCombination[thisActionIndex] = newActionCombination[thisActionIndex];
                // update the cost per unit of time for this action combination
                if (CurrentActionCombination[thisActionIndex] == 1)
                {
                    resultingCost += thisAction.CostPerDecisionPeriod;
                    ++thisAction.NumOfDecisionPeriodsOverWhichThisInterventionWasUsed;
                }
            }
        }
        public void ChangeCurrentActionCombination(int[] newActionCombination)
        {
            double tempCost = 0;
            ChangeCurrentActionCombination(newActionCombination, ref tempCost);
        }
        
    }
}