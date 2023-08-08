using AI;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using IsekaiDungeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Animator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Sprite _sprite;
    private RuntimeAnimatorController _controller;
    private Agent_AI agentAI;

    private SimpleFlash flashEffect { get { return GetComponent<SimpleFlash>(); } }
    private ColoredFlash coloredflashEffect { get { return GetComponent<ColoredFlash>(); } }

    [SerializeField]
    private GameObject LevelUpFX;

    // Define the range for sorting order (minimum and maximum values)
    private int minSortingOrder = -15;
    private int maxSortingOrder = 10;

    public void Initialize_Data(Sprite spr, RuntimeAnimatorController anim)
    {
        _sprite = spr;
        _controller = anim;
    }

    public void Initialize(Sprite spr, RuntimeAnimatorController anim)
    {
        _sprite = spr;
        _controller = anim;

        agentAI = GetComponentInParent<Agent_AI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = _sprite;
        animator.runtimeAnimatorController = _controller;
    }

    private void LateUpdate()
    {
        if(agentAI.enabled)
        {
            // Get the Y position of the GameObject
            float yPosition = transform.position.y;

            // Calculate the sorting order based on Y position
            // Higher Y position will have lower sorting order
            int sortingOrder = Mathf.RoundToInt(-yPosition * 10f);

            // Clamp the sorting order to the specified range
            sortingOrder = Mathf.Clamp(sortingOrder, minSortingOrder, maxSortingOrder);

            // Set the sorting order of the Sprite Renderer
            spriteRenderer.sortingOrder = sortingOrder;
        }
    }

    public void Animate_Idle()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Move", false);
    }

    public void Animate_Move()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", true);
    }

    public void Animate_Attack()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", false);
        animator.ResetTrigger("OnShoot");
        animator.SetTrigger("OnAttack");
    }

    public void Animate_Shoot()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", false);
        animator.ResetTrigger("OnAttack");
        animator.SetTrigger("OnShoot");
    }

    public void Animate_LevelUp()
    {
        GameObject fx = Instantiate(LevelUpFX, transform);
        AnimateColorFlash(Color.cyan);
        Destroy(fx, 2f);
    }

    public void Animate_Death()
    {
        animator.SetTrigger("Death");
        spriteRenderer.sortingOrder = -99;
    }

    public void Animate_Hit()
    {
        //animator.SetTrigger("Hit");
        //animator.Play("Hit", -1, 0f);
    }

    public void Animate_Skill1()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", false);
        animator.SetTrigger("Skill_1");
    }

    public void Animate_Skill2()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", false);
        animator.SetTrigger("Skill_2");
    }

    public void ResetTriggers()
    {
        animator.ResetTrigger("OnAttack");
        animator.ResetTrigger("OnShoot");
        animator.ResetTrigger("Skill_1");
        animator.ResetTrigger("Skill_2");
        animator.ResetTrigger("Hit");
    }

    public void FlipSprite(bool condition)
    {
        spriteRenderer.flipX = condition;
    }

    public void DestroyAgent()
    {
        agentAI.AgentFlock.RemoveAgentFromFlock(agentAI);
        agentAI.Agent_Target = null;
        agentAI.AgentRigidBody.velocity = Vector3.zero;
    }

    public bool GetAnimBool(string animName)
    {
        return animator.GetBool(animName);
    }

    public void AnimateFlash()
    {
        flashEffect.Flash();
    }

    public void AnimateColorFlash(Color color)
    {
        coloredflashEffect.Flash(color);
    }

    public void ShootProjectile(int index)
    {
        Debug.Log("Shoot Projectile");
        GameObject projectilePrefab = agentAI.AgentData.GetAgentClass().abilities[index].projectilePrefab;

        GameObject _GO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = _GO.GetComponent<Projectile>();

        //Debug.Log("Prefab: " + projectilePrefab);
        //Debug.Log("Instantiate: " + _GO);
        //Debug.Log("Projectile Component: " + projectile);

        projectile.sender = agentAI;
        projectile._damage = agentAI.AgentData.GetAgentStats().STAT_DAMAGE;
        projectile.target = agentAI.Agent_Target.transform;

        if(agentAI.AgentFlock.IsEnemy)
        {
            projectile.projectileTeam = Projectile_Team.Enemy;
        }
        else
        {
            projectile.projectileTeam = Projectile_Team.Player;
        }
    }

    public void UseSkill(int index)
    {
        agentAI.AgentData.GetAgentClass().abilities[index].ActivateSkill();
    }

    public void AnimateSkillFX(GameObject fx, Vector3 spawnPos, Vector3 offset, float damageScale)
    {
        GameObject go = Instantiate(fx, spawnPos + offset, Quaternion.identity);
        Projectile projectile = go.GetComponent<Projectile>();
        projectile.target = agentAI.Agent_Target.transform;

        float finalDamage = agentAI.AgentData.GetAgentStats().CalculateDamage() * damageScale;
        projectile._damage = finalDamage;
    }
}
