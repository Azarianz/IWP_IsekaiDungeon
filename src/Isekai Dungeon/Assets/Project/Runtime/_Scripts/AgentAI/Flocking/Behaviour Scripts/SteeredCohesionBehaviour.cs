using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public float speedMultiplier = 1f; // Speed multiplier based on the distance


    public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustments
        if (context.Count == 0)
            return Vector2.zero;

        //Find middle point between agents and set their destination there
        {
            //add all points together and average
            Vector2 cohesionMove = Vector2.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                float distance = Vector2.Distance(agent.transform.position, item.position);

                if (distance < flock.cohesionRange)
                {                   
                    flock.DebugDrawOverlapCircle(agent.transform.position, Color.yellow, flock.cohesionRange);
                    cohesionMove += (Vector2)item.transform.position;                   
                }

            }

            cohesionMove /= context.Count;

            //create offset from agent position
            cohesionMove -= (Vector2)agent.transform.position;

            // Speed adjustment based on distance
            float distanceMultiplier = Mathf.Clamp01(cohesionMove.magnitude / flock.cohesionRange);
            float adjustedSpeed = agent.moveSpeed * (1f + speedMultiplier * distanceMultiplier);
            cohesionMove = cohesionMove.normalized * adjustedSpeed;

            return cohesionMove;
        }

    }

}
