using System.Collections.Generic;
using UnityEngine;
using TerritoryBoard.Grid2D;

namespace TerritoryBoard
{
    public class HexagonBoard : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [HideInInspector] public BoardConfig boardConfig;
        [HideInInspector] public HexagonTile.Config tileConfig;

        public Mesh CachedMesh { get; private set; }
        public Material CachedMaterial { get; private set; }
        private HexagonGrid _grid;

        public void Initialize()
        {
            // Get parameters from configs
            int width = boardConfig.width;
            int height = boardConfig.height;
            bool forwardOffsetOddHeight = boardConfig.forwardOffsetOddHeight;
            Vector3 spawnPos = boardConfig.spawnPosition;
            float interval = boardConfig.interval;

            float hexagonSize = tileConfig.size;
            float hexagonHeight = tileConfig.height;
            HexagonTile.Config.Pivot pivot = tileConfig.pivot;

            // Procedurally create mesh and material and cache them
            CachedMesh = HexagonTileUtilities.GetMesh(hexagonSize, hexagonHeight, pivot);
            CachedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));

            // Initialize each tile and assign each tile to the cell's value
            _grid = new HexagonGrid(width, height, forwardOffsetOddHeight);
            _grid.ForEach(cell =>
            {
                bool isForwardOffsetHeight = (forwardOffsetOddHeight && cell.Y % 2 == 1) || (!forwardOffsetOddHeight && cell.Y % 2 == 0);
                float _x = spawnPos.x + cell.X * HexagonTileUtilities.WidthInterval * hexagonSize + (isForwardOffsetHeight ? HexagonTileUtilities.EdgeDistance * hexagonSize : 0);
                float _y = spawnPos.y;
                float _z = spawnPos.z + cell.Y * 1.5f * hexagonSize;
                Vector3 center = new Vector3(_x,_y,_z);

                var go = Instantiate(prefab, center, Quaternion.identity, transform);
                var hexTile = go.GetComponent<HexagonTile>();
                hexTile.SetCell(cell);
                hexTile.SetDefaultWorldPosition(center);

                cell.Value = hexTile;
            });
        }

        public HexagonTile GetTileAtPosition(int x, int y)
        {
            var cell = _grid.GetCell(x, y);
            if(cell == null) { return null; }
            return cell.Value as HexagonTile;
        }

        public HexagonTile[] GetSurroundingTiles(int x, int y, bool includeSelf = true)
        {
            var cell = _grid.GetCell(x, y);
            if (cell == null) { return new HexagonTile[0]; }
            var cells = _grid.GetSurroundingCells(cell, includeSelf);
            var result = new List<HexagonTile>(cells.Length);
            foreach(var c in cells)
            {
                result.Add(c.Value as HexagonTile);
            }

            return result.ToArray();
        }

        public HexagonTile[] GetSurroundingTiles(int x, int y, int layerCount, bool includeSelf = true)
        {
            var cell = _grid.GetCell(x, y);
            if (cell == null) { return new HexagonTile[0]; }
            var cells = _grid.GetSurroundingCells(cell, layerCount, includeSelf);
            var result = new List<HexagonTile>(cells.Length);
            foreach (var c in cells)
            {
                result.Add(c.Value as HexagonTile);
            }

            return result.ToArray();
        }

        [System.Serializable]
        public struct BoardConfig
        {
            public int width;
            public int height;
            public bool forwardOffsetOddHeight;
            public Vector3 spawnPosition;
            public float interval;
        }
    }
}
