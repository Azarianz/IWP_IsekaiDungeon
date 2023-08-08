using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject, ISkill
{
    public string abilityName;
    public float cooldownTimer { get; set; }
    public float skillCooldown { get; set; }
    public int skillPriority = 0;
    public Sprite skill_icon;
    public GameObject projectilePrefab;

    //Cost
    public float healthCost, manaCost;

    public float CooldownTimer { get; set; }
    public float DamageMultiplier { get; set; }
    public SkillType Ability_Type { get; set; }

    public virtual void ActivateSkill() { }
    public virtual void ActivateSkill(Agent_AI caster) { }
    public virtual bool SkillCondition(Agent_AI caster) { return true; }
}
