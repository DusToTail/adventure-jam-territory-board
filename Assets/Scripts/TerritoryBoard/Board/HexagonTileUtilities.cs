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

        public static Mesh GetMesh(float hexagonSize, float hexagonHeight, HexagonTile.Config.Pivot pivot)
        {

            var dic = HexagonTileUtilities.Vertices;
            Vector3 north = dic["North"] * hexagonSize;
            Vector3 northEast = dic["NorthEast"] * hexagonSize;
            Vector3 southEast = dic["SouthEast"] * hexagonSize;
            Vector3 south = dic["South"] * hexagonSize;
            Vector3 southWest = dic["SouthWest"] * hexagonSize;
            Vector3 northWest = dic["NorthWest"] * hexagonSize;

            Vector3[] vertices = new Vector3[14];
            switch(pivot)
            {
                case HexagonTile.Config.Pivot.Center:
                    {
                        // Top face
                        vertices[0] = Vector3.up * hexagonHeight / 2f;
                        vertices[1] = north + Vector3.up * hexagonHeight / 2f;
                        vertices[2] = northEast + Vector3.up * hexagonHeight / 2f;
                        vertices[3] = southEast + Vector3.up * hexagonHeight / 2f;
                        vertices[4] = south + Vector3.up * hexagonHeight / 2f;
                        vertices[5] = southWest + Vector3.up * hexagonHeight / 2f;
                        vertices[6] = northWest + Vector3.up * hexagonHeight / 2;

                        // Bottom face
                        vertices[7] = Vector3.down * hexagonHeight / 2f;
                        vertices[8] = north + Vector3.down * hexagonHeight / 2f;
                        vertices[9] = northEast + Vector3.down * hexagonHeight / 2f;
                        vertices[10] = southEast + Vector3.down * hexagonHeight / 2f;
                        vertices[11] = south + Vector3.down * hexagonHeight / 2f;
                        vertices[12] = southWest + Vector3.down * hexagonHeight / 2f;
                        vertices[13] = northWest + Vector3.down * hexagonHeight / 2f;
                        break;
                    }
                case HexagonTile.Config.Pivot.Bottom:
                    {
                        // Top face
                        vertices[0] = Vector3.up * hexagonHeight;
                        vertices[1] = north + Vector3.up * hexagonHeight;
                        vertices[2] = northEast + Vector3.up * hexagonHeight;
                        vertices[3] = southEast + Vector3.up * hexagonHeight;
                        vertices[4] = south + Vector3.up * hexagonHeight;
                        vertices[5] = southWest + Vector3.up * hexagonHeight;
                        vertices[6] = northWest + Vector3.up * hexagonHeight;

                        // Bottom face
                        vertices[7] = Vector3.zero;
                        vertices[8] = north;
                        vertices[9] = northEast;
                        vertices[10] = southEast;
                        vertices[11] = south;
                        vertices[12] = southWest;
                        vertices[13] = northWest;
                        break;
                    }
                case HexagonTile.Config.Pivot.Top:
                    {
                        // Top face
                        vertices[0] = Vector3.zero;
                        vertices[1] = north;
                        vertices[2] = northEast;
                        vertices[3] = southEast;
                        vertices[4] = south;
                        vertices[5] = southWest;
                        vertices[6] = northWest;

                        // Bottom face
                        vertices[7] = Vector3.down * hexagonHeight;
                        vertices[8] = north + Vector3.down * hexagonHeight;
                        vertices[9] = northEast + Vector3.down * hexagonHeight;
                        vertices[10] = southEast + Vector3.down * hexagonHeight;
                        vertices[11] = south + Vector3.down * hexagonHeight;
                        vertices[12] = southWest + Vector3.down * hexagonHeight;
                        vertices[13] = northWest + Vector3.down * hexagonHeight;
                        break;
                    }
            }
            
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

            //Vector3[] normals = new Vector3[14]
            //{
            //    vertices[0] / vertices[0].magnitude,
            //    vertices[1] / vertices[1].magnitude,
            //    vertices[2] / vertices[2].magnitude,
            //    vertices[3] / vertices[3].magnitude,
            //    vertices[4] / vertices[4].magnitude,
            //    vertices[5] / vertices[5].magnitude,
            //    vertices[6] / vertices[6].magnitude,
            //    vertices[7] / vertices[7].magnitude,
            //    vertices[8] / vertices[8].magnitude,
            //    vertices[9] / vertices[9].magnitude,
            //    vertices[10] / vertices[10].magnitude,
            //    vertices[11] / vertices[11].magnitude,
            //    vertices[12] / vertices[12].magnitude,
            //    vertices[13] / vertices[13].magnitude
            //};

            Mesh mesh = new Mesh()
            {
                vertices = vertices,
                triangles = indices.ToArray(),
                name = "Hexagon Tile"
            };

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();
            mesh.Optimize();

            return mesh;
        }
    }
}

