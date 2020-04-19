using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroRadius : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.GetComponentInParent<Enemy>().PlayerInAggroRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.GetComponentInParent<Enemy>().PlayerInAggroRadius = false;
        }
    }
}
