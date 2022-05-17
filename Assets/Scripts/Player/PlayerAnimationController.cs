using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

namespace Player {

    public class PlayerAnimationController: MonoBehaviour {
        
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField] private AnimationReferenceAsset idleAnimation;
        [SerializeField] private AnimationReferenceAsset summonAnimation;
        [SerializeField] private AnimationReferenceAsset walkAnimation;
        [SerializeField] private AnimationReferenceAsset runAnimation;
        [SerializeField] private AnimationReferenceAsset jumpAnimation;
        [SerializeField] private AnimationReferenceAsset runToIdleAnimation;
        [SerializeField] private AnimationReferenceAsset idleTurnAnimation;
        [SerializeField] private AnimationReferenceAsset shootAnimation;
        [SerializeField] private AnimationReferenceAsset deathAnimation;
        [SerializeField] private AnimationReferenceAsset aimAnimation;

        private bool running;
        private bool onAir;
        public bool summoned;
        private Coroutine stopAimCoroutine;

        private void Start() {
            GetComponent<MeshRenderer>().enabled = true;
            Summon();
            Idle();
            StartCoroutine(SummonCounter());
        }

        private IEnumerator SummonCounter() {
            yield return new WaitForSecondsRealtime(3.6f);
            summoned = true;
        }

        public void Run() {
            if (running || onAir || !summoned) return;
            running = true;
            Animate(runAnimation, 1, true, true);
        }

        public void RunToIdle() {
            if (!running || !summoned) return;
            Animate(runToIdleAnimation, 1, true, false);
            Idle();
        }

        public void Jump() {
            if (!summoned) return;
            onAir = true;
            skeletonAnimation.AnimationState.SetEmptyAnimation(1, float.MinValue);
            Animate(jumpAnimation, 1, true, false);
            Idle();
        }

        public void Die() {
            if (!summoned) return;
            summoned = false;
            skeletonAnimation.AnimationState.SetEmptyAnimation(1, float.MinValue);
            skeletonAnimation.AnimationState.SetEmptyAnimation(2, float.MinValue);
            skeletonAnimation.AnimationState.SetEmptyAnimation(3, float.MinValue);
            Animate(deathAnimation, 1, true, false);
        }

        public void Shoot() {
            if (!summoned) return;
            if (stopAimCoroutine != null) {
                StopCoroutine(stopAimCoroutine);   
            }
            Animate(aimAnimation, 2, true, false);
            Animate(shootAnimation, 3, false, false);
            stopAimCoroutine = StartCoroutine(CancelAim());
        }

        private IEnumerator CancelAim() {
            yield return new WaitForSecondsRealtime(2f);
            skeletonAnimation.AnimationState.SetEmptyAnimation(2, float.MinValue);
        }

        private void Summon() {
            Animate(summonAnimation, 1, false, false);
        }

        private void Idle() {
            running = false;
            Animate(idleAnimation, 1, false, true);
        }

        private void Animate(AnimationReferenceAsset asset, int index, bool force, bool loop, bool setEmpty = false, float scale = 1f) {
            if (force) {
                skeletonAnimation.AnimationState.SetAnimation(index, asset, loop).TimeScale = scale;
            } else {
                skeletonAnimation.AnimationState.AddAnimation(index, asset, loop, 0).TimeScale = scale;   
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collisionData) {
            if (collisionData.gameObject.CompareTag("Ground") || collisionData.gameObject.CompareTag("Interactable")) {
                onAir = false;
            }
        }
        
    }

}