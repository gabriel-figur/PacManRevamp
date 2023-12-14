using UnityEngine;

public class GhostFrightenedState : GhostBehaviourSystem
{
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer eyesRenderer;
    public SpriteRenderer scaredRenderer;
    public SpriteRenderer scaredEndRender;
    public float oldSpeed;
    
    public bool isDead { get; private set; }

    public override void EnableState(float stateDuration)
    {
        base.EnableState(stateDuration);

        bodyRenderer.enabled = false;
        eyesRenderer.enabled = false;
        scaredRenderer.enabled = true;
        scaredEndRender.enabled = false;

        Invoke(nameof(ScaredEnding), stateDuration / 2.0f);
    }
    
    public override void DisableState()
    {
        base.DisableState();
        
        bodyRenderer.enabled = true;
        eyesRenderer.enabled = true;
        scaredRenderer.enabled = false;
        scaredEndRender.enabled = false;
    }

    private void ScaredEnding()
    {
        if (!isDead)
        {
            scaredRenderer.enabled = false;
            scaredEndRender.enabled = true;
            scaredEndRender.GetComponent<SpriteAnimation>().RestartAnimation();
        }
    }

    private void OnEnable()
    {
        ghost.ghostMovement.SetDirection(-ghost.ghostMovement.currentDirection);
        scaredRenderer.GetComponent<SpriteAnimation>().RestartAnimation();
        oldSpeed = ghost.ghostMovement.movementSpeedMultiplier;
        ghost.ghostMovement.movementSpeedMultiplier = 0.5f;
        isDead = false;
    }

    private void OnDisable()
    {
        ghost.ghostMovement.movementSpeedMultiplier = oldSpeed;
        isDead = false;
    }

    private void Dead()
    {
        isDead = true;
        ghost.SetPosition(ghost.ghostHome.inHomeLocation.position);
        ghost.ghostHome.EnableState(stateDuration);
        bodyRenderer.enabled = false;
        eyesRenderer.enabled = true;
        scaredRenderer.enabled = false;
        scaredEndRender.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
            {
                Dead();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        NodeScript node = other.GetComponent<NodeScript>();
        if (node != null && enabled)
        {
            if (node.validDirections.Count > 1)
            {
                Vector2 direction = Vector2.zero;
                float furthestDistance = float.MinValue;
                Transform playerTransform = ghost.target;
                Vector2 currentDirection = ghost.ghostMovement.currentDirection;
                Vector2 oppositeDirection = -currentDirection;

                foreach (Vector2 validDirection in node.validDirections)
                {
                    if (validDirection != oppositeDirection)
                    {
                        Vector3 newPosition = transform.position + new Vector3(validDirection.x, validDirection.y, 0.0f);
                        float distance = (playerTransform.position - newPosition).sqrMagnitude;

                        if (distance > furthestDistance)
                        {
                            direction = validDirection;
                            furthestDistance = distance;
                        }
                    }
                }
                ghost.ghostMovement.SetDirection(direction);
            }
        }
        
    }
}
