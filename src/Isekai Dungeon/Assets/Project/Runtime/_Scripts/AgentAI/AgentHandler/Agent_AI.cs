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
        public Agent_AI(Agent_Data data)
        {
            agentData = data;
        }

        //Variables
        [SerializeField]
        private Agent_Data agentData;
        public Agent_Data AgentData { get { return agentData; } set { agentData = value; } }

        private StateMachine _stateMachine;
        public StateMachine AgentStateMachine { get { return _stateMachine; } }
        public string sCurrentState;    //For Debug

        [SerializeField]
        private Flock agentFlock;
        public Flock AgentFlock { get { return agentFlock; } set { agentFlock = value; } }

        private Collider2D agentCollider;
        public Collider2D AgentCollider { get { return agentCollider; } }

        private Rigidbody2D agentRigidbody;
        public Rigidbody2D AgentRigidBody { get { return agentRigidbody; } }

        [SerializeField]
        private Agent_AI currentTarget;
        public Agent_AI Agent_Target { get { return currentTarget; } set { currentTarget = value; } }

        private Agent_Animator agentAnimator;
        public Agent_Animator Agent_Animator { get { return agentAnimator; } set { agentAnimator = value; } }

        public UnityEvent OnIdle, OnMove, OnAttack, OnShoot, OnCastSkill1, OnCastSkill2;

        [SerializeField]
        public Slider healthBar;

        public float moveSpeed = 4.5f;
        public float knockbackForce = 10f;
        public GameObject damagePopupText;

        //Function Bools
        private bool IsIdle() { return currentTarget == null && agentRigidbody.velocity.magnitude <= 0
                                  || DungeonManager.instance.hasDungeonEnded; }
        public bool IsAttacking(bool condition) { _isAttacking = condition; return _isAttacking; }
        public bool IsAttacking() { return _isAttacking && currentTarget != null && currentTarget.enabled; }
        public bool IsChasing() { return currentTarget != null && agentRigidbody.velocity.magnitude > 0.01f; }
        private bool IsMove() { return currentTarget == null && agentRigidbody.velocity.magnitude > 0.01f; }

        bool _isAttacking = false, isInitialized;

        public BaseStats baseStat;
        public ClassSystem charClass; //for enemy

        public void SetUnitData(Agent_Data data)
        {
            agentData = data;
            Initialize();
        }

        public void Initialize()
        {
            _stateMachine = new StateMachine();
            agentCollider = GetComponent<Collider2D>();
            agentRigidbody = GetComponent<Rigidbody2D>();
            agentAnimator = GetComponentInChildren<Agent_Animator>();
            agentData.AgentAnimator = agentAnimator;

            if (agentAnimator != null)
            {
                agentAnimator.Initialize(agentData.unit_icon, agentData.animationSet);
            }

            if (agentData != null && agentData.GetAgentStats() != null)
            {

                if (healthBar != null)
                {
                    float maxH = agentData.GetAgentStats().STAT_HEALTH = agentData.GetAgentStats().STAT_MAXHEALTH;              
                    healthBar.value = healthBar.maxValue = maxH;
                    UpdateSliderWidth(maxH);
                }
            }

            // States Init
            {
                // Add States
                IdleState idleState = new IdleState(this, agentData);
                MoveState moveState = new MoveState(this, agentData);
                SeekTargetState chaseState = new SeekTargetState(this, agentData);
                AttackState attackState = new AttackState(this, agentData);

                // Add Transitions
                if (_stateMachine != null)
                {
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

                    // Default Starting State
                    _stateMachine.setState(idleState);
                }
            }

            isInitialized = true;
        }


        // Update is called once per frame
        void Update()
        {
            if (!agentAnimator.GetAnimBool("Death"))
            {
                _stateMachine.Update();

                sCurrentState = _stateMachine.GetCurrentState().ToString();
            }
        }

        public void Move(Vector2 velocity)
        {
            if (GameManager.Instance.State == GameState.GameStart
                && GameManager.Instance.State < GameState.Win)
            {
                agentRigidbody.velocity = velocity * moveSpeed;

                Vector2 dir = (transform.position + (Vector3)velocity) - transform.position;

                if(!IsAttacking())
                {
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

            }
        }

        public void AssignFlock(Flock flock)
        {
            agentFlock = flock;
        }

        public List<Transform> GetNearbyObjects()
        {
            List<Transform> context = new List<Transform>();
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, 30.0f);
            foreach (Collider2D c in contextColliders)
            {
                if (c != AgentCollider)
                {
                    context.Add(c.transform);
                }
            }

            return context;
        }

        public List<Agent_AI> GetNearbyTeam(float range)
        {
            List<Agent_AI> context = new List<Agent_AI>();
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (Collider2D c in contextColliders)
            {
                Agent_AI neighbour = c.GetComponent<Agent_AI>();
                if(neighbour != null && !neighbour.AgentFlock.IsEnemy)
                {
                    context.Add(neighbour);
                }
            }

            return context;
        }

        public List<Agent_AI> GetNearbyEnemy(float range)
        {
            List<Agent_AI> context = new List<Agent_AI>();
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (Collider2D c in contextColliders)
            {
                Agent_AI neighbour = c.GetComponent<Agent_AI>();
                if (neighbour != null && neighbour.AgentFlock.IsEnemy)
                {
                    context.Add(neighbour);
                }
            }

            return context;
        }

        public void DeliverDamage()
        {
            if (currentTarget == null)
                return;

            // Damage
            float targetHealth = currentTarget.AgentData.GetAgentStats().STAT_HEALTH;
            float finalDmg = AgentData.GetAgentStats().CalculateDamage();

            if(agentFlock.IsEnemy)
            {
                SpawnTextValue(finalDmg, Color.red, currentTarget.transform.position);
            }
            else
            {
                SpawnTextValue(finalDmg, Color.white, currentTarget.transform.position);
            }
            

            if (targetHealth <= finalDmg && !agentFlock.IsEnemy)
            {
                int xpReward = currentTarget.agentData.getRewardXP;
                //Debug.Log("XP Reward: " + xpReward);
                agentData.GetLevelSystem().AddExperience(xpReward);
                DungeonManager.instance.expEarned += xpReward;

                int goldReward = currentTarget.agentData.getRewardGold;
                //Debug.Log("Gold Reward:" + goldReward);
                InventoryController.Inventory_Instance.AddGold(goldReward);
                DungeonManager.instance.goldEarned += goldReward;
                DungeonManager.instance.KillCount++;
            }

            currentTarget.ReceiveDamage(finalDmg);
            currentTarget = null;
        }

        public void DeliverDamage(Agent_AI target)
        {
            // Damage
            float targetHealth = target.AgentData.GetAgentStats().STAT_HEALTH;
            float finalDmg = AgentData.GetAgentStats().CalculateDamage();


            GameObject go = Instantiate(damagePopupText, target.transform.position, Quaternion.identity);
            go.GetComponent<TextMeshPro>().text = finalDmg.ToString("F1");
            Destroy(go, 1.5f);

            if (targetHealth <= finalDmg && !agentFlock.IsEnemy)
            {
                int currentLvl = agentData.GetAgentLevel();

                int xpReward = target.agentData.getRewardXP;
                Debug.Log("XP Reward: " + xpReward);
                AgentData.GetLevelSystem().AddExperience(xpReward);
                DungeonManager.instance.expEarned += xpReward;

                int goldReward = target.agentData.getRewardGold;
                Debug.Log("Gold Reward:" + goldReward);
                InventoryController.Inventory_Instance.AddGold(goldReward);
                DungeonManager.instance.goldEarned += goldReward;
                DungeonManager.instance.KillCount++;

                UpdateHealthBar();
            }

            target.ReceiveDamage(finalDmg);
        }

        public void ReceiveDamage(float val)
        {
            agentData.GetAgentStats().ReceiveDamage(val);
            agentData.GetAgentAnimator().AnimateFlash();
            UpdateHealthBar();
        }

        public void ReceiveHeal(float val)
        {
            SpawnTextValue(val, Color.green, transform.position);

            agentData.GetAgentStats().ReceiveHeal(val);
            agentData.GetAgentAnimator().AnimateColorFlash(Color.green);
            UpdateHealthBar();
        }

        public void UpdateHealthBar()
        {
            healthBar.value = agentData.GetAgentStats().STAT_HEALTH;

            if(healthBar.value <= 0)
            {
                if(!agentFlock.IsEnemy)
                {
                    DungeonManager.instance.DeathCount++;
                }

                if(AgentData.GetAgentStats().UNIT_RACE != "Hero")   //Don't Kill Hero
                {
                    InventoryController.Inventory_Instance.RemoveUnit(agentData);
                }

                agentAnimator.Animate_Death();
            }
            else
            {
                currentTarget.agentAnimator.Animate_Hit();
            }
        }

        public void UpdateSliderWidth(float currentHealth, int maxHealth = 2000, float minWidth = 4.0f, float maxWidth = 18.0f)
        {
            float normalizedHealth = currentHealth / maxHealth;

            if(normalizedHealth < 0)
            {
                normalizedHealth = baseStat.BASE_HEALTH;
            }

            float targetWidth = normalizedHealth * maxWidth;

            // Check if the target width is below the minimum width
            if (targetWidth < minWidth)
            {
                targetWidth = minWidth;
            }

            else if(targetWidth > maxWidth)
            {
                targetWidth = maxWidth;
            }

            // Set the width of the Slider's RectTransform
            RectTransform rectTransform = healthBar.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(targetWidth, rectTransform.sizeDelta.y);
        }

        public void SpawnTextValue(float val, Color color, Vector3 target)
        {
            GameObject go = Instantiate(damagePopupText, target, Quaternion.identity);
            TextMeshPro txtGO = go.GetComponent<TextMeshPro>();

            txtGO.text = val.ToString("F1");
            txtGO.color = color;

            Destroy(go, 1.5f);
        }
    }
}

