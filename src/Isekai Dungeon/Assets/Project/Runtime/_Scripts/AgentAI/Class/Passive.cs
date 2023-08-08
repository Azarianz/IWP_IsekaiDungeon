using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Passive Buff")]
public class Passive : Ability
{
    //TODO, Seperate Ability Code to ActiveAbility & PassiveAbility
    public float minHeal, maxHeal;

    public float healthIncrease, manaIncrease, damageIncrease, defenseIncrease;
    public float skillRange;

    public GameObject fx1;

    public int skillAnimation = 1;

    public override void ActivateSkill(Agent_AI caster)
    {
        CharacterStats stat = caster.AgentData.GetAgentStats();
        if (stat.STAT_MANA >= manaCost && stat.STAT_HEALTH > healthCost)
        {
            List<Agent_AI> nearbyAllies = caster.GetNearbyTeam(skillRange);

            foreach (Agent_AI agent in nearbyAllies)
            {
                float rand = Random.Range(minHeal, maxHeal);
                agent.ReceiveHeal(rand);
            }

            stat.STAT_MANA -= manaCost;
            stat.STAT_HEALTH -= healthCost;
            caster.UpdateHealthBar();

            Debug.Log("Activate Skill: Passive");
        }

    }
}
