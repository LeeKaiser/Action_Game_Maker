using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using System.Collections;

[CreateAssetMenu(fileName = "AbilityTest", menuName = "Scriptable Objects/Ability/AbilityTest")]
public class AbilityTest : Ability
{
    public StatusEffect SpeedBoost;

    protected override IEnumerator Execute()
    {
        if (currentCharge > 0)
        {
            Debug.Log("Ability tapped or held. apply speed boost");
            UserRef.GetComponent<StatusEffectManager>().AddNewEffect(SpeedBoost);
            onAbilityUsePlayerEvent.DetectPlayerEvent();
            currentCharge -= 1;
        }
        InterruptReload();
        yield return new WaitForSeconds(useTime);
    }

    protected override IEnumerator ExecuteReleased(float chargeRatio)
    {
        Debug.Log($"Ability released with charge: {chargeRatio:F2}");
        currentCharge -= 1;
        yield return null;
    }

    public override void perTick(float deltaTime)
    {
        if (currentCharge < maxCharge && !isActive)
        {
            rechargeInProgress = true;
        }
        RecoverChargePoint(deltaTime); //recharge every tick if possible
    }
}
