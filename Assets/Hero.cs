using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    public bool isGrounded = false;

    public float speed = 5.0F;

    public float jumpForce = 15.0F;

    public float groundSize = 2.20f;

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
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, groundSize);

        isGrounded = coliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    void Update()
    {
        if (isGrounded) State = CharState.Idle;
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space))) Jump();
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
}

public enum CharState
{
    Idle,
    Walk,
    Jump,
}