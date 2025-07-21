using UnityEngine;
using StarterAssets;




[CreateAssetMenu(fileName = "StatusEffect", menuName = "Scriptable Objects/StatusEffect")]
public abstract class StatusEffect : ScriptableObject
{
    [Header("Status Effect Stats")]
    [Tooltip("duration")]
    public float EffectDuration;
    protected float RemainingDuration;

    [Tooltip("duration is time based. true if it is time based, false if duration uses other system")]
    public bool ExpireViaTime = true;

    public PlayableCharCore EffectedPlayer;

    protected bool Active = true; //is active

    

    public EffectCategory effectCategory;

    //main effect
    public abstract void ApplyEffect();

    //inverse of effect that activates to remove the effect
    protected abstract void RemoveEffect();

    public bool CurrentlyActive()
    {
        return Active;
    }

    public void SpendDuration(float timePassed)
    {
        RemainingDuration -= timePassed;
        if (RemainingDuration <= 0)
        {
            RemoveEffect();
            Active = false;
        }

    }
}
