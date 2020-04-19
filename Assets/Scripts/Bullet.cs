using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    public float speed = 1;

    void Update() {
        this.transform.position += transform.right * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<Enemy>().Hurt(this.damage);
            Destroy(this.gameObject);
        }
    }
}
