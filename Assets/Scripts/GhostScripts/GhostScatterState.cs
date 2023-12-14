using System;
using UnityEngine;

public class GhostScatterState : GhostBehaviourSystem
{
    private void OnEnable()
    {
        ghost.ghostMovement.SetDirection(-ghost.ghostMovement.currentDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        NodeScript node = other.GetComponent<NodeScript>();

        if (node != null && enabled && !ghost.ghostFrightened.enabled)
        {
            if (node.validDirections.Count > 1)
            {
                Vector2 direction = Vector2.zero;
                float shortestDistance = float.MaxValue;
                Transform scatterHomeTransform = ghost.scatterTarget;
                Vector2 currentDirection = ghost.ghostMovement.currentDirection;
                Vector2 oppositeDirection = -currentDirection;

                foreach (Vector2 validDirection in node.validDirections)
                {
                    if (validDirection != oppositeDirection)
                    {
                        Vector3 newPosition =
                            transform.position + new Vector3(validDirection.x, validDirection.y, 0.0f);
                        float distance = (scatterHomeTransform.position - newPosition).sqrMagnitude;

                        if (distance < shortestDistance)
                        {
                            direction = validDirection;
                            shortestDistance = distance;
                        }
                    }
                }

                ghost.ghostMovement.SetDirection(direction);
                //Debug.Log(direction.ToString());
            }
        }
    }


    private void OnDisable()
    {
        ghost.ghostChase.EnableState();
    }
}
