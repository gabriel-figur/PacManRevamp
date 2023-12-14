using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBodyScript : MonoBehaviour
{
    public Sprite[] upSprites;
    public Sprite[] downSprites;
    public Sprite[] leftSprites;
    public Sprite[] rightSprites;

    public SpriteRenderer spriteRenderer { get; private set; }
    public MovementScript ghostMovement { get; private set; }

    public float animationTimer = 0.15f;
    private float currentTimer = 0f;
    private int currentSpriteIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ghostMovement = GetComponentInParent<MovementScript>();
    }

    private void Update()
    {
        if (ghostMovement.currentDirection == Vector2.up)
        {
            SwitchSprites(upSprites);
        }
        else if (ghostMovement.currentDirection == Vector2.down)
        {
            SwitchSprites(downSprites);
        }
        else if (ghostMovement.currentDirection == Vector2.left)
        {
            SwitchSprites(leftSprites);
        }
        else if (ghostMovement.currentDirection == Vector2.right)
        {
            SwitchSprites(rightSprites);
        }
    }

    private void SwitchSprites(Sprite[] sprites)
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= animationTimer)
        {
            currentSpriteIndex++;
            if (currentSpriteIndex >= sprites.Length)
            {
                currentSpriteIndex = 0;
            }

            spriteRenderer.sprite = sprites[currentSpriteIndex];
            currentTimer = 0f;
        }
    }
}