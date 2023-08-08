using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Class")]
public class ClassSystem : CharacterClass
{
    [HideInInspector]
    [SerializeField]
    public List<String> Abilities;  // for json enemy loading only

    public ClassSystem() { }
    public ClassSystem(ClassSystem import)
    {
        attackRange = import.attackRange;
        Abilities = import.Abilities;
    }

    public float GetAttackRange()
    {
        return (float)attackRange;
    }
}
