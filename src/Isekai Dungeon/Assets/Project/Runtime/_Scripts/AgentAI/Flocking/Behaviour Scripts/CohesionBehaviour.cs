using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public float cohesionRange = 1.5f;

    public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustments
        if(context.Count == 0)
            return Vector2.zero;

        //Find middle point between agents and set their destination there
        {
            //add all points together and average
            Vector2 cohesionMove = Vector2.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                Vector2 closestPoint = item.gameObject.GetComponent<Collider2D>().ClosestPoint(agent.transform.position);

                if (Vector2.SqrMagnitude((Vector3)closestPoint - agent.transform.position) < flock.SquareAvoidanceRadius)
                {
                    //Debug.Log("Closest Point: " + closestPoint);
                    //Debug.Log("Avoidance: " + flock.SquareAvoidanceRadius);

                    cohesionMove += (Vector2)item.transform.position;
                }

                cohesionMove /= context.Count;

                //create offset from agent position
                cohesionMove -= (Vector2)agent.transform.position;
            }

            return cohesionMove;
        }

    }

}
