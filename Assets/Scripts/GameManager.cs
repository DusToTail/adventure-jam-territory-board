using System;
using UnityEngine;
using TerritoryBoard;
using TerritoryBoard.TurnBasedSystem;
using TerritoryBoard.Mechanics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameConfigSO gameConfigSO;
    public HexagonBoard Board { get; private set; }
    public TurnBasedEngine TurnEngine { get; private set; }
    public TeamManager TeamManager { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        {
            Board = FindObjectOfType<HexagonBoard>();
            if (gameConfigSO == null)
            {
                throw new Exception("GameManager: game config is null");
            }
            Board.boardConfig = gameConfigSO.boardConfig;
            Board.tileConfig = gameConfigSO.tileConfig;
        }

        {
            TurnEngine = TurnBasedEngine.Instance;
            TurnEngine.SetTurnController(new PerActorTurnController());
            TurnEngine.SetActorManager(new ActorManager());
            TurnEngine.Rebind();
        }

        {
            TeamManager = TeamManager.Instance;
        }
    }
}
