using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerDeathPosition : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (this.player.partnerDied) {
                this.player.triggerPartnerDeathScene();
            }
        }
    }
}
