using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    [SerializeField] private TeamManager[] TeamsInMatch;
}
