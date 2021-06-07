using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skot : Enemy
{
    private static System.Random random = new System.Random(DateTime.Now.ToString().GetHashCode());

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
            if(isGrounded)State = EnemyState.JUMP;

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
}
