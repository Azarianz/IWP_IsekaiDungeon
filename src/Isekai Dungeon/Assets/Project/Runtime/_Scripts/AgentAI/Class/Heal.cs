using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Heal")]
public class Heal : Ability
{
    public float skillRange;

    public GameObject fx1;

    public int skillAnimation = 1;

    public override void ActivateSkill(Agent_AI caster)
    {
        CharacterStats stat = caster.AgentData.GetAgentStats();
        if (stat.STAT_MANA >= manaCost && stat.STAT_HEALTH > healthCost)
        {
            float heal = caster.AgentData.GetAgentStats().STAT_DAMAGE * 4;
            float minHeal = heal * 0.8f;
            float maxHeal = heal * 1.2f;

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

            List<Agent_AI> nearbyAllies = caster.GetNearbyTeam(skillRange);

            foreach (Agent_AI agent in nearbyAllies)
            {
                float rand = Random.Range(minHeal, maxHeal);
                agent.ReceiveHeal(rand);
                agent.UpdateHealthBar();

            }

            stat.STAT_MANA -= manaCost;
            stat.STAT_HEALTH -= healthCost;
            caster.UpdateHealthBar();
            //Mana Bar Also

            //Debug.Log("Activate Skill: Heal");
        }


    }
}
