using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    public Camera mainCamera;
    public GameObject player;
    private Vector3 offset;
    void Start() {
        this.player = GameObject.FindWithTag("Player");
        this.offset = new Vector3(0,3,0);
        Camera.main.transform.position = new Vector3(this.player.transform.position.x + this.offset.x, this.player.transform.position.y + this.offset.y, Camera.main.transform.position.z);
    }


    void LateUpdate() {
        Camera.main.transform.position = new Vector3(this.player.transform.position.x + this.offset.x, this.player.transform.position.y + this.offset.y, Camera.main.transform.position.z);
    }
}
