using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] public Ability[] abilities;
    private PlayerInput playerInput;
    private Ability currentlyActiveAbility;
    public GameObject playerRef;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        foreach (var ability in abilities)
        {
            if (ability != null)
                ability.Initialize(playerInput, this, playerRef);
        }
    }

    void OnDisable()
    {
        foreach (var ability in abilities)
        {
            if (ability != null)
                ability.Cleanup();
        }
    }

    public bool CanUseAbility(Ability ability)
    {
        return currentlyActiveAbility == null || ability.canInterruptOthers;
    }

    public void NotifyAbilityStarted(Ability ability)
    {
        if (currentlyActiveAbility != null && ability.canInterruptOthers)
        {
            // Optionally add cancellation logic here
            Debug.Log($"{ability.name} is interrupting {currentlyActiveAbility.name}");
        }

        currentlyActiveAbility = ability;
    }

    public void NotifyAbilityEnded(Ability ability)
    {
        if (currentlyActiveAbility == ability)
            currentlyActiveAbility = null;
    }

    private void Update()
    {
        foreach (var ability in abilities)
        {
            if (ability != null)
                ability.perTick(Time.deltaTime);
        }
    }
}
