using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerHealthCheck : MonoBehaviour
{
    public Player player;
    public Partner partner;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.partner = GameObject.FindWithTag("Partner").GetComponent<Partner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerInRange) {
            if (this.partner.health < 1) {
                this.player.partnerDied = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            this.playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            this.playerInRange = false;
        }
    }
}
