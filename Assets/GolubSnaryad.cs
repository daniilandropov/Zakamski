using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolubSnaryad : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        var ground = collision.gameObject.GetComponent<Ground>();

        if (ground && ground is Ground)
        {
            Destroy(gameObject);
        }

        var hero = collision.gameObject.GetComponent<Hero>();

        if (hero && hero is Hero)
        {
            hero.ReciveDamage();
            Destroy(gameObject);
        }
    }
}
