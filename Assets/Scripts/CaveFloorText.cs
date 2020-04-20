using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveFloorText : MonoBehaviour
{
    public Player player;
    private TextMesh text;

    void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.text = this.GetComponent<TextMesh>();
    }

    void Update() {
        if (this.player.InCave) {
            this.text.text = this.player.CaveFloor;
        } else {
            this.text.text = "";
        }
    }
}
