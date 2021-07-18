using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MedvedTaxi : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        var hero = collision.gameObject.GetComponent<Hero>();

        if (hero) SceneManager.LoadScene("Level2");
    }
}
