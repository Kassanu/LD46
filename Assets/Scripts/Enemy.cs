using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float maxSpeed = 10f;
    public float speed = 10f;
    public float jumpForce = 10f;
    public Rigidbody2D rb2d;
    public Player player;
    public bool facingRight = false;
    public bool FacingRight {
        get { return this.facingRight; }
        set {
            this.facingRight = value;
            this.FlipSprite();
        }
    }

    public int moveCooldownStart = 100;
    public int moveCooldown = 0;
    public bool playerInAttackRadius = false;
    public bool PlayerInAttackRadius {
        get { return this.playerInAttackRadius; }
        set {
            this.playerInAttackRadius = value;
            this.moveCooldown = this.moveCooldownStart;
        }
    }

    public bool playerInAggroRadius = false;
    public bool PlayerInAggroRadius {
        get { return this.playerInAggroRadius; }
        set {
            this.playerInAggroRadius = value;
        }
    }

    public int attackCooldownStart = 10;
    public int attackCooldown = 0;
    public int attackAmount = 1;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private float groundRayLength = .1f;
    [SerializeField]
    private float forwardRayLength = .1f;
    [SerializeField]
    private Vector3[] jumpGroundRayPositions;
    [SerializeField]
    private Vector3[] jumpForwardRayPositions;

    public float distanceFromPlayer = 0;
    public bool destroyIfFarFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.FacingRight = false;
    }

    private void Update() {
        Debug.DrawLine(this.transform.position, player.transform.position);
        foreach (Vector3 rayPosition in this.jumpGroundRayPositions) {
            Debug.DrawRay(transform.position + rayPosition, Vector2.down * this.groundRayLength, Color.green);
        }
        foreach (Vector3 rayPosition in this.jumpForwardRayPositions) {
            Debug.DrawRay(transform.position + rayPosition, ( this.facingRight ? Vector2.right : Vector2.left ) * this.forwardRayLength, Color.green);
        }
    }

    private void FixedUpdate() {
        Vector3 direction = this.player.transform.position - this.transform.position;
        this.distanceFromPlayer = direction.magnitude;
        if (this.distanceFromPlayer > 20 && this.destroyIfFarFromPlayer) {
            Destroy(this.gameObject);
        }
        if (!this.PlayerInAttackRadius) {
            if (this.moveCooldown < 0 && this.PlayerInAggroRadius) {
                float moveHorizontal = direction.x > 0 ? 1 : -1;
                if (this.shouldJump() && this.isGrounded()) {
                    this.Jump();
                }
                this.rb2d.velocity = new Vector2(moveHorizontal * this.maxSpeed, this.rb2d.velocity.y);
                this.clampHorizontalSpeed();
                this.Flip(moveHorizontal);
            } else {
                this.moveCooldown--;
            }
        } else {
            if (this.attackCooldown < 0) {
                this.player.Hurt(this.attackAmount);
                this.attackCooldown = this.attackCooldownStart;
            } else {
                this.attackCooldown--;
            }
        }
    }

    void clampHorizontalSpeed() {
        var v = this.rb2d.velocity;
        var yd = v.y;
        v.y = 0f;
        v = Vector2.ClampMagnitude(v, this.maxSpeed);
        v.y = yd;
        this.rb2d.velocity = v;
    }

    void Jump() {
        this.rb2d.velocity = new Vector2(this.rb2d.velocity.y, 0);
        this.rb2d.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
    }

    void Flip(float moveHorizontal) {
        if (moveHorizontal > 0 && !this.FacingRight || moveHorizontal < 0 && this.FacingRight) {
            this.FacingRight = !this.FacingRight;
        }
    }

    void FlipSprite() {
        this.sprite.flipX = !this.FacingRight;
    }

    private bool isGrounded() {
        bool grounded = false;
        int i = 0;
        while (!grounded && i < this.jumpGroundRayPositions.Length) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + this.jumpGroundRayPositions[i++], Vector2.down, this.groundRayLength);
            if (hit.collider != null) {
                grounded = hit.collider.tag == "Ground";
            }
        }
        return grounded;
    }

    private bool shouldJump() {
        bool jump = false;
        int i = 0;
        while (!jump && i < this.jumpForwardRayPositions.Length) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + this.jumpForwardRayPositions[i++], this.facingRight ? Vector2.right : Vector2.left, this.forwardRayLength);
            if (hit.collider != null) {
                jump = hit.collider.tag == "Ground";
            }
        }
        return jump;
    }

    public void Hurt(int damage) {
        this.health -= damage;
        if (this.health < 1) {
            Destroy(this.gameObject);
        }
    }
}
