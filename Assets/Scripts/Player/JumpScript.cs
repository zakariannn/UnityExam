using Misc;
using UnityEngine;

namespace Player {

    public class JumpScript: Physics2DObject {
        [SerializeField] private PlayerAnimationController animationController;
        private bool canJump = true;

        // Read the input from the player
        private void Update() {
            if (animationController == null) return;
            if (!animationController.summoned) return;
            
            if (canJump && Input.GetKeyDown(KeyCode.UpArrow)) {
                // Apply an instantaneous upwards force
                rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                canJump = true;
                animationController.Jump();
            }
        }
        
    }

}