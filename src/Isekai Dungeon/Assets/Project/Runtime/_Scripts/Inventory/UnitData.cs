using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.STATS;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public string name;
    public Sprite icon;
    public BaseStats unitBase;
}
