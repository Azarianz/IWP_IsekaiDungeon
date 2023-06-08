using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : ScriptableObject, ISkill
{
    public string abilityName;
    public float currentTimer;

    public float CooldownTimer { get; set; }
    public float DamageMultiplier { get; set; }
    public SkillType Ability_Type { get; set; }

    public void ActivateSkill()
    {
    }

    public void SkillCooldown()
    {
    }
}
