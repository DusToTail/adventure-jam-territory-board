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
                //var surroundings = _grid.GetSurroundings(_grid.GetCell(targetPosition.x, targetPosition.y), true);
                //foreach (var cell in surroundings)
                //{
                //    Debug.Log(cell);
                //}
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

