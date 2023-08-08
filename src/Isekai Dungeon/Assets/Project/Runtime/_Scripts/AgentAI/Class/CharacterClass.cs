using AI;
using AI.STATS;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum AttackRange { Sword = 8, Lance = 15, Bow = 125, Magic = 200};

public abstract class CharacterClass : ScriptableObject
{
    [SerializeField]
    public AttackRange attackRange;

    public string className;
    public List<Ability> abilities = new List<Ability>();

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }

    public int CastingPriority(Agent_AI agent)
    {
        int index = -1;
        int topPriority = -1;

        CharacterStats agentStat = agent.AgentData.GetAgentStats();

        for (int i = 0; i < abilities.Count; i++)
        {
            Ability ability = abilities[i];
            if (agentStat.STAT_HEALTH >= ability.healthCost &&
                agentStat.STAT_MANA >= ability.manaCost)
            {
                if(ability.SkillCondition(agent) && ability.skillPriority > topPriority)
                {
                    topPriority = ability.skillPriority;
                    index = i;
                }
            }
            
        }

        //Debug.Log("Casting Priority: " + index);
        return index;     
    }

    public void CastAbility(Agent_AI agent)
    {
        int abilityIndex = CastingPriority(agent);

        //Debug.Log("Casting ability at index: " + abilityIndex);

        if (abilityIndex == -1)
            return;

        abilities[abilityIndex].ActivateSkill(agent);
    }

}
