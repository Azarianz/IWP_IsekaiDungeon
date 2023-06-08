using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public float avoidanceRange = 1.5f;
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    //Seperation
    public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
            return Vector2.zero;    // No neighbors, no separation force needed
        {
            Vector2 avoidanceMove = Vector2.zero;
            int nAvoid = 0;

            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                Vector2 closestPoint = item.gameObject.GetComponent<Collider2D>().ClosestPoint(agent.transform.position);
                float closestDistance = Vector2.SqrMagnitude((Vector3)closestPoint - agent.transform.position);

                if (closestDistance < flock.SquareNeighbourRadius)
                {
                    flock.DebugDrawOverlapCircle(agent.transform.position, Color.green, flock.SquareNeighbourRadius);
                    flock.DebugDrawOverlapCircle(closestPoint, Color.white, 0.1f);

                    // Calculate the avoidance force based on the distance and relative velocity
                    Vector2 avoidanceForce = (Vector2)(agent.transform.position - item.position);
                    float distance = avoidanceForce.magnitude;

                    // Scale the avoidance force based on distance and relative velocity
                    avoidanceForce *= Mathf.Clamp01(avoidanceRange - distance) / distance;

                    // Apply avoidance force
                    avoidanceMove += avoidanceForce;
                    nAvoid++;
                }
            }

            if (nAvoid > 0)
            {
                avoidanceMove /= nAvoid;
            }

            return avoidanceMove;
        }

    }
}
