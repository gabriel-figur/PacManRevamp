using UnityEngine;
public class GhostEyesScript : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public SpriteRenderer spriteRender { get; private set; }
    public MovementScript ghostMovement { get; private set; }

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        ghostMovement = GetComponentInParent<MovementScript>();
    }

    private void Update()
    {
        if (ghostMovement.currentDirection == Vector2.up)
        {
            spriteRender.sprite = up;
        }
        else if (ghostMovement.currentDirection == Vector2.down)
        {
            spriteRender.sprite = down;
        }
        else if (ghostMovement.currentDirection == Vector2.left)
        {
            spriteRender.sprite = left;
        }
        else if (ghostMovement.currentDirection == Vector2.right)
        {
            spriteRender.sprite = right;
        }
    }
}
