using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    public Camera mainCamera;
    public GameObject player;
    public GameObject rain;
    public GameObject playerDeathScenePosition;
    private Vector3 offset;
    public bool follow = true;

    void Start() {
        this.player = GameObject.FindWithTag("Player");
        this.offset = new Vector3(0,3,0);
        Camera.main.transform.position = new Vector3(this.player.transform.position.x + this.offset.x, this.player.transform.position.y + this.offset.y, Camera.main.transform.position.z);
    }


    void LateUpdate() {
        if (this.follow) {
            Camera.main.transform.position = new Vector3(this.player.transform.position.x + this.offset.x, this.player.transform.position.y + this.offset.y, Camera.main.transform.position.z);
        }
    }

    public void triggerPlayerDeathScene() {
        this.follow = false;
        this.rain.SetActive(false);
        Camera.main.transform.position = new Vector3(this.playerDeathScenePosition.transform.position.x, this.playerDeathScenePosition.transform.position.y, Camera.main.transform.position.z);
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, 0));
    }
}
