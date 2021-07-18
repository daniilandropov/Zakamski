using UnityEngine;

public class Golub : Enemy
{
    public GolubSnaryad Snaryad;

    void Update()
    {
        int value = random.Next(0, 1000);

        if (value < 995)
            State = EnemyState.RUN;
        else
            State = EnemyState.CHANGINGDIRECTION;

        if (value < 2)
            Attack();

        switch (State)
        {
            case EnemyState.RUN:
                _speed = DefaultSpeed;
                break;
            case EnemyState.CHANGINGDIRECTION:
                sr.flipX = !sr.flipX;
                direction = transform.right * (sr.flipX ? -1.0F : 1.0F);
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
    }

    private void Attack()
    {
        Instantiate(Snaryad, transform.position, Quaternion.identity);
    }
}