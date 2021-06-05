using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Petukh : Enemy
{
    private SpriteRenderer sr;
    private new Rigidbody2D rigidbody;
    public GameObject Foots;

    public float DefaultSpeed = 15f;
    public float jumpForce = 10.0F;
    private float _speed = 0f;
    private Vector3 direction;
    public bool isGrounded = false;

    public float groundSize = 2.20f;

    public PeyukhState State = PeyukhState.IDLE;

    private static System.Random random = new System.Random(DateTime.Now.ToString().GetHashCode());

    void Start()
    {
        direction = transform.right;
        sr = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        var sprites = Resources.LoadAll<Sprite>("PetyaTexturs");

        var index = random.Next(0, sprites.Length);
        sr.sprite = sprites[index];

    }

    // Update is called once per frame
    void Update()
    {
        int value = random.Next(0, 1000);

        if (value < 200)
            State = PeyukhState.IDLE;
        else if (value > 200 & value < 700)
            State = PeyukhState.RUN;
        else if (value > 990 & value < 995)
            State = PeyukhState.CHANGINGDIRECTION;
        else
            if(isGrounded)State = PeyukhState.JUMP;

        switch (State)
        {
            case PeyukhState.IDLE:
                _speed = 0f;
                break;
            case PeyukhState.RUN:
                _speed = DefaultSpeed;
                break;
            case PeyukhState.JUMP:
                if (isGrounded) rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                break;
            case PeyukhState.CHANGINGDIRECTION:
                sr.flipX = !sr.flipX;
                direction = transform.right * (sr.flipX ? -1.0F : 1.0F);
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
    }

    private void CheckGround()
    {
        var coliders = Physics2D.OverlapCircleAll(Foots.transform.position, groundSize).Where(x => x.GetComponent<Ground>() != null);

        isGrounded = coliders.Count() > 0;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    public enum PeyukhState
    {
        IDLE,
        RUN,
        JUMP,
        CHANGINGDIRECTION
    }
}
