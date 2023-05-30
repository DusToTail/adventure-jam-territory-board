using System.Collections.Generic;
using UnityEngine;
using Grid2D;

namespace Board
{
    public class HexagonBoard : MonoBehaviour
    {
        [SerializeField] BoardConfig boardConfig;
        [SerializeField] TileConfig tileConfig;

        private HexagonGrid _grid;
        private Mesh _cachedHexagonMesh;
        private Material _cachedMaterial;

#if UNITY_EDITOR
        [SerializeField] private bool initializeOnStart;
        private void Start()
        {
            if(initializeOnStart)
                Initialize();
        }
#endif

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

            // Procedurally create mesh and material and cache them
            // Not ideal way of getting mesh and material.
            // TODO: Can just simply create a mesh model and also assign material directly in Editor
            _cachedHexagonMesh = HexagonTileUtilities.GetMesh(hexagonSize, hexagonHeight);
            _cachedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));

            // Initialize each tile and assign each tile to the cell's value
            _grid = new HexagonGrid(width, height, forwardOffsetOddHeight);
            _grid.ForEach(cell =>
            {
                bool isForwardOffsetHeight = (forwardOffsetOddHeight && cell.Y % 2 == 1) || (!forwardOffsetOddHeight && cell.Y % 2 == 0);
                float _x = spawnPos.x + cell.X * HexagonTileUtilities.WidthInterval * hexagonSize + (isForwardOffsetHeight ? HexagonTileUtilities.EdgeDistance * hexagonSize : 0);
                float _y = spawnPos.y;
                float _z = spawnPos.z + cell.Y * 1.5f * hexagonSize;
                Vector3 center = new Vector3(_x,_y,_z);

                // TODO: Refactor as this can just be a prefab with all the settings configured for components beforehand
                var go = new GameObject($"{cell.X},{cell.Y}", new System.Type[] { typeof(MeshFilter), typeof(MeshRenderer), typeof(HexagonTile) });
                go.transform.position = center;
               
                var meshFilter = go.GetComponent<MeshFilter>();
                meshFilter.sharedMesh = _cachedHexagonMesh;
                var renderer = go.GetComponent<MeshRenderer>();
                renderer.sharedMaterial = _cachedMaterial;
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

        [System.Serializable]
        public struct TileConfig
        {
            public float size;
            public float height;
        }
    }
}
