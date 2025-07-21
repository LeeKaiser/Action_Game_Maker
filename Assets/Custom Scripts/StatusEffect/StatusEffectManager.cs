using UnityEngine;
using System.Collections.Generic;
using StarterAssets;

[System.Flags]
    public enum EffectCategory
    {
        None,
        Buff,
        Debuff,
        Ohter
    }

public class StatusEffectManager : MonoBehaviour
{
    [Header("Status Effect Stats")]
    [Tooltip("current status effects")]
    List<StatusEffect> statusEffectList = new List<StatusEffect>();

    PlayableCharCore playerRef;

    [Tooltip("EffectTimeMultiplier for buffs")]
    public float EffectTimeMultiplierBuffs = 1;
    [Tooltip("EffectTimeMultiplier for debuffs")]
    public float EffectTimeMultiplierDebuffs = 1;

    public void Start()
    {
        playerRef = this.gameObject.GetComponent<PlayableCharCore>();
    }

    //get effect
    public void AddNewEffect(StatusEffect newEffect)
    {
        statusEffectList.Add(newEffect);
        newEffect.EffectedPlayer = playerRef;
        newEffect.ApplyEffect();
    }

    public void Update()
    {
        //delete inactive SE
        for (int i = statusEffectList.Count - 1; i >= 0; i--)
        {
            if (statusEffectList[i].ExpireViaTime)
            {
                float DurationMult = 1;
                if (statusEffectList[i].effectCategory == EffectCategory.Buff)
                {
                    DurationMult = EffectTimeMultiplierBuffs;
                }
                if (statusEffectList[i].effectCategory == EffectCategory.Debuff)
                {
                    DurationMult = EffectTimeMultiplierDebuffs;
                }

                statusEffectList[i].SpendDuration(Time.deltaTime / DurationMult);
                
            }
            
            

            if (!statusEffectList[i].CurrentlyActive())
            {
                statusEffectList.RemoveAt(i);
            }
        }
    }
}
