using UnityEngine;

public class PelletScript : MonoBehaviour
{
    public int worth = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Consume();
        }
    }

    protected virtual void Consume()
    {
        FindObjectOfType<GameManager>().PelletConsumed(this);
    }
}
