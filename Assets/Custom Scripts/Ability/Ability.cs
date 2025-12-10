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

    public AbilityManager manager;
    protected bool isActive = false;

    void Awake()
    {
        currentCharge = abilityStat.maxCharge;
        GetComponentInParent<AbilityManager>().AddAbility(this.gameObject);
    }

    public virtual void Initialize(AbilityManager owningManager, GameObject playerReference)
    {
        this.manager = owningManager;
        this.UserRef = playerReference;

        if (abilityStat.actionReference == null || abilityStat.actionReference.action == null)
        {
            Debug.LogError($"{gameObject.name}: No InputActionReference assigned.");
            return;
        }
    }

    protected void ConsumeCharge(int chargeConsumed)
    {
        currentCharge -= chargeConsumed;
        if (currentCharge < 0)
        {
            currentCharge = 0;
        }
    }

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

    public abstract void Cleanup();

}
