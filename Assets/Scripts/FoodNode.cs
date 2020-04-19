using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNode : ResourceNode
{
    override
    protected void giveResource() {
        this.player.Food += this.resourceAmount;
    }
}
