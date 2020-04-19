using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoNode : ResourceNode {
    override
    protected void giveResource() {
        this.player.Ammo += this.resourceAmount;
    }

    override
    protected bool canPickup() {
        return this.player.Ammo < this.player.maxAmmo;
    }
}
