using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace FSM.STATES
{
    public class IdleState : IState
    {
        //Variables
        private readonly Agent_AI agent;
        private readonly Agent_Data agent_data;

        //Constructor
        public IdleState(Agent_AI context, Agent_Data data) { agent = context; agent_data = data; }

        public void OnEnterState() 
        {
            //Debug.Log("Enter IdleState");
        }

        public void DoState() 
        {
            agent.OnIdle?.Invoke();
            //Debug.Log("Do IdleState");
        }

        public void OnExitState() 
        {
            //Debug.Log("Exit IdleState");
        }
    }
}

