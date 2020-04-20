using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : MonoBehaviour
{
    public Player player;
    public bool playerInRange;
    public int food = 0;
    public int givenFood = 0;
    public int health = 100;
    public float foodDropTime = 0;
    public float nextFoodDropTime = 100f;
    public float healthDropTime = 0;
    public float nextHealthDropTime = 2f;
    public GameObject tutorialText;
    public GameObject winText;

    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.foodDropTime = this.nextFoodDropTime;
        this.healthDropTime = this.nextHealthDropTime;
    }

    // Update is called once per frame
    void Update() {
        if (this.givenFood >= 5) {
            this.winText.SetActive(true);
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, 0));
        } else {
            if (this.health > 0) {
                if (Input.GetKeyDown(KeyCode.E) && this.playerInRange) {
                    if (this.player.Food > 0) {
                        this.player.Food -= 1;
                        this.food += 1;
                        this.givenFood += 1;
                        this.player.holdUntilPressE = false;
                        this.player.interactText.SetActive(false);
                        this.tutorialText.SetActive(false);
                    }
                }

                if (!this.player.holdUntilPressE) {
                    if (this.food > 0) {
                        if (this.foodDropTime < 0) {
                            this.food--;
                            this.foodDropTime = this.nextFoodDropTime;
                        } else {
                            this.foodDropTime -= Time.deltaTime;
                        }
                    } else {
                        if (this.healthDropTime < 0) {
                            this.health--;
                            this.healthDropTime = this.nextHealthDropTime;
                        } else {
                            this.healthDropTime -= Time.deltaTime;
                        }
                    }

                    if (this.playerInRange && this.player.Food > 0) {
                        this.player.interactText.SetActive(true);
                    }
                }
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
            this.player.interactText.SetActive(false);
        }
    }
}
