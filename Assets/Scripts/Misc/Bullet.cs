using System;
using UnityEngine;

namespace Misc {

    public class Bullet: MonoBehaviour {

        public void Shoot(Vector2 dir) {
            GetComponent<Rigidbody2D>().AddForce(dir * 100);
        }
        private void OnTriggerEnter2D(Collider2D other) {
            
        }

        private void OnCollisionEnter2D(Collision2D other) {
            
        }
        
    }

}