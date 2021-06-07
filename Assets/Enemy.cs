using System;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected new Rigidbody2D rigidbody;
    public GameObject Foots;

    public float DefaultSpeed = 15f;
    public float jumpForce = 10.0F;
    protected float _speed = 0f;
    protected Vector3 direction;
    public bool isGrounded = false;
    public float DamageSpeed = 3.0f;

    public float groundSize = 2.20f;

    public EnemyState State = EnemyState.IDLE;

    public string TextureName = "SvinTexturs";

    protected static System.Random random = new System.Random(DateTime.Now.ToString().GetHashCode());

    void Start()
    {
        direction = transform.right;
        sr = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        var sprites = Resources.LoadAll<Sprite>(TextureName);

        var index = random.Next(0, sprites.Length);
        sr.sprite = sprites[index];

    }

    void Update()
    {
        int value = random.Next(0, 1000);

        if (value < 200)
            State = EnemyState.IDLE;
        else if (value > 200 & value < 700)
            State = EnemyState.RUN;
        else if (value > 990 & value < 995)
            State = EnemyState.CHANGINGDIRECTION;
        else
            if (isGrounded) State = EnemyState.JUMP;

        switch (State)
        {
            case EnemyState.IDLE:
                _speed = 0f;
                break;
            case EnemyState.RUN:
                _speed = DefaultSpeed;
                break;
            case EnemyState.JUMP:
                if (isGrounded) rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                break;
            case EnemyState.CHANGINGDIRECTION:
                sr.flipX = !sr.flipX;
                direction = transform.right * (sr.flipX ? -1.0F : 1.0F);
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
    }

    protected void CheckGround()
    {
        var coliders = Physics2D.OverlapCircleAll(Foots.transform.position, groundSize).Where(x => x.GetComponent<Ground>() != null);

        isGrounded = coliders.Count() > 0;
    }

    protected void FixedUpdate()
    {
        CheckGround();
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        var kvas = collision.gameObject.GetComponent<Kvas>();
        {
            if (kvas && kvas is Kvas)
            {
                var speed = kvas.GetSpeed();
                if (speed > DamageSpeed)
                    Destroy(gameObject);
            }
        }

        var enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy && enemy is Enemy) State = EnemyState.CHANGINGDIRECTION;
    }

    public enum EnemyState
    {
        IDLE,
        RUN,
        JUMP,
        CHANGINGDIRECTION
    }
}