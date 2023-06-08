using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using FSM.STATES;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using AI.STATS;

namespace AI
{
    [RequireComponent(typeof(Collider2D))]
    public class Agent_AI : MonoBehaviour
    {
        public BaseStats baseStat;
        public ClassSystem charClass;

        //Variables
        [SerializeField]
        private Agent_Data agentData;
        public Agent_Data AgentData { get { return agentData; } }

        private StateMachine _stateMachine;
        public StateMachine AgentStateMachine { get { return _stateMachine; } }
        public string sCurrentState;    //For Debug

        [SerializeField]
        private Flock agentFlock;
        public Flock AgentFlock { get { return agentFlock; } }

        private Collider2D agentCollider;
        public Collider2D AgentCollider { get { return agentCollider; } }

        private Rigidbody2D agentRigidbody;
        public Rigidbody2D AgentRigidBody { get { return agentRigidbody; } }

        [SerializeField]
        private Agent_AI currentTarget;
        public Agent_AI Agent_Target { get { return currentTarget; } set { currentTarget = value; } }

        private Agent_Animator agentAnimator;
        public Agent_Animator Agent_Animator { get; set; }

        public UnityEvent OnIdle, OnMove, OnAttack;

        [SerializeField]
        public Slider healthBar;

        [Range(0f, 100f)]
        public float AttackRange = 20.0f;
        public float moveSpeed = 1f;
        public float knockbackForce = 10f;
        public GameObject damagePopupText;

        //Function Bools
        private bool IsIdle() { return currentTarget == null && agentRigidbody.velocity.magnitude <= 0; }
        public bool IsAttacking(bool condition) { _isAttacking = condition; return _isAttacking; }
        public bool IsAttacking() { return _isAttacking && currentTarget != null; }
        public bool IsChasing() { return currentTarget != null && agentRigidbody.velocity.magnitude > 0.01f; }
        private bool IsMove() { return currentTarget == null && agentRigidbody.velocity.magnitude > 0.01f; }

        bool _isAttacking;

        // Awake is called when the script is loaded
        private void Awake()
        {
            agentData = new Agent_Data(baseStat, charClass);

            _stateMachine = new StateMachine();
            agentCollider = GetComponent<Collider2D>();
            agentRigidbody = GetComponent<Rigidbody2D>();
            agentAnimator = GetComponentInChildren<Agent_Animator>();

            //States Init
            {
                //Add States
                IdleState idleState = new IdleState(this, agentData);
                MoveState moveState = new MoveState(this, agentData);
                SeekTargetState chaseState = new SeekTargetState(this, agentData);   
                AttackState attackState = new AttackState(this, agentData);

                //Add Transitions
                _stateMachine.AddTransition(idleState, moveState, IsMove);
                _stateMachine.AddTransition(idleState, chaseState, IsChasing);
                _stateMachine.AddTransition(idleState, attackState, IsAttacking);

                _stateMachine.AddTransition(moveState, chaseState, IsChasing);
                _stateMachine.AddTransition(moveState, attackState, IsAttacking);

                _stateMachine.AddTransition(chaseState, attackState, IsAttacking);
                _stateMachine.AddTransition(chaseState, moveState, IsMove);

                _stateMachine.AddTransition(attackState, moveState, IsMove);
                _stateMachine.AddTransition(attackState, chaseState, IsChasing);

                _stateMachine.AddAnyTransition(idleState, IsIdle);

                //Default Starting State
                _stateMachine.setState(idleState);
            }

            StartCoroutine(getStats());
        }

        IEnumerator getStats()
        {
            yield return null;

            agentData = new Agent_Data(baseStat, charClass);
            if (agentData != null && agentData.GetAgentStats() != null)
            {
                moveSpeed = agentData.GetAgentStats().STAT_SPEED / 1.5f;
                healthBar.value = healthBar.maxValue = agentData.GetAgentStats().STAT_MAXHEALTH;
            }


        }

        // Update is called once per frame
        void Update()
        {
            _stateMachine.Update();

            sCurrentState =  _stateMachine.GetCurrentState().ToString();
            //Debug.Log("Get Move Speed: " + agentData.GetMoveSpeed());
        }

        public void Move(Vector2 velocity)
        {
            if (GameManager.Instance.State == GameState.GameStart
                && GameManager.Instance.State < GameState.Win)
            {
                agentRigidbody.velocity = velocity * moveSpeed;

                if(IsAttacking())
                {
                    Vector2 dir = currentTarget.transform.position - transform.position;

                    if (Mathf.Sign(dir.x) > 0)  // Positive X velocity
                    {
                        // Flip the sprite to face right
                        agentAnimator.FlipSprite(false);
                    }
                    else if (Mathf.Sign(dir.x) < 0)  // Negative X velocity
                    {
                        // Flip the sprite to face left
                        agentAnimator.FlipSprite(true);
                    }
                }
                else
                {
                    if(gameObject.tag == "Enemy")
                    {
                        agentAnimator.FlipSprite(true);
                    }

                    else
                    {
                        agentAnimator.FlipSprite(false);
                    }
                }
                
            }
        }

        public void AssignFlock(Flock flock)
        {
            agentFlock = flock;
        }

        public void DeliverDamage()
        {
            if (currentTarget == null)
                return;

            // Damage
            float finalDmg = AgentData.GetAgentStats().CalculateDamage();

            GameObject go = Instantiate(damagePopupText, currentTarget.transform.position, Quaternion.identity);
            go.GetComponent<TextMeshPro>().text = finalDmg.ToString("F1");
            Destroy(go, 1.5f);

            currentTarget.agentData.GetAgentStats().ReceiveDamage(finalDmg);
            currentTarget.UpdateHealthBar();
        }

        public void UpdateHealthBar()
        {
            healthBar.value = agentData.GetAgentStats().STAT_HEALTH;

            if(healthBar.value <= 0)
            {
                if(gameObject.tag == "Enemy")
                {
                    InventoryController.Inventory_Instance.RewardGold(30);
                }

                Destroy(gameObject);
            }
        }

    }
}

