using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private PlayableCharCore[] TeamMembers;

    public LayerMask teamLayer;

    public int SpawnTime;
}
