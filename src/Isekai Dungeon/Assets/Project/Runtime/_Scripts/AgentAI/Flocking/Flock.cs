using AI;
using FSM.STATES;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class Flock : MonoBehaviour
{
    public MultipleTargetCamera cam;

    public FlockBehavior moveBehaviour;

    [SerializeField]
    List<Agent_AI> agents = new List<Agent_AI>();

    [Range(1f, 100f)]
    public float acceleration = 60f;
    [Range(1f, 5f)]
    public float maxSpeed = 5f;
    [Range(0f, 5f)]
    public float avoidanceRange = 1.5f;
    [Range(0f, 10f)]
    public float avoidanceStrength = 1.5f;
    [Range(0f, 5f)]
    public float cohesionRange = 1.5f;
    [Range(0f, 5f)]
    public float alignmentRange = 1.5f;

    [Range(1f, 100f)]
    public float detectionRadius = 30.0f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    public float SquareNeighbourRadius { get { return squareNeighbourRadius; } }

    [Header("Debug Draw Settings")]
    public int segments = 16;
    private bool isInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = avoidanceRange * avoidanceRange;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceStrength * avoidanceStrength;

        agents = GetComponentsInChildren<Agent_AI>().ToList();

        foreach (Agent_AI agent in agents)
        {
            AddAgentsToTrack(agent.gameObject);
            agent.AssignFlock(this);
        }

        isInitialized = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (agents.Count > 0)
        {
            foreach (Agent_AI agent in agents)
            {
                if (agent == null)
                {
                    if(cam != null)
                    {
                        cam.targets.RemoveAll(target => target == null);
                    }

                    agents.RemoveAll(item => item == null);
                    break;
                }

                List<Transform> context = GetNearbyObjects(agent);

                // Calculate the move using the assigned FlockBehavior
                Vector3 move = Vector3.zero;
                move = moveBehaviour.CalculateMove(agent, context, this);

                Vector3 agentVelocity = agent.AgentRigidBody.velocity;
                agentVelocity = Vector3.Lerp(agentVelocity, move, acceleration * Time.fixedDeltaTime);

                //If greater than max speed
                if (agentVelocity.sqrMagnitude > squareMaxSpeed)
                {
                    agentVelocity = agentVelocity.normalized * maxSpeed; //clamp at max speed
                }

                agent.Move(agentVelocity);
                Debug.DrawLine(agent.transform.position, move, Color.cyan);
            }
        }
    }

    List<Transform> GetNearbyObjects(Agent_AI agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, detectionRadius);
        foreach(Collider2D c in contextColliders){
            if (c != agent.AgentCollider){
                context.Add(c.transform);
            }
        }

        return context;
    }

    public void DebugDrawOverlapCircle(Vector2 position, Color color, float radius)
    {
        // Calculate the angle between each segment
        float angleIncrement = 360f / segments;

        // Store the start and end points of each segment
        Vector2 startPoint = Vector2.zero;
        Vector2 endPoint = Vector2.zero;

        // Draw the wire circle segment by segment
        for (int i = 0; i < segments; i++)
        {
            // Calculate the angle of the current segment
            float angle = i * angleIncrement;

            // Calculate the start and end points of the segment
            startPoint.x = position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            startPoint.y = position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            angle += angleIncrement;

            endPoint.x = position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            endPoint.y = position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            // Draw a debug line between the start and end points
            Debug.DrawLine(startPoint, endPoint, color);
        }
    }

    public void AddAgentToFlock(Agent_AI agent)
    {
        if(agent.AgentFlock == null)
        {
            agents.Add(agent);
            agent.AssignFlock(this);
        }
        else
        {
            Debug.Log("Agent: " + agent + " is already in flock: " + agents);
        }
    }

    public void AddAgentsToTrack(GameObject agent)
    {
        //TO CHANGE LATER
        if (cam != null && !cam.targets.Contains(agent.gameObject.transform))
        {
            cam.targets.Add(agent.gameObject.transform);
        }
    }

    public bool IsFlockDead()
    {
        if(isInitialized)
        {
            return agents.Count <= 0;
        }
        else
        {
            return false;
        }

    }
}
