using UnityEngine;

public class GhostBehaviourSystem : MonoBehaviour
{
    public GhostScript ghost { get; private set; }
    public float stateDuration;
    public bool isRed;
    public bool isPink;
    public bool isCyan;
    public bool isOrange;

    private void Awake()
    {
        ghost = GetComponent<GhostScript>();
        enabled = false;
    }

    public void EnableState()
    {
        EnableState(stateDuration);
    }

    public virtual void EnableState(float stateDuration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(DisableState), this.stateDuration);
    }

    public virtual void DisableState()
    {
        this.enabled = false;
        CancelInvoke();
    }
}
