using UnityEngine;
using TerritoryBoard;
using TerritoryBoard.TurnController;

[CreateAssetMenu(fileName ="GameConfigDefault", menuName = "TerritoryBoard/GameConfig")]
public class GameConfigSO : ScriptableObject
{
    public HexagonBoard.BoardConfig boardConfig;
    public HexagonTile.Config tileConfig;
    public TurnController.Config turnControllerConfig;
}
