using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    public GameObject Foots;
    public bool isGrounded = false;
    private Kvas _kvas;

    public float ShootSpeed = 1.0F;
    public float ShootTimer = 0.0f;

    public float speed = 5.0F;

    public float jumpForce = 15.0F;

    public float groundSize = 2.20f;

    private LevelManager _levelManager;

    public bool GetKeyPressed = false;
    public bool KickKeyPressed = false;

    public bool Invulnerability = false; // неуязвимость во время получения удара

    public float InvulnerabilityTime = 1.0F;
    public float InvulnerabilityTimer = 0.0f;

    public void SetLM(LevelManager lm)
    {
        _levelManager = lm;
    }

    public CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        _kvas = Resources.Load<Kvas>("Kvas");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        var coliders = Physics2D.OverlapCircleAll(Foots.transform.position, groundSize).Where(x => x.GetComponent<Ground>() != null);

        isGrounded = coliders.Count() > 0;

        if (!isGrounded) State = CharState.Jump;
    }

    void Update()
    {
        if (isGrounded) State = CharState.Idle;
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) Jump();
        if (ShootTimer <= 0 && Input.GetKeyDown(KeyCode.Q)) Attack();
        if (ShootTimer <= 0 && Input.GetKeyDown(KeyCode.C)) DropKvas();

        if (Input.GetKey(KeyCode.E)) GetKeyPressed = true; else GetKeyPressed = false;
        if (Input.GetKey(KeyCode.V)) KickKeyPressed = true; else KickKeyPressed = false;

        if (ShootTimer > 0)
        {
            ShootTimer -= Time.deltaTime;
        }

        if (Invulnerability && InvulnerabilityTimer > 0)
        {
            InvulnerabilityTimer -= Time.deltaTime;
        }
        else
        {
            InvulnerabilityTimer = InvulnerabilityTime;
            Invulnerability = false;
        }
    }

    private void Attack()
    {
        if (!_levelManager.MinusKvas() || Invulnerability)
            return;

        Vector3 position = transform.position;
        position.y += 0.5F;

        position.x += sprite.flipX ? -2f : 2F;

        var kvasBullet = Instantiate(_kvas, position, Quaternion.identity);

        kvasBullet.Kick(kvasBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F), 15f, 0.2f);

        ShootTimer = ShootSpeed;
    }

    private void DropKvas()
    {
        if (!_levelManager.MinusKvas() || Invulnerability)
            return;

        Vector3 position = transform.position;
        position.y += 0.5F;

        position.x += sprite.flipX ? -2f : 2F;

        var kvasBullet = Instantiate(_kvas, position, Quaternion.identity);
    }

    private void Run()
    {
        if (isGrounded) State = CharState.Walk;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;
    }

    public void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var kvas = collision.gameObject.GetComponent<Kvas>();

        if (kvas && kvas is Kvas)
        {
            if (GetKeyPressed)
                if (kvas.Take())
                    _levelManager.AddKvas();

            if (KickKeyPressed)
                kvas.Kick(transform.right * (sprite.flipX ? -1.0F : 1.0F), 1.2f, 0.7f);

        }

        if (!Invulnerability)
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy && enemy is Enemy)
            {
                ReciveDamage();
            }
        }
    }

    public void ReciveDamage()
    {
        Invulnerability = true;
        rigidbody.AddForce(transform.up * jumpForce + transform.right * (sprite.flipX ? 1.0F : -1.0F), ForceMode2D.Impulse);

        var countOfKvas = _levelManager.Damage();

        if (countOfKvas <= 0)
            SceneManager.LoadScene("SampleScene");
        else
        {
            for (int i = 0; i < countOfKvas; i++)
            {
                Vector3 position = transform.position;
                position.y += 0.5F;

                position.x += sprite.flipX ? 2f : -2F;

                var kvasBullet = Instantiate(_kvas, position, Quaternion.identity);

                kvasBullet.Kick(kvasBullet.transform.right * (sprite.flipX ? 1.0F : -1.0F), 15f, 0.2f);
            }


        }
    }
}

public enum CharState
{
    Idle,
    Walk,
    Jump,
}