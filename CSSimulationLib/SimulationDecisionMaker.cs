using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ComputationLib;
using RandomVariateLib;

namespace SimulationLib
{
    public class SimulationDecisionMaker
    {
        public List<SimulationAction> Actions { get; set; } = new List<SimulationAction>();
        public int NumOfActions { get; set; } = 0;
        public int NumOfActionsControlledDynamically { get; set; } = 0;
        public int[] CurrentActionCombination { get; set; } = new int[0];   // array of 0 and 1 to represent which action is on or off
        public int[] DefaultActionCombination { get; set; } = new int[0];   // if all other actions become unavailable, we will use this action combination 
        public double AccumulatedCost { get; set; } = 0; // cost accumulated due to actions and decision making
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
            if (action.DecisionRule is DecionRule_Dynamic)
            {
                ++NumOfActionsControlledDynamically;
            }
        }

        // find a new action combination 
        public int[] FindNewActionCombination(int timeIndex)
        {
            int[] newActionCombination = new int[0];

            // find the switch status of each action
            foreach (SimulationAction simAction in Actions)
            {
                newActionCombination.Append(simAction.FindSwitchStatus(timeIndex));
            }            
            return newActionCombination;
        }

        
        // update the currect action combination       
        public void UpdateCurrentActionCombination(int[] newActionCombination)
        {
            int thisActionIndex = 0;
            foreach (SimulationAction thisAction in Actions)
            {
                thisActionIndex = thisAction.Index;
                // calculate the number of switches        
                if (CurrentActionCombination[thisActionIndex] != newActionCombination[thisActionIndex])
                    thisAction.NumOfSwitchesOccured += 1;
                
                // if turning on
                if (CurrentActionCombination[thisActionIndex] == 0 && newActionCombination[thisActionIndex] == 1)
                {
                    AccumulatedCost += thisAction.FixedCost;
                    thisAction.NumOfSwitchesOccured += 1;
                    thisAction.IfHasBeenTrunedOnBefore = true;                    
                }
                
                // calculate the penalty cost for switching from on to off
                if (thisAction.PenaltyForSwitchingFromOnToOff > 0)
                {
                    if (CurrentActionCombination[thisActionIndex] == 1 && newActionCombination[thisActionIndex] == 0)
                        AccumulatedCost += thisAction.PenaltyForSwitchingFromOnToOff;
                }
                
                // update the new action
                CurrentActionCombination[thisActionIndex] = newActionCombination[thisActionIndex];

                // update the cost per unit of time for this action combination
                if (CurrentActionCombination[thisActionIndex] == 1)
                {
                    AccumulatedCost += thisAction.CostPerDecisionPeriod;
                    ++thisAction.NumOfDecisionPeriodsOverWhichThisInterventionWasUsed;
                }
            }
        }

        public void ResetForAnotherSimulationRun()
        {
            AccumulatedCost = 0;
            CurrentActionCombination = (int[])DefaultActionCombination.Clone();
        }
        
    }
}