using UnityEngine;

public class PacmanScript : MonoBehaviour
{
    public MovementScript pacmanMovement;
    private Collider2D pacmanCollider;
    private SpriteRenderer spriteRenderer;
    public SpriteAnimation deadAnimation;
    public float targetDistance = 4f;
    public GameObject pinkyTarget;
    public GameObject cyanTargetFirst;
    public GameObject cyanTargetFinal;
    public GameObject redGhost;
    public GameManager GameManager;


    private void Awake()
    {
        pacmanMovement = GetComponent<MovementScript>();
        pacmanCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pacmanMovement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pacmanMovement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pacmanMovement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pacmanMovement.SetDirection(Vector2.right);
        }

        pinkyTarget.transform.position = transform.position + (Vector3)(pacmanMovement.currentDirection.normalized * targetDistance);

        cyanTargetFirst.transform.position = transform.position + (Vector3)(pacmanMovement.currentDirection.normalized * targetDistance / 2);
        Vector3 cyanTargetPivot = cyanTargetFirst.transform.position;
        Vector3 redPosition = redGhost.transform.position;
        Vector3 redToTargetVector = cyanTargetPivot - redPosition;
        Vector3 cyanToTargetVector = cyanTargetPivot + (redToTargetVector * 2);
        float desiredDistance = Vector3.Distance(cyanTargetPivot, redPosition) * 2f;
        float currentVectorDistance = Vector3.Distance(cyanTargetPivot, cyanToTargetVector);
        Vector3 newCyanToTargetVector = cyanTargetPivot + (redToTargetVector.normalized * desiredDistance);
        if (currentVectorDistance > desiredDistance)
        {
            cyanToTargetVector = newCyanToTargetVector;
        }
        cyanTargetFinal.transform.position = cyanToTargetVector;
        
        
        float rotation = Mathf.Atan2(pacmanMovement.currentDirection.y, pacmanMovement.currentDirection.x);
        Quaternion targetRotation = Quaternion.AngleAxis(rotation * Mathf.Rad2Deg, Vector3.forward);
        float zRotation = targetRotation.eulerAngles.z;
        
        if (Mathf.Approximately(zRotation, 180f) || Mathf.Approximately(zRotation, -180f))
        {
            targetRotation = Quaternion.Euler(180f, targetRotation.eulerAngles.y, 180f);
        }

        transform.rotation = targetRotation;
        if (GameManager.currentLevel == 1)
        {
            pacmanMovement.movementSpeedMultiplier = 0.8f;
        }
        else if(GameManager.currentLevel >= 2 && GameManager.currentLevel <= 4)
        {
            pacmanMovement.movementSpeedMultiplier = 0.9f;
        }
        else if (GameManager.currentLevel >= 5 && GameManager.currentLevel <= 20)
        {
            pacmanMovement.movementSpeedMultiplier = 1f;
        }
        else if (GameManager.currentLevel >= 21)
        {
            pacmanMovement.movementSpeedMultiplier = 0.9f;
        }
    }

    public void ResetPacmanState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        pacmanCollider.enabled = true;
        deadAnimation.enabled = false;
        deadAnimation.spriteRenderer.enabled = false;
        pacmanMovement.ResetMovement();
        gameObject.SetActive(true);
    }

    public void DeathAnimation()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        pacmanCollider.enabled = false;
        pacmanMovement.enabled = false;
        deadAnimation.enabled = true;
        deadAnimation.spriteRenderer.enabled = true;
        deadAnimation.RestartAnimation();
    }
}
