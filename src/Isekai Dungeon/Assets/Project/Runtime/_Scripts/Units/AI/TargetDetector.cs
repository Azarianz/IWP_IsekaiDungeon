using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField]
    private float targetDetectionRange = 6;

    [SerializeField]
    private LayerMask obstaclesLayerMask, enemyLayerMask;

    [SerializeField]
    private bool showGizmos = false;

    private List<Transform> colliders;

    public override void Detect(AIData aiData)
    {
        //Find out if player is near
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, targetDetectionRange, enemyLayerMask);

        if(enemyColliders != null)
        {
            colliders = new List<Transform>();

            foreach (Collider col in enemyColliders)
            {
                //Check if unit can see an enemy
                Vector3 direction = (col.transform.position - transform.position).normalized;
                Ray ray = new Ray(transform.position, direction);

                if (Physics.Raycast(ray, out RaycastHit hit, targetDetectionRange, obstaclesLayerMask))
                {
                    //Make sure that the collider it sees is on the "Enemy" layer
                    if (hit.collider != null && (enemyLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
                    {
                        //Check distance between this unit to the target
                        float distance = Vector3.Distance(transform.position, col.transform.position);

                        Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                        colliders.Add(col.transform);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

        }
        else
        {
            //Unit doesn't see an enemy
            colliders = null;
        }

        aiData.targets = colliders;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if (colliders == null)
            return;

        Gizmos.color = Color.magenta;
        foreach(var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
