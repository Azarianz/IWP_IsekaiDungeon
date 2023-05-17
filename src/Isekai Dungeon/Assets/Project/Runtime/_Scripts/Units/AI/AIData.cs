using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    public List<Transform> targets = null;
    public Collider[] obstacles = null;

    public Transform currentTarget;

    //To choose which target is the best to attack
    public int GetTargetsCount() => targets == null ? 0 : targets.Count;    
}
