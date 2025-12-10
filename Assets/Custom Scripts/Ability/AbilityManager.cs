using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<Ability> abilitiesList;
    private Ability currentlyActiveAbility;
    public GameObject playerRef;

    void Awake()
    {
    }

    void Start()
    {
        foreach (var ability in abilitiesList)
        {
            if (ability != null)
                ability.Initialize( this, playerRef);
        }
    }

    void OnDisable()
    {
        foreach (var ability in abilitiesList)
        {
            if (ability != null)
                ability.Cleanup();
        }
    }

    public bool CanUseAbility(Ability ability)
    {
        //returns if ability is set as usable in the current system
        return currentlyActiveAbility == null || ability.abilityStat.canInterruptOthers;
    }

    public void NotifyAbilityStarted(Ability ability)
    {
        if (currentlyActiveAbility != null && ability.abilityStat.canInterruptOthers)
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

    public void AddAbility(GameObject newAbility)
    {
        // Make a copy of the prefab and attach it to the player
        //GameObject abilityObj = Instantiate(abilityPrefab, transform);

        // Grab the Ability script on that prefab
        Ability ability = newAbility.GetComponent<Ability>();
        if (ability == null)
        {
            Debug.LogError("The prefab does not have an Ability component!");
            return;
        }
        ability.Initialize(this, playerRef);
        abilitiesList.Add(ability);
    }
}
