using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNode : MonoBehaviour
{
    public Player player;
    public bool playerInRange;
    public int cooldownMax = 30;
    public int cooldown = 0;
    public Sprite activeSprite;
    public Sprite onCooldownSprite;
    public SpriteRenderer spriteRenderer;
    public bool canRefresh = true;

    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (this.cooldown <= 0) {
            this.spriteRenderer.sprite = this.activeSprite;
            if (this.playerInRange) {
                this.player.interactText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    this.player.Food += 1;
                    this.cooldown = this.cooldownMax;
                    this.spriteRenderer.sprite = this.onCooldownSprite;
                }
            }
        } else if (this.canRefresh) {
            this.cooldown--;
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
