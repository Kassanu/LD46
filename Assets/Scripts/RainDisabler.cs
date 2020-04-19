using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDisabler : MonoBehaviour
{
    public GameObject rain;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.rain.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.rain.SetActive(true);
        }
    }
}
