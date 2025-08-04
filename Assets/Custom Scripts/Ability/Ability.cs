using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Collections;

public abstract class Ability: MonoBehaviour
{
    //charge represents a use of an ability (functional as ammo)
    //charge point is progress to complete a full charge up or reload.
    [Header("ability stats")]
    [Tooltip("ability stats")]
    public AbilityStats abilityStat;
    [Tooltip("reference to user")]
    public GameObject UserRef;
    //variables
    protected int currentCharge ; //remaining  charge

    protected float chargePointsProgress; //current progress on getting new charge

    protected float chargePointMultiplier = 1; //amount of multiplier to the charge rate

    protected bool rechargeInProgress = false;

    protected InputAction boundAction;

    protected AbilityManager manager;
    protected bool isActive = false;

    private float holdStartTime;
    private Coroutine holdLoop;
    private bool isCharging = false;

    void Start()
    {
        currentCharge = abilityStat.maxCharge;
    }

    public virtual void Initialize(PlayerInput playerInput, AbilityManager owningManager, GameObject playerReference)
    {
        this.manager = owningManager;
        this.UserRef = playerReference;

        if (abilityStat.actionReference == null || abilityStat.actionReference.action == null)
        {
            Debug.LogError($"{gameObject.name}: No InputActionReference assigned.");
            return;
        }

        // Get the runtime action instance from PlayerInput
        boundAction = playerInput.actions.FindAction(abilityStat.actionReference.action.name);

        if (boundAction == null)
        {
            Debug.LogError($"{gameObject.name}: Action '{abilityStat.actionReference.action.name}' not found in PlayerInput.");
            return;
        }

        boundAction.started += OnInputStarted;
        boundAction.performed += OnInputHeld;
        boundAction.canceled += OnInputCanceled;

        Debug.Log($"{gameObject.name} bound to input: {boundAction.name}");

        abilityStat.onAbilityUsePlayerEvent.Init( UserRef.GetComponent<PlayerEventManager>());
    }

    //method that activates when user first presses the input
    protected virtual void OnInputStarted(InputAction.CallbackContext context)
    {
        if (!CanActivate()) return;

        if (context.interaction is HoldInteraction)
        {
            Debug.Log("Hold started → begin auto-fire and charge timer");

            if (abilityStat.inputMode.HasFlag(AbilityStats.InputResponseMode.Release) )
            {
                isCharging = true;
                holdStartTime = Time.time;
            }

            if (holdLoop == null && abilityStat.inputMode.HasFlag(AbilityStats.InputResponseMode.Hold) )
            {
                holdLoop = StartCoroutine(RepeatWhileHeld());
            }
        }

        
    }

    //method that activates when user holds the input
    protected virtual void OnInputHeld(InputAction.CallbackContext context)
    {
        if (!CanActivate()) return;

        if (context.interaction is TapInteraction && abilityStat.inputMode.HasFlag(AbilityStats.InputResponseMode.Tap))
        {
            Debug.Log("Tap performed → single fire");
            StartCoroutine(ActivateAbility());
        }
    }

    //method that activates when user lets go of the input
    protected virtual void OnInputCanceled(InputAction.CallbackContext context)
    {
        if (!CanActivate()) return;

        if (isCharging && abilityStat.inputMode.HasFlag(AbilityStats.InputResponseMode.Release))
        {
            Debug.Log("Hold released → fire charged shot");

            isCharging = false;

            float heldTime = Time.time - holdStartTime;
            float chargeRatio = Mathf.Clamp01(heldTime / abilityStat.maxChargeTime);

            StartCoroutine(ActivateReleasedAbility(chargeRatio));
        }

        if (holdLoop != null)
        {
            StopCoroutine(holdLoop);
            holdLoop = null;
        }
    }

    protected virtual IEnumerator RepeatWhileHeld()
    {
        while (CanActivate())
        {
            
            Debug.Log("Auto-firing while held");
            yield return Execute(); // normal attack
            
            float unloadTime = 1 / abilityStat.usePerSec;

            yield return new WaitForSeconds(unloadTime);
        }
        holdLoop = null;
        yield return null;
    }

    protected virtual IEnumerator ActivateAbility()
    {
        isActive = true;
        manager.NotifyAbilityStarted(this);

        yield return Execute(); // ability logic

        isActive = false;
        manager.NotifyAbilityEnded(this);
    }

    protected virtual IEnumerator ActivateReleasedAbility(float chargeRatio)
    {
        isActive = true;
        manager.NotifyAbilityStarted(this);

        yield return ExecuteReleased(chargeRatio); // override this in subclasses

        isActive = false;
        manager.NotifyAbilityEnded(this);
    }

    // tap or held logic
    protected abstract IEnumerator Execute();

    // charged then released logic
    protected abstract IEnumerator ExecuteReleased(float chargeRatio);

    protected virtual bool CanActivate()
    {
        return !isActive && manager.CanUseAbility(this) && currentCharge >= 1;
    }



    // # Ability recharge code
    //recover ability charge point
    public void RecoverChargePoint(float TimeElapsed){

        if (rechargeInProgress)
        {
            chargePointsProgress += abilityStat.chargePointsPerSec * TimeElapsed * chargePointMultiplier;
            while (chargePointsProgress >= abilityStat.chargePointsRequired)
            {
                //give a charge
                if (currentCharge < abilityStat.maxCharge)
                {
                    currentCharge += abilityStat.chargeGainPerFullRecharge;
                }
                //subtract charge points required from charge point progress 
                chargePointsProgress -= abilityStat.chargePointsRequired;
                //if fully charged, reset charge point progress to 0
                if (currentCharge >= abilityStat.maxCharge)
                {
                    currentCharge = abilityStat.maxCharge;
                    chargePointsProgress = 0;
                    rechargeInProgress = false;
                }
            }
        }
    }

    public void GiveChargePointDirect(float ChargePtAdd )
    {
        RecoverChargePoint(ChargePtAdd / abilityStat.chargePointsPerSec);
    }

    public void InterruptReload()
    {
        if (rechargeInProgress)
        {
            chargePointsProgress = 0;
        }
    }

    public virtual void Cleanup()
    {
        if (abilityStat.actionReference != null)
            boundAction.started -= OnInputStarted;
            boundAction.performed -= OnInputHeld;
            boundAction.canceled -= OnInputCanceled;
    }

}
