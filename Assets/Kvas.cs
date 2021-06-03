using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kvas : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public bool Take()
    {
        if (enabled)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Kick(Vector3 direction, float kickForce, float y)
    {
        var newPos = new Vector2(direction.x, y);

        var v = newPos * kickForce;
        rigidbody.AddForce(v, ForceMode2D.Impulse);
    }


}
