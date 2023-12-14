using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] spriteArray;
    private int spriteFrames;
    public float animationRate = 0.3f;
    public bool shouldLoop = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextSprite), animationRate, animationRate);
    }

    private void NextSprite()
    {
        if(!spriteRenderer.enabled)
            return;
        
        spriteFrames++;

        if (spriteFrames >= spriteArray.Length && shouldLoop)
        {
            spriteFrames = 0;
        }

        if (spriteFrames >= 0 && spriteFrames < spriteArray.Length)
        {
            spriteRenderer.sprite = spriteArray[spriteFrames];
        }
    }

    public void RestartAnimation()
    {
        spriteFrames = -1;
        NextSprite();
    }
}
