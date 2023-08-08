using AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    float cooldownTimer { get; set; }
    float skillCooldown { get; set; }
    float DamageMultiplier { get; set; }
    SkillType Ability_Type { get; set; }
    void ActivateSkill();
    bool SkillCondition(Agent_AI agent);
}

[Serializable]
public enum SkillType
{
    Passive = 0,
    Active = 1
}
