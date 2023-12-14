using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform portalTransform;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER");
        Vector3 position = other.transform.position;
        position.x = portalTransform.position.x;
        position.y = portalTransform.position.y;
        other.transform.position = position;
    }
}
