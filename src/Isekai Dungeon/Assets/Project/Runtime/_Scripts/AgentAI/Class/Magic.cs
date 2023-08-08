using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Magic")]
public class Magic : Ability
{
    public Vector3 skilloffset;
    public float damageScale;
    public int skillAnimation = 1;

    public override void ActivateSkill(Agent_AI caster)
    {
        CharacterStats stat = caster.AgentData.GetAgentStats();
        if (stat.STAT_MANA >= manaCost && stat.STAT_HEALTH > healthCost)
        {
            switch (skillAnimation)
            {
                case 1:
                    caster.OnCastSkill1?.Invoke();
                    break;
                case 2:
                    caster.OnCastSkill2?.Invoke();
                    break;
                default:
                    caster.OnCastSkill1?.Invoke();
                    break;
            }

            Transform target = caster.Agent_Target.transform;
            caster.Agent_Animator.AnimateSkillFX(projectilePrefab, target.position, skilloffset, damageScale);


            stat.STAT_MANA -= manaCost;
            stat.STAT_HEALTH -= healthCost;
            caster.UpdateHealthBar();

            //Debug.Log("Activate Skill: Artillery");
        }

    }
}
