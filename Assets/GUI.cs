using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Text CountOfKvas;
    public void SetKvas(int count)
    {
        CountOfKvas.text = count.ToString();
    }
}
