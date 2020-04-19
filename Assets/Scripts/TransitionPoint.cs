using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{

    public Transform transitionTo;
    public Player player;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && this.isActive) {
            Camera.main.transform.position = new Vector3(this.transitionTo.position.x, this.transitionTo.position.y, Camera.main.transform.position.z);
            this.player.transform.position = new Vector3(this.transitionTo.position.x, this.transitionTo.position.y, this.player.transform.position.z);
        }
        if (this.isActive) {
            this.player.interactText.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Debug.Log("enter collided with player");
            this.isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            Debug.Log("exit collided with player");
            this.isActive = false;
            this.player.interactText.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected() {
        Debug.DrawLine(this.transform.position, this.transitionTo.position);
    }
}
