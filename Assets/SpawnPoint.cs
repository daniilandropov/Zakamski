using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Kvas ThisKvas;

    private static System.Random random = new System.Random(DateTime.Now.ToString().GetHashCode());
    void Start()
    {
        int value = random.Next(0, 1000);

        if (value > 400 && value < 700)
            Instantiate(ThisKvas, this.transform.position, Quaternion.identity);
    }
}
