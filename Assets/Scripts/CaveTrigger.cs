using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    public GameObject sky;
    public GameObject dirt;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            this.sky.SetActive(false);
            this.dirt.SetActive(true);
            other.GetComponent<Player>().InCave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            this.sky.SetActive(true);
            this.dirt.SetActive(false);
            other.GetComponent<Player>().InCave = false;
        }
    }
}
