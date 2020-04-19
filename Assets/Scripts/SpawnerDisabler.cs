using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDisabler : MonoBehaviour
{
    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        this.spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.spawner.SpawnerEnabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            this.spawner.SpawnerEnabled = true;
        }
    }
}
