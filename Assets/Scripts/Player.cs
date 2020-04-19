using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public int Health {
        get => health;
        set {
            if (value > 100) {
                this.health = 100;
            } else if (value < 0) {
                this.health = 0;
            } else {
                this.health = value;
            }
        }
    }
    public float maxSpeed = 10f;
    public float speed = 10f;
    public float jumpForce = 10f;
    private Rigidbody2D rb2d;
    public bool facingRight = false;
    public bool FacingRight {
        get { return this.facingRight; }
        set {
            this.facingRight = value;
            this.FlipSprite();
        }
    }

    public int food = 1;
    public int Food {
        get { return this.food; }
        set {
            this.food = value;
        }
    }

    private bool alive = true;
    public bool Alive {
        get { return this.alive; }
        set { this.alive = value; }
    }
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private float rayLength = .1f;
    [SerializeField]
    private Vector3[] rayPositions;
    [SerializeField]
    private Bullet bulletPrefab;
    public float fireRate;
    private float nextFire;
    public float swingRate;
    public float nextSwing;
    public float swingDelay;
    public bool partnerDied = false;
    public bool inPartnerDiedPosition = false;
    public Transform partnerDiedPosition;
    public GameObject interactText;
    public bool holdUntilPressE = true;
    public GameObject partnerDiedText;
    [SerializeField]
    private int ammo;
    public int maxAmmo;
    public int Ammo {
        get => ammo;
        set {
            if (value > this.maxAmmo) {
                this.ammo = this.maxAmmo;
            } else if (value < 0) {
                this.ammo = 0;
            } else {
                this.ammo = value;
            }
        }
    }
    public float eatCooldown;
    public float nextEat;

    private bool attackEnabled = true;
    public bool AttackEnabled { get => attackEnabled; set => attackEnabled = value; }

    public GameObject meleeHitbox;
    public int meleeDamage;

    void Start() {
        this.FacingRight = false;
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        foreach (Vector3 rayPosition in this.rayPositions) {
            Debug.DrawRay(transform.position + rayPosition, Vector2.down * this.rayLength, Color.green);
        }
    }

    void FixedUpdate() {
        if (!this.holdUntilPressE) {
            if (this.Alive && !this.partnerDied) {
                float moveHorizontal = Input.GetAxis("Horizontal");
                if (Input.GetButton("Jump")) {
                    this.Jump();
                }

                if (this.AttackEnabled) {
                    if (this.Ammo > 0) {
                        if (this.nextFire <= 0) {
                            if (Input.GetButton("Fire1") && !Input.GetKey(KeyCode.F)) {
                                this.Shoot();
                            }
                        } else {
                            this.nextFire -= 1;
                        }
                    }
                    if (this.nextSwing <= 0) {
                        if (Input.GetKey(KeyCode.F) && !Input.GetButton("Fire1")) {
                            this.Swing();
                        }
                    } else {
                        this.nextSwing -= Time.deltaTime;
                    }
                }

                if (this.nextEat <= 0) {
                    if (Input.GetKey(KeyCode.C)) {
                        this.Eat();
                    }
                } else {
                    this.nextEat -= Time.deltaTime;
                }

                if (this.swingDelay < 0) {
                    this.meleeHitbox.SetActive(false);
                } else {
                    this.swingDelay -= Time.deltaTime;
                }

                this.rb2d.velocity = new Vector2(moveHorizontal * this.maxSpeed, this.rb2d.velocity.y);
                this.clampHorizontalSpeed();
                this.Flip(moveHorizontal);
            } else if (!this.inPartnerDiedPosition) {
                this.rb2d.velocity = new Vector2(-1 * 1f, this.rb2d.velocity.y);
                this.clampHorizontalSpeed();
                this.FacingRight = false;
            }
        }
    }

    void Jump() {
        if (this.isGrounded()) {
            Debug.Log("jumping");
            this.rb2d.velocity = new Vector2(this.rb2d.velocity.y, 0);
            this.rb2d.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        }
    }

    void Shoot() {
        this.nextFire = this.fireRate;
        this.Ammo--;
        Instantiate(this.bulletPrefab, new Vector3(( this.facingRight ? 0.1f : -0.1f ), 0, 0) + this.transform.position, Quaternion.AngleAxis(( this.facingRight ? 0 : 180 ), Vector3.forward));
    }

    void Swing() {
        this.meleeHitbox.SetActive(true);
        this.swingDelay = 0.25f;
        this.nextSwing = this.swingRate;
    }

    void Eat() {
        if (this.Food > 0) {
            this.Health += 20;
            this.Food--;
            this.nextEat = this.eatCooldown;
        }
    }

    void Flip(float moveHorizontal) {
        if (moveHorizontal > 0 && !this.FacingRight || moveHorizontal < 0 && this.FacingRight) {
            this.FacingRight = !this.FacingRight;
        }
    }

    void FlipSprite() {
        this.sprite.flipX = !this.FacingRight;
        if (this.FacingRight) {
            this.meleeHitbox.transform.localPosition = new Vector3(0.1f, 0, 0);
        } else {
            this.meleeHitbox.transform.localPosition = new Vector3(-0.2f, 0, 0);
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

    private bool isGrounded() {
        bool grounded = false;
        int i = 0;
        while (!grounded && i < this.rayPositions.Length) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + this.rayPositions[i++], Vector2.down, this.rayLength);
            if (hit.collider != null) {
                grounded = hit.collider.tag == "Ground";
            }
        }
        return grounded;
    }

    public void Hurt(int damage) {
        this.Health -= damage;
        if (this.Health < 1) {
            this.Alive = false;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraFollower>().triggerPlayerDeathScene();
        }
    }

    public void triggerPartnerDeathScene() {
        this.inPartnerDiedPosition = true;
        this.partnerDiedText.SetActive(true);
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, 0));
    }
}
