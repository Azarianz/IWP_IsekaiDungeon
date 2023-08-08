using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Melee")]
public class Slash : Ability
{
    public GameObject fx1;

    public override bool SkillCondition(Agent_AI caster)
    {
        // Get the position of the caster, adjusted for its collider radius.
        Vector3 casterPosition = caster.transform.position;

        // Get the position of the target, adjusted for its collider radius.
        Vector3 targetPosition = caster.Agent_Target.transform.position;

        float distance = (targetPosition - casterPosition).magnitude;

        if (caster.AgentData.unit_class == "Mage")
        {
            Debug.Log("SLASH DISTANCE: " + distance);
            Debug.Log("Attack Range: " + (float)AttackRange.Sword);
        }

        if (distance <= (float)AttackRange.Sword)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public override void ActivateSkill(Agent_AI caster)
    {
        CharacterStats stat = caster.AgentData.GetAgentStats();

        if (stat.STAT_MANA >= manaCost && stat.STAT_HEALTH > healthCost)
        {
            caster.OnAttack?.Invoke();

            stat.STAT_MANA -= manaCost;
            stat.STAT_HEALTH -= healthCost;
            caster.UpdateHealthBar();

            //Debug.Log("Activate Skill: Slash");
        }
    }
}
