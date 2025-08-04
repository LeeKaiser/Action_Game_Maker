using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerEvent", menuName = "Scriptable Objects/PlayerEvent")]
public class PlayerEvent : ScriptableObject
{
    //other player objects subscribe to this event
    public event System.Action EventActivated;

    //name of PlayerEvent
    public string EventName;

    //when this object is initialized, subscribe DetectPlayerEvent to an event in parent script
    //add itself to the playerEventManager's dictionary
    //init (script that holds this PlayerEvent as param)
    public void Init(PlayerEventManager playerEventManage)
    {
        playerEventManage.AddPlayerEventToDict(this);
    }
    
    //when a certain player event occurs, activate EventActivated event
    public void DetectPlayerEvent()
    {
        EventActivated?.Invoke();
        Debug.Log("Event Detected");
    }

    public void SubscribeToEvent(Action callback)
    {
        EventActivated += callback;
    }
}
