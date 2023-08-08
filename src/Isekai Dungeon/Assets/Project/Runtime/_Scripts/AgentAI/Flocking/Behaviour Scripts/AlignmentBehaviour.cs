using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    Vector2 currentVelocity;

    public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, maintain current allignment
        if (context.Count == 0)
            return agent.transform.up;
        {
            //add all points together and average
            Vector2 alignmentMove = Vector2.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

            foreach (Transform item in filteredContext)
            {
                float distance = Vector2.Distance(agent.transform.position, item.position);
                if (distance < flock.alignmentRange)
                {
                    //Debug.Log("Agent in alignment range!");
                    flock.DebugDrawOverlapCircle(agent.transform.position, Color.cyan, flock.alignmentRange);
                    //Debug.Log("Avoidance: " + flock.SquareAvoidanceRadius);
                    //Debug.Log("Allignment Distance: " + distance);
                    alignmentMove += (Vector2)item.transform.up;             
                }
            }

            alignmentMove /= context.Count;
            alignmentMove = Vector2.SmoothDamp(agent.transform.up, alignmentMove, ref currentVelocity, flock.alignmentSmoothTime);

            return alignmentMove;
        }

    }
}
