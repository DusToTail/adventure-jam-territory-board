using System;
using System.Collections.Generic;
using System.Text;

namespace Grid2D
{
    public class Grid2D<T> where T : class
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Cell> Array { get; private set; }

        public Grid2D(int width, int height)
        {
            if(width <= 0 || height <= 0)
            {
                throw new ArgumentException($"Grid2D: {width} and {height} must be positive integer!");
            }
            Width = width;
            Height = height;
            int total = width * height;
            Array = new List<Cell>(total);
            for(int i = 0; i < total; i++)
            {
                int x = (i % width);
                int y = (i / width);
                Array.Add(new Cell(x, y));
            }
        }

        public Cell GetCell(int x, int y)
        {
            if (x <= 0 || y <= 0 || x > Width - 1 || y > Height - 1)
            {
                return null;
            }
            return Array[y*Width + x];
        }

        public void ForEach(Action<Cell> action)
        {
            Array.ForEach(action);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Grid2D {Width}x{Height}");
            for(int i = Height-1; i >= 0; i--)
            {
                stringBuilder.Append('\n');
                stringBuilder.AppendJoin('\t', Array.GetRange(i*Width, Width));
            }
            return stringBuilder.ToString();
        }

        public class Cell
        {
            public T Value { get; set; }
            public int X { get; private set; }
            public int Y { get; private set; }

            public Cell(int x, int y, T value = null)
            {
                Value = value;
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"({X},{Y},{Value})";
            }
        }
    }
}
