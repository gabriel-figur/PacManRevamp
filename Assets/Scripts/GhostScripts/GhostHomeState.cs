using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostHomeState : GhostBehaviourSystem
{
    public Transform inHomeLocation;
    public Transform outHomeLocation;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ghost.ghostMovement.SetDirection(-ghost.ghostMovement.currentDirection);
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private IEnumerator ExitTransition()
    {
        ghost.ghostMovement.SetDirection(Vector2.up, true);
        ghost.ghostMovement.rigidBody.isKinematic = true;
        ghost.ghostMovement.enabled = false;

        Vector3 position = transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(position, inHomeLocation.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;
        
        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(inHomeLocation.position, outHomeLocation.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        ghost.ghostMovement.SetDirection(new Vector2(Random.value<0.5f?-1.0f:1.0f, 0.0f), true);
        ghost.ghostMovement.rigidBody.isKinematic = false;
        ghost.ghostMovement.enabled = true;
    }


}
