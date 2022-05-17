using System;
using UnityEngine;

namespace Boss {

    public class LineOfSight : MonoBehaviour {
        [SerializeField] private BossController boss;
        private bool trackCollision = true;

        [SerializeField] private GameObject bossFightWall1;

        private void OnTriggerEnter2D(Collider2D other) {
            if (trackCollision && other.gameObject.CompareTag("Player")) {
                trackCollision = false;
                bossFightWall1.SetActive(true);
                boss.PlayerFound(other.transform);
            }
        }
    }

}