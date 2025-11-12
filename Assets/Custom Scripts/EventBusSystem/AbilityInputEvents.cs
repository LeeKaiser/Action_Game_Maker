using UnityEngine;
using StarterAssets;

namespace AbilityInputEvents
{
    //attack1 input
    public struct PlayerStartAttack1{ //when first pressed
        public GameObject PlayerIdentity;

        public PlayerStartAttack1(GameObject player)
        {
            PlayerIdentity = player;
        }
    }

    public struct PlayerHoldAttack1{ //when held
        public GameObject PlayerIdentity;

        public PlayerHoldAttack1(GameObject player)
        {
            PlayerIdentity = player;
        }
    }

    public struct PlayerReleaseAttack1{ //when released
        public GameObject PlayerIdentity;

        public PlayerReleaseAttack1(GameObject player)
        {
            PlayerIdentity = player;
        }
    }



    //ability1 input
    public struct PlayerStartAbility1{ //when first pressed
        public GameObject PlayerIdentity;

        public PlayerStartAbility1(GameObject player)
        {
            PlayerIdentity = player;
        }
    }

    public struct PlayerHoldAbility1{ //when held
        public GameObject PlayerIdentity;

        public PlayerHoldAbility1(GameObject player)
        {
            PlayerIdentity = player;
        }
    }

    public struct PlayerReleaseAbility1{ //when released
        public GameObject PlayerIdentity;

        public PlayerReleaseAbility1(GameObject player)
        {
            PlayerIdentity = player;
        }
    }
}