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

        //Constructor
        public SeekTargetState(Agent_AI context, Agent_Data data) { agent = context; agent_data = data; }

        public void OnEnterState() { }

        public void DoState() { }

        public void OnExitState() { }
    }
}


