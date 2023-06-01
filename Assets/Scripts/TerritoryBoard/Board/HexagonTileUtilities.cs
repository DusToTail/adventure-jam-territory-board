using System.Collections.Generic;
using UnityEngine;

namespace TerritoryBoard
{
    public static class HexagonTileUtilities
    {
        public static readonly Dictionary<string, Vector3> Vertices = new Dictionary<string, Vector3>()
        {
            { "North", new Vector3(0, 0, 1)},
            { "NorthEast", new Vector3(0.866f, 0, 0.5f)},
            { "SouthEast", new Vector3(0.866f, 0, -0.5f)},
            { "South", new Vector3(0, 0, -1)},
            { "SouthWest", new Vector3(-0.866f, 0, -0.5f)},
            { "NorthWest", new Vector3(-0.866f, 0, 0.5f)},
        };
        public static readonly float WidthInterval = 1.732f;
        public static readonly float HeightInterval = 1.5f;
        public static readonly float EdgeDistance = 0.866f;

        public static Mesh GetMesh(float hexagonSize, float hexagonHeight)
        {

            var dic = HexagonTileUtilities.Vertices;
            Vector3 north = dic["North"] * hexagonSize;
            Vector3 northEast = dic["NorthEast"] * hexagonSize;
            Vector3 southEast = dic["SouthEast"] * hexagonSize;
            Vector3 south = dic["South"] * hexagonSize;
            Vector3 southWest = dic["SouthWest"] * hexagonSize;
            Vector3 northWest = dic["NorthWest"] * hexagonSize;
            Vector3[] vertices = new Vector3[14]
            {
                // Top face
                Vector3.up * hexagonHeight,
                north + Vector3.up * hexagonHeight,
                northEast + Vector3.up * hexagonHeight,
                southEast + Vector3.up * hexagonHeight,
                south + Vector3.up * hexagonHeight,
                southWest + Vector3.up * hexagonHeight,
                northWest + Vector3.up * hexagonHeight,

                // Bottom face
                Vector3.down * hexagonHeight,
                north + Vector3.down * hexagonHeight,
                northEast + Vector3.down * hexagonHeight,
                southEast + Vector3.down * hexagonHeight,
                south + Vector3.down * hexagonHeight,
                southWest + Vector3.down * hexagonHeight,
                northWest + Vector3.down * hexagonHeight
            };
            List<int> indices = new List<int>(72); // 24 tris, each tris require 3 index

            // Top tris except the last tri
            for (int i = 1; i < 6; i++)
            {
                indices.Add(0);
                indices.Add(i);
                indices.Add(i + 1);
            }
            // Last tri
            indices.Add(0);
            indices.Add(6);
            indices.Add(1);
            // Bottom tris except the last tri
            for (int i = 8; i < 13; i++)
            {
                indices.Add(7);
                indices.Add(i + 1);
                indices.Add(i);
            }
            // Last tri
            indices.Add(7);
            indices.Add(8);
            indices.Add(13);
            // Side tris except the last 2 tris
            for (int i = 1; i < 6; i++)
            {
                indices.Add(i);
                indices.Add(i + 7);
                indices.Add(i + 1);

                indices.Add(i + 1);
                indices.Add(i + 7);
                indices.Add(i + 8);
            }
            // Last 2 tris
            indices.Add(6);
            indices.Add(13);
            indices.Add(1);

            indices.Add(1);
            indices.Add(13);
            indices.Add(8);

            Mesh mesh = new Mesh()
            {
                vertices = vertices,
                triangles = indices.ToArray(),
                name = "Hexagon Tile"
            };
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();

            return mesh;
        }
    }
}

