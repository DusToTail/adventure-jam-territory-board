using UnityEngine;
using Grid2D;

namespace Test
{
    public class TestGrid2D : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        private Grid2D.Grid _grid;

        private void Start()
        {
            _grid = new Grid2D.Grid(width, height);
            Debug.Log(_grid);
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    _grid.GetCell(x, y);
                }
            }
            _grid.ForEach((x) => { Debug.Log(x); });
        }
    }
}
