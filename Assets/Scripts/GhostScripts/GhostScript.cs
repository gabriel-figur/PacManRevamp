using UnityEngine;
using UnityEngine.PlayerLoop;


public class GhostScript : MonoBehaviour
{
    public MovementScript ghostMovement { get; private set; }
    public GhostHomeState ghostHome { get; private set; }
    public GhostScatterState ghostScatter { get; private set; }
    public GhostChaseState ghostChase { get; private set; }
    public GhostFrightenedState ghostFrightened { get; private set; }
    public GhostBehaviourSystem primalState;
    public Transform target;
    public Transform scatterTarget;
    public Transform cyanTarget;
    public int worth = 200;
    public GameManager GameManager;
    public void Awake()
    {
        ghostMovement = GetComponent<MovementScript>();
        ghostHome = GetComponent<GhostHomeState>();
        ghostScatter = GetComponent<GhostScatterState>();
        ghostChase = GetComponent<GhostChaseState>();
        ghostFrightened = GetComponent<GhostFrightenedState>();
    }

    private void Start()
    {
        ResetGhostState();
    }

    public void SetPosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }

    public void ResetGhostState()
    {
        gameObject.SetActive(true);
        ghostMovement.ResetMovement();
        ghostFrightened.DisableState();
        ghostChase.DisableState();
        ghostScatter.EnableState();
        
        if (ghostHome != primalState)
        {
            ghostHome.DisableState();
        }
        if (primalState != null)
        {
            primalState.EnableState();
        }
        
        if (GameManager.currentLevel == 1)
        {
            ghostMovement.movementSpeedMultiplier = 0.75f;
        }
        else if(GameManager.currentLevel >= 2 && GameManager.currentLevel <= 4)
        {
            ghostMovement.movementSpeedMultiplier = 0.85f;
        }
        else if (GameManager.currentLevel >= 5 && GameManager.currentLevel <= 20)
        {
            ghostMovement.movementSpeedMultiplier = 0.95f;
        }
        else if (GameManager.currentLevel >= 21)
        {
            ghostMovement.movementSpeedMultiplier = 0.95f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Pacman"))
        {
            if (ghostFrightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostDead(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanDead();
            }
        }
    }
}
