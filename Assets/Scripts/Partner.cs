using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : MonoBehaviour
{
    public Player player;
    public bool playerInRange;
    public int food = 0;
    public int health = 100;
    public GameObject tutorialText;

    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && this.playerInRange) {
            if (this.player.Food > 0) {
                this.player.Food -= 1;
                this.food += 1;
                this.player.holdUntilPressE = false;
                this.tutorialText.SetActive(false);
            }
        }

        if(this.playerInRange && this.player.Food > 0 && this.health > 0) {
            this.player.interactText.SetActive(true);
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
            this.player.interactText.SetActive(false);
        }
    }
}
