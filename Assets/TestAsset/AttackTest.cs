using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using System.Collections;

public class AttackTest : Ability
{
    


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

    }
}
