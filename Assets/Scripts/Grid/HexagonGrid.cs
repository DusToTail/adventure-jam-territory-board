using System.Text;
using System.Collections.Generic;

namespace Grid2D
{
    public class HexagonGrid : Grid
    {
        public enum Direction
        {
            Left,
            UpperLeft,
            UpperRight,
            Right,
            BottomRight,
            BottomLeft
        }

        public bool ForwardOffsetOddHeight { get; private set; }

        public HexagonGrid(int width, int height, bool forwardOffsetOddHeight = true) : base(width, height)
        {
            ForwardOffsetOddHeight = forwardOffsetOddHeight;
        }

        public Cell[] GetCellsAtLayerIndex(Cell cell, int layerIndex, bool includeSelf)
        {
            if (cell == null || layerIndex < 0)
            {
                return new Cell[0];
            }
            if (layerIndex == 0)
            {
                return includeSelf ? new Cell[] { cell } : new Cell[0];
            }
            if (layerIndex == 1)
            {
                return GetSurroundingCells(cell, includeSelf);
            }

            var result = new List<Cell>(13);
            if (includeSelf) { result.Add(cell); }

            bool isForwardOffsetHeight = (ForwardOffsetOddHeight && cell.Y % 2 == 1) || (!ForwardOffsetOddHeight && cell.Y % 2 == 0);

            {
                // Left
                var outerMost = GetPositionAtDirection((cell.X, cell.Y), layerIndex, isForwardOffsetHeight, Direction.Left);
                for (int j = 0; j < layerIndex; j++)
                {
                    var pos = GetPositionAtDirection(outerMost, j, isForwardOffsetHeight, Direction.UpperRight);
                    var sub = GetCell(pos.Item1, pos.Item2);
                    if (sub != null)
                    {
                        result.Add(sub);
                    }
                }
            }

            {
                // Upper Left
                var outerMost = GetPositionAtDirection((cell.X, cell.Y), layerIndex, isForwardOffsetHeight, Direction.UpperLeft);
                bool isForwardOffset = (isForwardOffsetHeight && layerIndex % 2 == 0) || (!isForwardOffsetHeight && layerIndex % 2 == 1);
                for (int j = 0; j < layerIndex; j++)
                {
                    var pos = GetPositionAtDirection(outerMost, j, isForwardOffset, Direction.Right);
                    var sub = GetCell(pos.Item1, pos.Item2);
                    if (sub != null)
                    {
                        result.Add(sub);
                    }
                }
            }

            {
                // Upper Right
                var outerMost = GetPositionAtDirection((cell.X, cell.Y), layerIndex, isForwardOffsetHeight, Direction.UpperRight);
                bool isForwardOffset = (isForwardOffsetHeight && layerIndex % 2 == 0) || (!isForwardOffsetHeight && layerIndex % 2 == 1);
                for (int j = 0; j < layerIndex; j++)
                {
                    var pos = GetPositionAtDirection(outerMost, j, isForwardOffset, Direction.BottomRight);
                    var sub = GetCell(pos.Item1, pos.Item2);
                    if (sub != null)
                    {
                        result.Add(sub);
                    }
                }
            }

            {
                // Right
                var outerMost = GetPositionAtDirection((cell.X, cell.Y), layerIndex, isForwardOffsetHeight, Direction.Right);
                for (int j = 0; j < layerIndex; j++)
                {
                    var pos = GetPositionAtDirection(outerMost, j, isForwardOffsetHeight, Direction.BottomLeft);
                    var sub = GetCell(pos.Item1, pos.Item2);
                    if (sub != null)
                    {
                        result.Add(sub);
                    }
                }
            }

            {
                // Bottom Right
                var outerMost = GetPositionAtDirection((cell.X, cell.Y), layerIndex, isForwardOffsetHeight, Direction.BottomRight);
                bool isForwardOffset = (isForwardOffsetHeight && layerIndex % 2 == 0) || (!isForwardOffsetHeight && layerIndex % 2 == 1);
                for (int j = 0; j < layerIndex; j++)
                {
                    var pos = GetPositionAtDirection(outerMost, j, isForwardOffset, Direction.Left);
                    var sub = GetCell(pos.Item1, pos.Item2);
                    if (sub != null)
                    {
                        result.Add(sub);
                    }
                }
            }

            {
                // Bottom Left
                var outerMost = GetPositionAtDirection((cell.X, cell.Y), layerIndex, isForwardOffsetHeight, Direction.BottomLeft);
                bool isForwardOffset = (isForwardOffsetHeight && layerIndex % 2 == 0) || (!isForwardOffsetHeight && layerIndex % 2 == 1);
                for (int j = 0; j < layerIndex; j++)
                {
                    var pos = GetPositionAtDirection(outerMost, j, isForwardOffset, Direction.UpperLeft);
                    var sub = GetCell(pos.Item1, pos.Item2);
                    if (sub != null)
                    {
                        result.Add(sub);
                    }
                }
            }
            return result.ToArray();
        }

        public Cell[] GetSurroundingCells(Cell cell, int layerCount, bool includeSelf)
        {
            if (cell == null || layerCount <= 0)
            {
                return new Cell[0];
            }
            var result = new List<Cell>(7);
            if (includeSelf) { result.Add(cell); }
            for (int i = 1; i < layerCount; i++)
            {
                result.AddRange(GetCellsAtLayerIndex(cell, i, false));
            }
            return result.ToArray();
        }

