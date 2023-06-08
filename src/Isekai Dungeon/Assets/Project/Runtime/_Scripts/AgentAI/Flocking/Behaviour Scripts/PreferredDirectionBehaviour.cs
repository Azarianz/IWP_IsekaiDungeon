using AI;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Preferred Direction")]

public class PreferredDirectionBehaviour : FlockBehavior
{
    public Vector2 preferredDirection;

    public FlockBehavior[] behaviours;
    public float[] weights;

    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    {
        //handle data mismatch
        if (weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        //set up move
        Vector2 move = Vector2.zero;

        if (!agent.IsAttacking())
        {
            Vector2 prefDir = ((Vector2)agent.transform.position + preferredDirection) - (Vector2)agent.transform.position;
            move += prefDir.normalized;
        }

        //iterate through behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        if(agent.IsAttacking())
        {
            move = Vector2.zero;
        }

        return move;
    }
}
