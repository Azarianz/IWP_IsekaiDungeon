using AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsekaiDungeon
{
    public interface IProjectile
    {
        void Damage(Agent_AI agent);
        void Explode(Agent_AI agent);
        void Penetrate(Agent_AI agent);
    }

    [Serializable]
    public enum Projectile_Team
    {
        Player, 
        Enemy
    }

    [Serializable]
    public enum Projectile_Type
    {
        SigleTarget,
        AreaOfEffect,
        Penetrate
    }
}
