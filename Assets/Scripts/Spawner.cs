using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Player player;
    public Enemy enemyPrefab;
    public Transform leftSpawnPosition;
    public Transform rightSpawnPosition;
    public int maxEnemies = 3;
    public float spawnRate;
    private float nextSpawn;
    public List<Enemy> enemiesSpawned;
    public bool leftCanSpawn = false;
    public bool rightCanSpawn = false;
    public float spawnCheckRayLength = 1f;
    private bool spawnerEnabled = true;
    public bool SpawnerEnabled { get => spawnerEnabled; set => spawnerEnabled = value; }

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.transform.position = this.player.transform.position;
        this.enemiesSpawned = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(leftSpawnPosition.position, Vector2.down * this.spawnCheckRayLength, Color.green);
        Debug.DrawRay(rightSpawnPosition.position, Vector2.down * this.spawnCheckRayLength, Color.green);
        this.enemiesSpawned.RemoveAll(item => item == null);
        this.transform.position = this.player.transform.position;
        if (this.SpawnerEnabled) {
            this.checkCanSpawn();
            if (this.enemiesSpawned.Count < this.maxEnemies && ( this.leftCanSpawn || this.rightCanSpawn )) {
                if (this.nextSpawn <= 0) {
                    this.SpawnEnemy();
                } else {
                    this.nextSpawn -= 1;
                }
            }
        }
    }

    private void SpawnEnemy() {
        Vector3 spawnPosition;
        if (this.leftCanSpawn && this.rightCanSpawn) {
            if (UnityEngine.Random.Range(0, 100) < 50) {
                spawnPosition = this.leftSpawnPosition.position;
            } else {
                spawnPosition = this.rightSpawnPosition.position;
            }
        }

        if (this.leftCanSpawn) {
            spawnPosition = this.leftSpawnPosition.position;
        } else {
            spawnPosition = this.rightSpawnPosition.position;
        }

        this.enemiesSpawned.Add(Instantiate(this.enemyPrefab, spawnPosition, Quaternion.identity));
        this.nextSpawn = this.spawnRate;
    }

    private void checkCanSpawn() {
        RaycastHit2D leftHit = Physics2D.Raycast(this.leftSpawnPosition.position, Vector2.down, this.spawnCheckRayLength);
        RaycastHit2D rightHit = Physics2D.Raycast(this.rightSpawnPosition.position, Vector2.down, this.spawnCheckRayLength);
        if (leftHit.collider != null) {
            this.leftCanSpawn = leftHit.collider.tag == "Ground";
        }
        if (rightHit.collider != null) {
            this.rightCanSpawn = rightHit.collider.tag == "Ground";
        }
    }
}
