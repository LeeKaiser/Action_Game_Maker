using UnityEngine;

[CreateAssetMenu(fileName = "TestStatusEffect", menuName = "Scriptable Objects/TestStatusEffect")]
public class TestStatusEffect : StatusEffect
{
    [Tooltip("amount of speed boost")]
    public float SpeedMultiplierTest;

    public override void ApplyEffect()
    {
        Active = true;
        RemainingDuration = EffectDuration;
        EffectedPlayer.ModifyForwardSpeed(SpeedMultiplierTest);
        EffectedPlayer.ModifyStrafeSpeed(SpeedMultiplierTest);
        EffectedPlayer.ModifyBackwardSpeed(SpeedMultiplierTest);
    }

    protected override void RemoveEffect()
    {
        //reverse the speed bonus
        Active = false;
        EffectedPlayer.ModifyForwardSpeed(-SpeedMultiplierTest);
        EffectedPlayer.ModifyStrafeSpeed(-SpeedMultiplierTest);
        EffectedPlayer.ModifyBackwardSpeed(-SpeedMultiplierTest);
    }
}
