using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsekaiDungeon
{
    public class AnimatorFactory
    {
        public RuntimeAnimatorController GetAnimatorByName(string name)
        {
            string animatorPath = "Animators/" + name;
            RuntimeAnimatorController animator = Resources.Load<RuntimeAnimatorController>(animatorPath);
            if (animator == null)
            {
                Debug.LogError("Animator not found for name: " + name);
            }
            return animator;
        }
    }


}
