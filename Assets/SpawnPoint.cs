using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Kvas ThisKvas;
    public Skot Petukh;
    public Skot Khryak;
    public Skot Porosenok;
    public Skot Kozel;
    public Skot Baran;

    private static System.Random random = new System.Random(DateTime.Now.ToString().GetHashCode());
    void Start()
    {
        
        int value = random.Next(0, 1000);

        if(this.transform.position.y > 4)
        {
            if (value <= 150)
                Instantiate(ThisKvas, this.transform.position, Quaternion.identity);
            else if (value > 300 && value <= 450)
                Instantiate(Petukh, this.transform.position, Quaternion.identity);
            else if (value > 500 && value <= 600)
                Instantiate(Porosenok, this.transform.position, Quaternion.identity);
            else if (value > 600 && value <= 650)
                Instantiate(Kozel, this.transform.position, Quaternion.identity);
            else if (value > 650 && value <= 700)
                Instantiate(Baran, this.transform.position, Quaternion.identity);
        }
        else
        {
            if (value <= 100)
                Instantiate(ThisKvas, this.transform.position, Quaternion.identity);
            else if (value > 300 && value <= 400)
                Instantiate(Petukh, this.transform.position, Quaternion.identity);
            else if (value > 400 && value <= 500)
                Instantiate(Khryak, this.transform.position, Quaternion.identity);
            else if (value > 500 && value <= 600)
                Instantiate(Porosenok, this.transform.position, Quaternion.identity);
            else if (value > 600 && value <= 700)
                Instantiate(Kozel, this.transform.position, Quaternion.identity);
            else if (value > 700 && value <= 800)
                Instantiate(Baran, this.transform.position, Quaternion.identity);
        }

        
    }
}
