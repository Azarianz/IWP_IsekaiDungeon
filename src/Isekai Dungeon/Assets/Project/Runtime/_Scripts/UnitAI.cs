using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitAI : MonoBehaviour
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
    private float attackDistanceThreshold = 0.8f, chaseDistanceThreshold = 3.0f;

    [SerializeField]
    private bool teamHuman, teamEnemy;

    [SerializeField]
    private Health HEALTH;

    public UnityEvent<Vector3> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    // TO MOVE TO SCRIPTABLE OBJECT
    public int STAT_HP;
    public int STAT_DMG;

    bool follow = false;

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
        //old code
        {
            //if (follow)
            //{
            //    // Target Locked
            //    if (target != null)
            //    {
            //        // Distance between enemy and player
            //        float distance = Vector3.Distance(target.position, transform.position);
            //        OnPointerInput?.Invoke(target.position);

            //        if (distance <= chaseDistanceThreshold)
            //        {
            //            if (distance <= attackDistanceThreshold)
            //            {
            //                //Attack Behaviour
            //                OnMovementInput?.Invoke(Vector3.zero);
            //                if (passedTime >= attackDelay)
            //                {
            //                    passedTime = 0;
            //                    OnAttack?.Invoke();
            //                }
            //            }
            //            else
            //            {
            //                //Chasing the player
            //                Vector3 direction = target.position - transform.position;
            //                OnMovementInput?.Invoke(direction.normalized);
            //            }
            //        }
            //    }

            //    // Continue moving
            //    else
            //    {
            //        Vector3 direction = (transform.position + Vector3.forward) - transform.position;
            //        OnMovementInput?.Invoke(direction.normalized);

            //        // Create a sphere around this object with the specified radius
            //        Collider[] colliders = Physics.OverlapSphere(transform.position, chaseDistanceThreshold);
            //        Collider nearestCol = null;
            //        float bestDistance = 999f;

            //        // Loop through all the colliders in the sphere
            //        foreach (Collider collider in colliders)
            //        {
            //            float distance = Vector3.Distance(transform.position, collider.transform.position);

            //            // Check if the collider belongs to an object with the enemy tag
            //            if (collider.CompareTag("Enemy"))
            //            {
            //                //Check for first closest enemy to lock onto
            //                if (distance < bestDistance)
            //                {
            //                    bestDistance = distance;
            //                    nearestCol = collider;
            //                }
            //            }
            //        }

            //        target = nearestCol.transform;
            //    }


            //    //Idle
            //    if (passedTime < attackDelay)
            //    {
            //        passedTime += Time.deltaTime;
            //    }
            //}
        }

        if (aiData.currentTarget != null)
        {
            //Looking at the Target
            OnPointerInput?.Invoke(aiData.currentTarget.position);
            if (following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            //float closestDist = 99f;
            //Transform nearestTarget = null;

            ////Target acquisition logic
            //foreach (Transform target in aiData.targets)
            //{
            //    float distance = Vector3.Distance(target.position, transform.position);

            //    if (distance < closestDist)
            //    {
            //        closestDist = distance;
            //        nearestTarget = target;
            //    }
            //}

            //aiData.currentTarget = nearestTarget;
            ////Chase current target
            //OnMovementInput?.Invoke(aiData.currentTarget.position);
            aiData.currentTarget = aiData.targets[0];
        }

        else if (aiData.currentTarget == null)
        {
            Vector3 direction = Vector3.zero;

            if(teamHuman)
            {
                direction = (transform.position + Vector3.forward) - transform.position;
            }
            if (teamEnemy)
            {
                direction = (transform.position + Vector3.back) - transform.position;
            }

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
            //Debug.Log("Target: " + target);
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
        if (aiData.currentTarget == null)
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

            if (distance < attackDistanceThreshold)
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
