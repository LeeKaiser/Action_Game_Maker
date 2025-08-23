using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<Ability> abilitiesList;
    private PlayerInput playerInput;
    private Ability currentlyActiveAbility;
    public GameObject playerRef;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        foreach (var ability in abilitiesList)
        {
            if (ability != null)
                ability.Initialize(playerInput, this, playerRef);
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

    public void AddAbility(GameObject abilityPrefab)
    {
        // Make a copy of the prefab and attach it to the player
        GameObject abilityObj = Instantiate(abilityPrefab, transform);

        // Grab the Ability script on that prefab
        Ability ability = abilityObj.GetComponent<Ability>();
        if (ability == null)
        {
            Debug.LogError("The prefab does not have an Ability component!");
            return;
        }
        ability.Initialize(playerInput, this, playerRef);
        abilitiesList.Add(ability);
    }
}
