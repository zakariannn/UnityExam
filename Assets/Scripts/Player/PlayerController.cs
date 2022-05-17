using System.Collections;
using Misc;
using UnityEngine;

namespace Player {

    public class PlayerController: Physics2DObject {

        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private GameObject bulletPrefab;
        private Vector2 movement, cachedDirection;
        [HideInInspector] public bool isDead;

        private void Update() {
            if (!animationController.summoned) return;
            
            CalculateMovement();
            CalculateShoot();
        }

        private void CalculateMovement() {
            var moveHorizontal = Input.GetAxis("Horizontal");
            movement = new Vector2(moveHorizontal, 0);
            if (movement.sqrMagnitude >= 0.01f) {
                cachedDirection = movement;
                animationController.Run();
            } else {
                animationController.RunToIdle();
            }

            Helper.SetAxisTowards(Directions.Right, transform, cachedDirection);
        }

        private void CalculateShoot() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                animationController.Shoot();
            }
        }
        
        private void FixedUpdate() {
            // Apply the force to the Rigidbody2d for moving
            rigidbody2D.AddForce(movement * 20f);
        }

        private void OnTriggerEnter2D(Collider2D collisionData) {
            if (collisionData.gameObject.CompareTag("Enemy")) {
                isDead = true;
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die() {
            animationController.Die();
            yield return new WaitForSecondsRealtime(1f);
            Destroy(gameObject);
        }
        
    }

}