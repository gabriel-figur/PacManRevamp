using System;
using Unity.VisualScripting;
using UnityEngine;

public class GhostChaseState : GhostBehaviourSystem
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        NodeScript node = other.GetComponent<NodeScript>();
        if (node != null && enabled && !ghost.ghostFrightened.enabled)
        {
            if (node.validDirections.Count > 1)
            {
                Vector2 direction = Vector2.zero;
                float shortestDistance = float.MaxValue;
                Transform playerTransform = ghost.target;
                
                if (isCyan)
                {
                    playerTransform = ghost.cyanTarget;
                }
                
                float currentDistance = (playerTransform.position - transform.position).sqrMagnitude;
          
                if (isOrange)
                {
                    if (currentDistance > 80f)
                    {
                        playerTransform = ghost.scatterTarget;
                    }
                    else
                    {
                        playerTransform = ghost.target;
                    }
                }

                Vector2 currentDirection = ghost.ghostMovement.currentDirection;
                Vector2 oppositeDirection = -currentDirection;

                foreach (Vector2 validDirection in node.validDirections)
                {
                    if (validDirection != oppositeDirection)
                    {
                        Vector3 newPosition = transform.position + new Vector3(validDirection.x, validDirection.y, 0.0f);
                        float distance = (playerTransform.position - newPosition).sqrMagnitude;
                        

                        if (distance < shortestDistance)
                        {
                            direction = validDirection;
                            shortestDistance = distance;
                        }
                    }
                }
                ghost.ghostMovement.SetDirection(direction);
            }
        }
    }

    private void OnDisable()
    {
        ghost.ghostScatter.EnableState();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position, 8f);
    }
}
