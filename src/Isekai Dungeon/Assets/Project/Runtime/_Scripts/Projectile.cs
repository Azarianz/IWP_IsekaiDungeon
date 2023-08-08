using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace IsekaiDungeon
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        public Sprite _sprite;
        public float _damage;

        public Projectile_Team projectileTeam;
        public Projectile_Type projectileType;
        public int maxPen = -1, currentPen = 0;

        public Agent_AI sender;

        [Tooltip("Position we want to hit")]
        public Transform target;

        [Tooltip("Horizontal speed, in units/sec")]
        public float speed = 10;

        [Tooltip("How high the arc should be, in units")]
        public float arcHeight = 1;


        Vector3 startPos;

        void Start()
        {
            // Cache our start position, which is really the only thing we need
            // (in addition to our current position, and the target).
            startPos = transform.position;
            //Debug.Log(projectileTeam.ToString());
        }

        void Update()
        {
            if(target != null)
            {
                MoveProjectile();
            }

            if(transform.position == target.position)
            {
                DestroyProjectile();
            }
        }

        void DestroyProjectile()
        {
            Destroy(gameObject);
            //transform.position = startPos;
        }


        #region Projectile Logic

        public void Damage(Agent_AI target)
        {
            sender.DeliverDamage(target);
            DestroyProjectile();
        }

        public void Explode(Agent_AI agent)
        {

        }

        public void Penetrate(Agent_AI target)
        {
            sender.DeliverDamage(target);
            currentPen++;

            if(currentPen >= maxPen)
            {
                DestroyProjectile();
            }
        }
        #endregion

        #region Collision Handling
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the collided GameObject is the opposing team
            if (!collision.gameObject.CompareTag(projectileTeam.ToString()))
            {
                Agent_AI collidedAgent = collision.GetComponent<Agent_AI>();

                switch(projectileType)
                {
                    case Projectile_Type.SigleTarget:
                        Damage(collidedAgent);
                        break;
                    case Projectile_Type.AreaOfEffect:
                        DestroyProjectile();
                        break;
                    case Projectile_Type.Penetrate:
                        Penetrate(collidedAgent);
                        break;
                }
            }
        }
        #endregion

        #region Physic Calculation
        void MoveProjectile()
        {
            Vector3 targetPos = target.position;

            // Compute the next position, with arc added in
            float x0 = startPos.x;
            float x1 = targetPos.x;
            float dist = x1 - x0;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
            float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

            // Rotate to face the next position, and then move there
            transform.rotation = LookAt2D(nextPos - transform.position);
            transform.position = nextPos;

            // Do something when we reach the target
            if (nextPos == targetPos) DestroyProjectile();
        }


        /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
        /// that makes the local +X axis point in the given forward direction.
        /// 
        /// forward direction
        /// Quaternion that rotates +X to align with forward
        static Quaternion LookAt2D(Vector2 forward)
        {
            return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
        }
        #endregion
    }
}
