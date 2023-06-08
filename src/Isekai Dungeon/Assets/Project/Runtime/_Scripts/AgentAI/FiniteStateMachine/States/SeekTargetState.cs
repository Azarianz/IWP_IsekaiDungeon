using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace FSM.STATES
{
    public class SeekTargetState : IState
    {
        //Variables
        private readonly Agent_AI agent;
        private readonly Agent_Data agent_data;
        private readonly Agent_AI target;

        //Constructor
        public SeekTargetState(Agent_AI context, Agent_Data data)
        {
            agent = context; agent_data = data;
            this.target = target;
        }

        public void OnEnterState() 
        {
            //Debug.Log("Enter SeekTargetState");
        }

        public void DoState() 
        {
            agent.OnMove?.Invoke();
            //Debug.Log("Do SeekTargetState");
        }

        public void OnExitState()
        {
            //Debug.Log("Exit SeekTargetState");
        }
    }
}


