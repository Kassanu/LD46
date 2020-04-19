using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBar;
    public Player player;

    void Update() {
        healthBar.transform.localScale = new Vector3(this.getLocalScale(), healthBar.transform.localScale.y, 0);
        healthBar.transform.localPosition = new Vector3(this.getLocalPosition(), healthBar.transform.localPosition.y, 0) + new Vector3(-1.5f, 0, 0);
    }

    private float getHealthPercent() {
        return this.player.Health / 100f * 100f;
    }

    private float getLocalScale() {
        return 3 * (this.getHealthPercent ()) / 100f;
    }
    private float getLocalPosition() {
        return 1.5f * ( this.getHealthPercent() ) / 100f;
    }
}
