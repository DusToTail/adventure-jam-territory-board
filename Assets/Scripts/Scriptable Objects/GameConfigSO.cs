using UnityEngine;
using TerritoryBoard;

[CreateAssetMenu(fileName ="GameConfigDefault", menuName = "TerritoryBoard/GameConfig")]
public class GameConfigSO : ScriptableObject
{
    public HexagonBoard.BoardConfig boardConfig;
    public HexagonTile.Config tileConfig;
}
