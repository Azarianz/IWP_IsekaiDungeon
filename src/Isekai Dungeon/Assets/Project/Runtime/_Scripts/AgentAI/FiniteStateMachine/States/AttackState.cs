using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace FSM.STATES
{
    public class AttackState : IState
    {
        //Variables
        private readonly Agent_AI agent;
        private readonly Agent_Data agent_data;
        private float attackTimer = 0;
        private float attackInterval = 10f; // Set the interval between attacks here

        //Constructor
        public AttackState(Agent_AI context, Agent_Data data) 
        { 
            agent = context; 
            agent_data = data;
        }

        public void OnEnterState() 
        {
            float spd = agent_data.GetAgentStats().STAT_SPEED;

            // Trigger the attack animation event
            attackInterval = 10 - spd;    //Attack Spd Formula: 10 - SpdStat (5-8)
            //Debug.Log("Enter AttackState");
        }

        public void DoState() 
        {
            if(agent.Agent_Target == null)
            {
                OnExitState();
            }

            // Damage the enemy's health at regular intervals
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackInterval)
            {
                agent_data.GetAgentClass().CastAbility(agent);
                attackTimer = 0f;
            }
            agent.OnIdle?.Invoke();

            //Debug.Log("Do AttackState");
        }

        public void OnExitState()
        {
            agent.IsAttacking(false);
            // Reset the attack state when exiting

            //Debug.Log("Exit AttackState");
        }
    }
}
