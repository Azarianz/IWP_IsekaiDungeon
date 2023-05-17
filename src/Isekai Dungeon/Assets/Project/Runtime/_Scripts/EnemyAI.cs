using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private Vector3 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    bool following = false;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1, passedTime = 1;

    [SerializeField]
    private float attackDistanceThreshold = 0.5f, chaseDistanceThreshold = 3.0f;

    [SerializeField]
    private Health HEALTH;

    public UnityEvent<Vector3> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    // TO MOVE TO SCRIPTABLE OBJECT
    public int STAT_HP;
    public int STAT_DMG;

    private void Awake()
    {
        HEALTH = GetComponent<Health>();
        HEALTH.InitializeHealth(STAT_HP);
    }

    private void Start()
    {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void Update()
    {
        //Old Code(TODO: Translate to new)
        {
            // Target Locked
            //if (target != null)
            //{
            //    // Distance between this and target
            //    float distance = Vector3.Distance(target.position, transform.position);
            //    OnPointerInput?.Invoke(target.position);

            //    if(distance <= chaseDistanceThreshold)
            //    {
            //        if (distance <= attackDistanceThreshold)
            //        {
            //            //Attack Behaviour
            //            OnMovementInput?.Invoke(Vector3.zero);
            //            if (passedTime >= attackDelay)
            //            {
            //                passedTime = 0;
            //                OnAttack?.Invoke();
            //            }
            //        }
            //        else
            //        {
            //            //Chasing the player
            //            Vector3 direction = target.position - transform.position;
            //            OnMovementInput?.Invoke(direction.normalized);
            //        }
            //    }
            //}

            //// Continue moving
            //else
            //{
            //    Vector3 direction = (transform.position + Vector3.back) - transform.position;
            //    OnMovementInput?.Invoke(direction.normalized);

            //    // Create a sphere around this object with the specified radius
            //    Collider[] colliders = Physics.OverlapSphere(transform.position, chaseDistanceThreshold);

            //    // Loop through all the colliders in the sphere
            //    foreach (Collider collider in colliders)
            //    {
            //        // Check if the collider belongs to an object with the enemy tag
            //        if (collider.CompareTag("Player"))
            //        {
            //            //Debug.Log("Enemy detected!");
            //            target = collider.transform;
            //        }
            //    }
            //}
        }

        if (aiData.currentTarget != null)
        {
            //Looking at the Target
            OnPointerInput?.Invoke(aiData.currentTarget.position);
            if(following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if(aiData.GetTargetsCount() > 0)
        {
            //Target acquisition logic
            aiData.currentTarget = aiData.targets[0];
        }
        else if (aiData.currentTarget == null)
        {
            Vector3 direction = (transform.position + Vector3.back) - transform.position;
            movementInput = direction;
            OnMovementInput?.Invoke(direction);
        }


        //Idle
        if (passedTime < attackDelay)
        {
            passedTime += Time.deltaTime;
        }

    }

    public void SendDmg()
    {
        Health health;
        if (health = aiData.currentTarget.GetComponent<Health>())
        {
            health.GetHit(STAT_DMG, gameObject);
        }
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private IEnumerator ChaseAndAttack()
    {
        if(aiData.currentTarget == null)
        {
            //Stopping Logic
            Debug.Log("Stopping");
            movementInput = Vector3.zero;
            following = false;
            yield return null;
        }
        else
        {
            float distance = Vector3.Distance(aiData.currentTarget.position, transform.position);

            if(distance < attackDistanceThreshold)
            {
                //Attack logic
                movementInput = Vector3.zero;
                OnAttack?.Invoke();
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //Chase logic
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);

                //Moving the Agent
                OnMovementInput?.Invoke(movementInput);

                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }

        }
    }
}
