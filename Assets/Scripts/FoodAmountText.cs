using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAmountText : MonoBehaviour
{
    public Player player;
    private TextMesh text;

    void Start()
    {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.text = this.GetComponent<TextMesh>();
    }

    void Update()
    {
        this.text.text = "" + this.player.Food;
    }
}
