using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, maintain current allignment
        if (context.Count == 0)
            return Vector2.zero;

        {
            //add all points together and average
            Vector2 avoidanceMove = Vector2.zero;

            int nAvoid = 0;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                Vector2 closestPoint = item.gameObject.GetComponent<Collider2D>().ClosestPoint(agent.transform.position);

                if(Vector2.SqrMagnitude((Vector3)closestPoint - agent.transform.position) < flock.SquareAvoidanceRadius)
                {
                    nAvoid++;
                    avoidanceMove += (Vector2)(agent.transform.position - item.position);
                }
            }

            if (nAvoid > 0)
                avoidanceMove /= nAvoid;

            return avoidanceMove;
        }

    }
}
