using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


// Listens for events from player's PlayableCharacterCore and AbilityManager
// when it successfully listens to an event, activate an event representing the player's event
// other objects that want to listen to events from player can simply listen to it from a single source (PlayerEventManager)

public class PlayerEventManager : MonoBehaviour
{
    public Dictionary<string, List<PlayerEvent>> PlayerEventDict = new Dictionary<string, List<PlayerEvent>>();

    public void AddPlayerEventToDict(PlayerEvent newPlayerEvent)
    {
        //if there is already a same type of player event in dict
        if (PlayerEventDict.ContainsKey(newPlayerEvent.EventName))
        {
            PlayerEventDict[newPlayerEvent.EventName].Add(newPlayerEvent);
        }
        else
        {
            //otherwise add a new player event
            PlayerEventDict[newPlayerEvent.EventName] = new List<PlayerEvent> {newPlayerEvent};
        }
        
    }

    public void SubscribeToEvent(string targetEvent, System.Action handler)
    {
        if (PlayerEventDict.ContainsKey(targetEvent))
        {
            foreach (var playerEvent in PlayerEventDict[targetEvent])
            {
                playerEvent.SubscribeToEvent(handler);
            }
        }
    }


}
