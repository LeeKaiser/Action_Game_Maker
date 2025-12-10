using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using System.Collections;
using AbilityInputEvents;

public class AttackTest : Ability
{
    public GameObject AttackPrefab;
    
    void Start(){
        EventBus<PlayerStartAttack1>.Subscribe(executeAbility);
    }

    public void executeAbility(PlayerStartAttack1 inputEventInfo)
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
        Debug.Log("Attack1 activated");
        GameObject Attack = Instantiate(AttackPrefab, UserRef.GetComponent<PlayableCharCore>().PlayerArmature.position, UserRef.GetComponent<PlayableCharCore>().PlayerArmature.rotation);

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
        EventBus<PlayerStartAttack1>.Unsubscribe(executeAbility);
    }
}
