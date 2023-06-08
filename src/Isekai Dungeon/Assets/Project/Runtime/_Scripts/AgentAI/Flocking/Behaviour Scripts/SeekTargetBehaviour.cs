using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Flock/Behaviour/Seek Target Behaviour")]

public class SeekTargetBehaviour : FilteredFlockBehaviour
{
    //public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    //{
    //    if (context.Count == 0 && agent.IsAttacking())
    //        return Vector2.zero;    // No context, break

    //    List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
    //    float closestDistance = flock.detectionRadius;
    //    float attackRange = agent.AttackRange;
    //    Agent_AI target = null;
    //    Vector2 target_pos = Vector2.zero;
    //    Vector2 move = Vector2.zero;

    //    foreach (Transform item in filteredContext)
    //    {
    //        float distance = Vector2.Distance(agent.transform.position, item.position);

    //        if (distance < closestDistance)
    //        {
    //            closestDistance = distance;
    //        }

    //        target = item.GetComponent<Agent_AI>();
    //        target_pos = item.position;
    //    }

    //    if(target != null)
    //    {
    //        agent.Agent_Target = target;
    //        Vector2 direction = target_pos - (Vector2)agent.transform.position;
    //        move += direction;

    //        float squaredDistance = 9999;

    //        if (target_pos != Vector2.zero)
    //        {
    //            squaredDistance = ((Vector3)target_pos - agent.transform.position).sqrMagnitude;
    //        }

    //        //Debug.Log(squaredDistance);

    //        if (squaredDistance < attackRange)
    //        {
    //            agent.IsAttacking(true);
    //            //Set available target for attack
    //            return Vector2.zero;
    //        }
    //    }
    //    else
    //    {
    //        agent.Agent_Target = null;
    //        agent.IsAttacking(false);
    //    }

    //    return move;
    //}
    public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0 && agent.IsAttacking())
            return Vector2.zero;    // No context, break

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        float attackRange = agent.AttackRange;
        Agent_AI target = null;
        Vector2 targetPos = Vector2.zero;
        Vector2 move = Vector2.zero;

        // Shuffle the context list randomly
        ShuffleList(filteredContext);

        foreach (Transform item in filteredContext)
        {
            target = item.GetComponent<Agent_AI>();
            targetPos = item.position;

            Vector2 direction = targetPos - (Vector2)agent.transform.position;
            move += direction;

            float squaredDistance = ((Vector3)targetPos - agent.transform.position).sqrMagnitude;

            if (squaredDistance < attackRange)
            {
                agent.Agent_Target = target;
                agent.IsAttacking(true);
                return Vector2.zero;  // Stop moving, within attack range
            }
        }

        // No targets within attack range
        agent.Agent_Target = null;
        agent.IsAttacking(false);

        return move;
    }

    private void ShuffleList<T>(List<T> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            int randIndex = Random.Range(i, count);
            T temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }
}
