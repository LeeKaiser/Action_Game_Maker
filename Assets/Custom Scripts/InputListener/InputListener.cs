using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using StarterAssets;

public class InputListener : MonoBehaviour
{
    protected PlayerInput playerInput;

    protected Dictionary<string, InputAction> inputDict = new Dictionary<string, InputAction>();

    protected GameObject playerRef;
    
    void Awake()
    {
        //initialize playerinput
        playerInput = GetComponent<PlayerInput>();

        foreach (var actionMap in playerInput.actions.actionMaps)
        {
            //Debug.Log($"Action Map: {actionMap.name}");

            // Iterate through all Actions within the current Action Map
            foreach (var action in actionMap.actions)
            {
                Debug.Log($"  Action: {action.name}, Type: {action.type}");
                inputDict[action.name] = action;
            }
        }

        playerRef = transform.parent.gameObject;
    }
    

    // Update is called once per frame
    void Update()
    {
        //make a child class of this class and override update
    }
}
