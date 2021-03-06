﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceNode : MonoBehaviour
{
    public Player player;
    public bool playerInRange;
    public float cooldownMax = 30;
    public float cooldown = 0;
    public Sprite activeSprite;
    public Sprite onCooldownSprite;
    public SpriteRenderer spriteRenderer;
    public bool canRefresh = true;
    public int resourceAmount = 0;

    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (this.cooldown <= 0) {
            this.spriteRenderer.sprite = this.activeSprite;
            if (this.playerInRange && this.canPickup()) {
                this.player.interactText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    this.giveResource();
                    this.cooldown = this.cooldownMax;
                    this.spriteRenderer.sprite = this.onCooldownSprite;
                    this.player.interactText.SetActive(false);
                }
            }
        } else if (this.canRefresh) {
            this.cooldown -= Time.deltaTime;
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
    
    virtual protected bool canPickup() {
        return true;
    }

    protected abstract void giveResource();
}
