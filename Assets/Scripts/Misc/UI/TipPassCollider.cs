using System;
using UnityEngine;

namespace Misc.UI {

    public class TipPassCollider : MonoBehaviour {

        [SerializeField] private GameObject toHide;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                toHide.SetActive(false);
            }
        }
    }

}