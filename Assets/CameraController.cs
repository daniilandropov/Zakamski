using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 46.2F;

    [SerializeField]
    private Transform target;

    Vector3 MemoryPosition;

    private void Update()
    {
        try
        {
            if (!target)
            {
                var t = FindObjectOfType<Hero>().transform;
                if (t != null)
                    target = t;
                else
                    return;
            }
            Vector3 position = target.position;
            position.z = -10.0F;
            position.y += 4.5F;
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
            MemoryPosition = target.position;
        }
        catch
        {
            transform.position = Vector3.Lerp(transform.position, MemoryPosition, speed * Time.deltaTime);
            Application.Quit();
        }

    }
}
