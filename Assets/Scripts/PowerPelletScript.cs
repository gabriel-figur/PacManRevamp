public class PowerPelletScript : PelletScript
{
    public float duration = 8f;

    protected override void Consume()
    {
        FindObjectOfType<GameManager>().PowerPelletConsumed(this);
    }
}
