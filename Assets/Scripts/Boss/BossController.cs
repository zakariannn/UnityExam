using System.Collections;
using Misc;
using Player;
using UnityEngine;

namespace Boss {

    public class BossController : Physics2DObject {
        [SerializeField] private BossAnimationController animationController;
        private bool dead;
        private bool spottedPlayer;
        private Transform playerTransform;
        [SerializeField] private PlayerController playerController;

        private void Start() {
            Helper.SetAxisTowards(Directions.Right, transform, new Vector2(-1, 0));
        }

        public void PlayerFound(Transform other) {
            animationController.Run();
            spottedPlayer = true;
            playerTransform = other;
        }

        public void PlayerReached() {
            spottedPlayer = false;
            StartCoroutine(Attack());
        }

        private void FixedUpdate() {
            if (spottedPlayer) {
                var heading = playerTransform.position - transform.position;
                var dir = heading / heading.magnitude;
                Helper.SetAxisTowards(Directions.Right, transform, new Vector2(-1, 0));
                rigidbody2D.AddForce(dir * 700f);
            }
        }

        private IEnumerator Attack() {
            while (!dead && !playerController.isDead) {
                animationController.Attack();
                playerController.isDead = true;
                yield return new WaitForSecondsRealtime(0.5f);
            }
        }
    }

}