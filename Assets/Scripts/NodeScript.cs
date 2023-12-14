using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public List<Vector2> validDirections { get; private set; }
    public LayerMask wallLayer;
    public string specialNodeTag;

    private void Start()
    {
        validDirections = new List<Vector2>();
        ValidateDirection(Vector2.up);
        ValidateDirection(Vector2.down);
        ValidateDirection(Vector2.left);
        ValidateDirection(Vector2.right);
    }

    private void ValidateDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, wallLayer);

        if (hit.collider == null)
        {
            validDirections.Add(direction);
        }
    }
}
