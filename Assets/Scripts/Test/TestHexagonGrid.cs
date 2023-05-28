using System.Collections;
using UnityEngine;
using Grid2D;

namespace Test
{
    public class TestHexagonGrid : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float hexagonSize;
        [SerializeField] private bool forwardOffsetOddHeight;
        [SerializeField] private Vector2Int targetPosition;
        [SerializeField] private int layerCount;
        [SerializeField] private float drawDuration = 20f;

        private HexagonGrid _grid;
        private void Start()
        {
            _grid = new HexagonGrid(width, height, forwardOffsetOddHeight);
            Debug.Log(_grid);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _grid.GetCell(x, y);
                }
            }
            //_grid.ForEach((x) => { Debug.Log(x); });
            
            {
                // Took 113.2174 ms for getting surrounding cells of up to 1000 layer count, grid 1000x1000, position (500,500)
                // Took 24.96719 ms for getting surrounding cells of up to 500 layer count, grid 500x500, position (250,250)
                // Took 3.333092 ms for getting surrounding cells of up to 100 layer count, grid 100x100, position (50,50)
                // Took 2.102852 ms for getting surrounding cells of up to 50 layer count, grid 50x50, position (25,25)

                // Took 1.806259 ms for getting surrounding cells of up to 10 layer count, grid 10x10, position (5,5)
                // Took 1.760483 ms for getting surrounding cells of up to 3 layer count, grid 10x10, position (5,5)
                // Took 1.670837 ms for getting surrounding cells of up to 2 layer count, grid 10x10, position (5,5)
                // Took 0.1335144 ms for getting surrounding cells of up to 1 layer count, grid 10x10, position (5,5)

                // Took 1.953125 ms for getting surrounding cells of up to 2 layer count, grid 100x100, position (5,5)
                // Conclusion, though quite obvious, the more layer to calculate, the slower it is


                var start = Time.realtimeSinceStartup;
                var surroundings = _grid.GetSurroundingCells(_grid.GetCell(targetPosition.x, targetPosition.y), layerCount, true);
                var duration = Time.realtimeSinceStartup - start;
                Debug.Log($"Took {duration * 1000f} ms for getting surrounding cells of up to {layerCount} layers");
            }

            {
                // Took 0.003814697 ms for getting 6 surrounding cells
                // Conclusion, since there is not much calculation and loops at all in getting 6 surrounding cells directly
                // it is recommended to use this whenever possible

                var start = Time.realtimeSinceStartup;
                var surroundings = _grid.GetSurroundingCells(_grid.GetCell(targetPosition.x, targetPosition.y), true);
                var duration = Time.realtimeSinceStartup - start;
                Debug.Log($"Took {duration * 1000f} ms for getting 6 surrounding cells");
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
                StartCoroutine(GetSurroundingsDebugCoroutine());
            }
        }

        private IEnumerator GetSurroundingsDebugCoroutine()
        {
            var surroundings = _grid.GetSurroundingCells(_grid.GetCell(targetPosition.x, targetPosition.y), layerCount, true);
            var waitHalfSecond = new WaitForSeconds(0.1f);
            foreach (var cell in surroundings)
            {
                yield return waitHalfSecond;
                bool isForwardOffsetHeight = (forwardOffsetOddHeight && cell.Y % 2 == 1) || (!forwardOffsetOddHeight && cell.Y % 2 == 0);

                // 1.732 is square root of 3.
                // 0.886 is square root of 3 divided by 2
                // 1.5 is height difference between 2 row
                Vector3 start = new Vector3(cell.X * 1.732f * hexagonSize+ (isForwardOffsetHeight ? 0.866f * hexagonSize : 0), 0.2f, cell.Y * 1.5f * hexagonSize);

                Vector3 point1 = start + new Vector3(0, 0.2f, 1) * hexagonSize;
                Vector3 point2 = start + new Vector3(0.866f, 0.2f, 0.5f) * hexagonSize;
                Vector3 point3 = start + new Vector3(0.866f, 0.2f, -0.5f) * hexagonSize;
                Vector3 point4 = start + new Vector3(0, 0.2f, -1) * hexagonSize;
                Vector3 point5 = start + new Vector3(-0.866f, 0.2f, -0.5f) * hexagonSize;
                Vector3 point6 = start + new Vector3(-0.866f, 0.2f, 0.5f) * hexagonSize;

                Debug.DrawLine(point1, point2, Color.red, drawDuration);
                Debug.DrawLine(point2, point3, Color.red, drawDuration);
                Debug.DrawLine(point3, point4, Color.red, drawDuration);
                Debug.DrawLine(point4, point5, Color.red, drawDuration);
                Debug.DrawLine(point5, point6, Color.red, drawDuration);
                Debug.DrawLine(point6, point1, Color.red, drawDuration);
            }
        }

        private void OnDrawGizmos()
        {
            if(_grid != null)
            {
                _grid.ForEach((x)=>
                {
                    Gizmos.color = Color.yellow;
                    bool isForwardOffsetHeight = (forwardOffsetOddHeight && x.Y % 2 == 1) || (!forwardOffsetOddHeight && x.Y % 2 == 0);
                    Vector3 start = new Vector3(x.X * 1.732f * hexagonSize + (isForwardOffsetHeight ? 0.866f * hexagonSize : 0), 0, x.Y * 1.5f * hexagonSize);
                    Vector3 point1 = start + new Vector3(0, 0, 1) * hexagonSize;
                    Vector3 point2 = start + new Vector3(0.866f, 0, 0.5f) * hexagonSize;
                    Vector3 point3 = start + new Vector3(0.866f, 0, -0.5f) * hexagonSize;
                    Vector3 point4 = start + new Vector3(0, 0, -1) * hexagonSize;
                    Vector3 point5 = start + new Vector3(-0.866f, 0, -0.5f) * hexagonSize;
                    Vector3 point6 = start + new Vector3(-0.866f, 0, 0.5f) * hexagonSize;

                    Gizmos.DrawLine(point1, point2);
                    Gizmos.DrawLine(point2, point3);
                    Gizmos.DrawLine(point3, point4);
                    Gizmos.DrawLine(point4, point5);
                    Gizmos.DrawLine(point5, point6);
                    Gizmos.DrawLine(point6, point1);
                });
            }
        }
    }
}

