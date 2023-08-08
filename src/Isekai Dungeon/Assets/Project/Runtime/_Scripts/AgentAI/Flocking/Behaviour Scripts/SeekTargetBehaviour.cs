using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Seek Target Behaviour")]

public class SeekTargetBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
            return Vector2.zero;    // No context, break

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        float attackRange = agent.AgentData.getAttackRange;
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
