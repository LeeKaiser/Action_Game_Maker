using UnityEngine;
using AbilityInputEvents;
using MovementInputEvents;


public class TestPlayerInputListener : InputListener
{
    // Update is called once per frame
    void Update()
    {
        if (inputDict["Ability1"].WasPerformedThisFrame())
        {
            EventBus<PlayerStartAbility1>.Invoke(new PlayerStartAbility1(playerRef));
        }
        if (inputDict["Ability1"].IsPressed())
        {
            EventBus<PlayerHoldAbility1>.Invoke(new PlayerHoldAbility1(playerRef));
        }
        if (inputDict["Ability1"].WasReleasedThisFrame() )
        {
            EventBus<PlayerReleaseAbility1>.Invoke(new PlayerReleaseAbility1(playerRef));
        }
    }
}
