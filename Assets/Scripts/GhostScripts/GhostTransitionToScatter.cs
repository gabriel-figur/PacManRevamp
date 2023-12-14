using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostTransitionToScatter : GhostBehaviourSystem
{
    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        NodeScript node = other.GetComponent<NodeScript>();
        if (node != null && enabled)
        {
            if (node.validDirections.Count > 1)
            {
                Vector2 direction = Vector2.zero;
                float shortestDistance = float.MaxValue;
                Transform playerTransform = ghost.ghostHome.inHomeLocation;
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
        ghost.ghostHome.EnableState();
    }
}
