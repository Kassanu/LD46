using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRadius : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.GetComponentInParent<Enemy>().PlayerInAttackRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.GetComponentInParent<Enemy>().PlayerInAttackRadius = false;
        }
    }
}
