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
        public MoveState(Agent_AI context, Agent_Data data) 
        { 
            agent = context; 
            agent_data = data;
        }

        public void OnEnterState() 
        {
            //Debug.Log("Enter MoveState");
        }

        public void DoState() 
        {
            agent.OnMove?.Invoke();
            //Debug.Log("Do MoveState");
        }

        public void OnExitState() 
        {
            //Debug.Log("Exit MoveState");
        }
    }

}

