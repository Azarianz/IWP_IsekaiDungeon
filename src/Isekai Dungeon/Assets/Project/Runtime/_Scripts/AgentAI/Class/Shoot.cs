using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Shoot")]
public class Shoot : Ability
{
    public GameObject fx1;

    public override bool SkillCondition(Agent_AI caster)
    {
        // Get the position of the caster, adjusted for its collider radius.
        Vector3 casterPosition = caster.transform.position;

        // Get the position of the target, adjusted for its collider radius.
        Vector3 targetPosition = caster.Agent_Target.transform.position;

        // Calculate the distance between the adjusted positions of the caster and the target, subtracting the collider radii.
        float distance = Vector3.Distance(casterPosition, targetPosition);

        if(distance <= (float)AttackRange.Bow && 
            distance > (float)AttackRange.Sword)
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
            caster.OnShoot?.Invoke();

            stat.STAT_MANA -= manaCost;
            stat.STAT_HEALTH -= healthCost;
            caster.UpdateHealthBar();

            //Debug.Log("Activate Skill: Slash");
        }
    }
}
