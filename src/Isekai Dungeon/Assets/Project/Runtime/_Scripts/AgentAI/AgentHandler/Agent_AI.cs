using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using FSM.STATES;

namespace AI
{
    [RequireComponent(typeof(Collider2D))]
    public class Agent_AI : MonoBehaviour
    {
        //Variables
        [SerializeField]
        private Agent_Data agentData;

        private StateMachine _stateMachine;
        public string sCurrentState;    //For Debug

        private Flock agentFlock;
        public Flock AgentFlock { get { return agentFlock; } }

        private Collider2D agentCollider;
        public Collider2D AgentCollider { get { return AgentCollider; } }

        //Function Bools
        private bool HasGameStarted()
        {
            if (GameManager.Instance.State == GameState.GameStart)
            {
                return true; 
            }
            else
            {
                return false; 
            }
        }
        private bool testCondition() { return false; }

        // Awake is called when the script is loaded
        private void Awake()
        {
            _stateMachine = new StateMachine();
            agentData = GetComponent<Agent_Data>();
            agentCollider = GetComponent<Collider2D>();

            //States Init
            {
                //Add States
                IdleState idleState = new IdleState(this, agentData);
                MoveState moveState = new MoveState(this, agentData);
                SeekTargetState seekTargetState = new SeekTargetState(this, agentData);   
                AttackState attackState = new AttackState(this, agentData);

                //Add Transitions
                _stateMachine.AddTransition(idleState, moveState, HasGameStarted);
                _stateMachine.AddTransition(idleState, seekTargetState, testCondition);
                _stateMachine.AddTransition(idleState, attackState, testCondition);
                _stateMachine.AddTransition(moveState, seekTargetState, testCondition);
                _stateMachine.AddTransition(seekTargetState, attackState, testCondition);

                _stateMachine.AddAnyTransition(idleState, testCondition);

                //Default Starting State
                _stateMachine.setState(idleState);
            }
        }

        // Update is called once per frame
        void Update()
        {
            _stateMachine.Update();

            sCurrentState =  _stateMachine.GetCurrentState().ToString();
            //Debug.Log("Get Move Speed: " + agentData.GetMoveSpeed());
        }

        public void Move(Vector2 position, float spd)
        {
            transform.up = position;
            transform.position += (Vector3)position * spd * Time.deltaTime;
        }

    }
}

