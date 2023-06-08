using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeFlockBehaviour : FilteredFlockBehaviour
{
    public FlockBehavior[] behaviors; // Array of individual flocking behaviors
    public float[] weights; // Weights corresponding to each behavior

    // Group Movement
    public Vector2 groupMoveVector; // The direction in which the entire flock should move

    // Pathfinding
    public Transform pathfindingOrigin; // The origin point for pathfinding
    public float pathfindingWeight; // Weight for the pathfinding behavior

    public override Vector2 CalculateMove(Agent_AI agent, List<Transform> context, Flock flock)
    {
        // Initialize the move vector
        Vector2 move = Vector2.zero;

        // Apply group movement
        move += groupMoveVector;

        // Apply individual flocking behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];
            move += partialMove;
        }

        // Apply pathfinding behavior
        if (pathfindingOrigin != null)
        {
            Vector2 pathfindingMove = (pathfindingOrigin.position - agent.transform.position).normalized * pathfindingWeight;
            move += pathfindingMove;
        }

        return move;
    }
}