        public Cell[] GetSurroundingCells(Cell cell, bool includeSelf)
        {
            if(cell == null)
            {
                return new Cell[0];
            }
            var result = new List<Cell>(7);
            if (includeSelf) { result.Add(cell); }

            int x = cell.X;
            int y = cell.Y;
            bool isForwardOffsetHeight = (ForwardOffsetOddHeight && y % 2 == 1) || (!ForwardOffsetOddHeight && y % 2 == 0);

            var left = GetCellAtDirection(cell, 1, isForwardOffsetHeight, Direction.Left);
            var upperLeft = GetCellAtDirection(cell, 1, isForwardOffsetHeight, Direction.UpperLeft);
            var upperRight = GetCellAtDirection(cell, 1, isForwardOffsetHeight, Direction.UpperRight);
            var right = GetCellAtDirection(cell, 1, isForwardOffsetHeight, Direction.Right);
            var bottomRight = GetCellAtDirection(cell, 1, isForwardOffsetHeight, Direction.BottomRight);
            var bottomLeft = GetCellAtDirection(cell, 1, isForwardOffsetHeight, Direction.BottomLeft);

            if (left != null) { result.Add(left); }
            if (upperLeft != null) { result.Add(upperLeft); }
            if (upperRight != null) { result.Add(upperRight); }
            if (right != null) { result.Add(right); }
            if (bottomRight != null) { result.Add(bottomRight); }
            if (bottomLeft != null) { result.Add(bottomLeft); }
            
            return result.ToArray();
        }

        public Cell GetCellAtDirection(Cell cell, int layersAway, bool isForwardOffsetHeight, Direction direction)
        {
            if (cell == null) { return null; }
            if (layersAway == 0)
            {
                return cell;
            }
            int x = cell.X;
            int y = cell.Y;
            int l = layersAway;
            Cell result = null;

            if (isForwardOffsetHeight)
            {
                switch(direction)
                {
                    case Direction.Left:
                        {
                            result = GetCell(x - l, y);
                            break;
                        }
                    case Direction.UpperLeft:
                        {
                            result = GetCell(x - l / 2, y + l);
                            break;
                        }
                    case Direction.UpperRight:
                        {
                            result = GetCell(x + (l + 1) / 2, y + l);
                            break;
                        }
                    case Direction.Right:
                        {
                            result = GetCell(x + l, y);
                            break;
                        }
                    case Direction.BottomRight:
                        {
                            result = GetCell(x + (l + 1) / 2, y - l);
                            break;
                        }
                    case Direction.BottomLeft:
                        {
                            result = GetCell(x - l / 2, y - l);
                            break;
                        }
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Left:
                        {
                            result = GetCell(x - l, y);
                            break;
                        }
                    case Direction.UpperLeft:
                        {
                            result = GetCell(x - (l + 1)/ 2, y + l);
                            break;
                        }
                    case Direction.UpperRight:
                        {
                            result = GetCell(x + (l - 1) / 2, y + l);
                            break;
                        }
                    case Direction.Right:
                        {
                            result = GetCell(x + l, y);
                            break;
                        }
                    case Direction.BottomRight:
                        {
                            result = GetCell(x + (l - 1) / 2, y - l);
                            break;
                        }
                    case Direction.BottomLeft:
                        {
                            result = GetCell(x - (l + 1) / 2, y - l);
                            break;
                        }
                }
            }

            return result;
            
        }
        
        public (int, int) GetPositionAtDirection((int, int) position, int layersAway, bool isForwardOffsetHeight, Direction direction)
        {
            if (layersAway == 0)
            {
                return position;
            }
            int x = position.Item1;
            int y = position.Item2;
            int l = layersAway;
            (int,int) result = (-1,-1);

            if (isForwardOffsetHeight)
            {
                switch (direction)
                {
                    case Direction.Left:
                        {
                            result = (x - l, y);
                            break;
                        }
                    case Direction.UpperLeft:
                        {
                            result = (x - l / 2, y + l);
                            break;
                        }
                    case Direction.UpperRight:
                        {
                            result = (x + (l + 1) / 2, y + l);
                            break;
                        }
                    case Direction.Right:
                        {
                            result = (x + l, y);
                            break;
                        }
                    case Direction.BottomRight:
                        {
                            result = (x + (l + 1) / 2, y - l);
                            break;
                        }
                    case Direction.BottomLeft:
                        {
                            result = (x - l / 2, y - l);
                            break;
                        }
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Left:
                        {
                            result = (x - l, y);
                            break;
                        }
                    case Direction.UpperLeft:
                        {
                            result = (x - (l + 1) / 2, y + l);
                            break;
                        }
                    case Direction.UpperRight:
                        {
                            result = (x + l / 2, y + l);
                            break;
                        }
                    case Direction.Right:
                        {
                            result = (x + l, y);
                            break;
                        }
                    case Direction.BottomRight:
                        {
                            result = (x + l / 2, y - l);
                            break;
                        }
                    case Direction.BottomLeft:
                        {
                            result = (x - (l + 1) / 2, y - l);
                            break;
                        }
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Grid2D {Width}x{Height}");
            for (int i = Height - 1; i >= 0; i--)
            {
                string offset = "\n";
                bool isForwardOffsetHeight = (ForwardOffsetOddHeight && i % 2 == 1) || (!ForwardOffsetOddHeight && i % 2 == 0);
                if (isForwardOffsetHeight)
                {
                    offset += '\t';
                }
                stringBuilder.Append(offset);
                stringBuilder.AppendJoin("\t\t", Array.GetRange(i * Width, Width));
            }
            return stringBuilder.ToString();
        }
    }
}
