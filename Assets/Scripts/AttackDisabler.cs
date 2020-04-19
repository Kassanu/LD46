using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDisabler : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.player.AttackEnabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.player.AttackEnabled = true;
        }
    }
}
