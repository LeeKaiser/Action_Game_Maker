using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using System.Collections;
using AbilityInputEvents;

public class AbilityTest : Ability
{
    public GameObject SpeedBoostPrefab;

    void Start(){
        EventBus<PlayerStartAbility1>.Subscribe(executeAbility);
    }

    public void executeAbility(PlayerStartAbility1 inputEventInfo)
    {
        if (inputEventInfo.PlayerIdentity != UserRef)
        {
            return;
        }

        if (!CanActivate())
        {
            return;
        }

        if (currentCharge <= 0)
        {
            return;
        }

        InterruptReload();
        Debug.Log("Ability1 activated");
        ConsumeCharge(1);
    }

    void Update()
    {
        if (currentCharge < abilityStat.maxCharge && !isActive)
        {
            rechargeInProgress = true;
        }
        RecoverChargePoint(Time.deltaTime); //recharge every tick if possible
    }

    public override void Cleanup()
    {
        EventBus<PlayerStartAbility1>.Unsubscribe(executeAbility);
    }
}
