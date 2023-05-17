using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace FSM.STATES
{
    public class MoveState : IState
    {
        //Variables
        private readonly Agent_AI agent;
        private readonly Agent_Data agent_data;

        //Constructor
        public MoveState(Agent_AI context, Agent_Data data) { agent = context; agent_data = data; }

        public void OnEnterState() { }

        public void DoState() 
        {
            agent.Move(agent.transform.up, agent_data.GetAgentStats().STAT_SPEED);
        }

        public void OnExitState() { }
    }

}

