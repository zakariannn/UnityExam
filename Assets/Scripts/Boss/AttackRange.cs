using UnityEngine;

namespace Boss {

    public class AttackRange : MonoBehaviour {
        
        [SerializeField] private BossController boss;
        private bool trackCollision = true;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (trackCollision && other.gameObject.CompareTag("Player")) {
                trackCollision = false;
                Debug.Log("Attack");
                boss.PlayerReached();
            }
        }
        
    }

}