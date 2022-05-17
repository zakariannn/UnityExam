using System;
using Spine.Unity;
using UnityEngine;

namespace Boss {

    public class BossAnimationController : MonoBehaviour {
        
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField] private AnimationReferenceAsset idleAnimation;
        [SerializeField] private AnimationReferenceAsset headTurnAnimation;
        [SerializeField] private AnimationReferenceAsset walkAnimation;
        [SerializeField] private AnimationReferenceAsset runAnimation;
        [SerializeField] private AnimationReferenceAsset attackAnimation;
        [SerializeField] private AnimationReferenceAsset deathAnimation;
        
        private bool running;

        private void Start() {
            Idle();
        }
        
        public void Run() {
            if (running) return;
            running = true;
            Animate(runAnimation, 1, true, true);
        }

        private void Idle() {
            running = false;
            Animate(idleAnimation, 1, false, true);
        }
        
        public void Die() {
            skeletonAnimation.AnimationState.SetEmptyAnimation(1, float.MinValue);
            skeletonAnimation.AnimationState.SetEmptyAnimation(2, float.MinValue);
            skeletonAnimation.AnimationState.SetEmptyAnimation(3, float.MinValue);
            Animate(deathAnimation, 1, true, false);
        }

        public void Attack() {
            Animate(attackAnimation, 3, false, false);
        }

        private void Animate(AnimationReferenceAsset asset, int index, bool force, bool loop, bool setEmpty = false, float scale = 1f) {
            if (force) {
                skeletonAnimation.AnimationState.SetAnimation(index, asset, loop).TimeScale = scale;
            } else {
                skeletonAnimation.AnimationState.AddAnimation(index, asset, loop, 0).TimeScale = scale;   
            }
        }
        
    }

}