using UnityEngine;

public class HasWidth : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    public float GetWitdth()
    {
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        return _boxCollider2D.size.x;
    }

    public float GetHeight()
    {
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        return _boxCollider2D.size.y;
    }
}
