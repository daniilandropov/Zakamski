using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private static System.Random random = new System.Random(DateTime.Now.ToString().GetHashCode());


    void Start()
    {
        int value = random.Next(0, 1000);

        if(value <= 150)
        {
            var bear = Resources.Load<Enemy>("Enemy/MEDVED");
            Instantiate(bear, this.transform.position, Quaternion.identity);
        }else if(value > 150 && value <= 350)
        {
            Instantiate(Resources.Load<Kvas>("Kvas"), this.transform.position, Quaternion.identity);
        }

    }
}
