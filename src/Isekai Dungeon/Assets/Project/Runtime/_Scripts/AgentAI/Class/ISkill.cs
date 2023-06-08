using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    float CooldownTimer { get; set; }
    float DamageMultiplier { get; set; }
    SkillType Ability_Type { get; set; }
    void ActivateSkill();
    void SkillCooldown();
}

[Serializable]
public enum SkillType
{
    Passive = 0,
    Active = 1
}
