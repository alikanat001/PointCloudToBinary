using System.Collections.Generic;
using System;
namespace TextToBinaryConsoleAppp
{
    internal class Cell
    {
        public Vector3 cellPos = new Vector3(0, 0, 0);
        public List<Vector3> cellPoints = new List<Vector3>();
        public List<Vector3> cellPointsDownSampled = new List<Vector3>();
        public float gridSize;
        public int downSampleRatio;
       
        private int index;
        private Dictionary<int, List<Vector3>> DownSampleDict = new Dictionary<int, List<Vector3>>();

        //Cell min max values
        private float CellmaxX = float.MinValue;
        private float CellmaxY = float.MinValue;
        private float CellmaxZ = float.MinValue;
        private float CellminX = float.MaxValue;
        private float CellminY = float.MaxValue;
        private float CellminZ = float.MaxValue;
        private readonly int cellGridRes = 125;
        private readonly int gridCount = 5;

        public Cell(Vector3 pos, float size, int index)
        {
            gridSize = size;
            cellPos = pos;
            this.index = index;
            CellmaxX = cellPos.x + (gridSize / 2);
            CellminX = cellPos.x - (gridSize / 2);
            CellmaxY = cellPos.y + (gridSize / 2);
            CellminY = cellPos.y - (gridSize / 2);
            CellmaxZ = cellPos.z + (gridSize / 2);
            CellminZ = cellPos.z - (gridSize / 2);

            for (int i = 0; i < cellGridRes; i++)
            {
                DownSampleDict.Add(i, new List<Vector3>());
            }
        }

        public void AddElement(Vector3 Point)
        {
            try
            {
                cellPoints.Add(Point);
                int index = FindPointIndexInCell(Point);
                if (index >= 0 && index < 125)
                {
                    DownSampleDict[index].Add(Point);
                }               
            }

            catch (NullReferenceException ex)
            {
                Console.WriteLine("Null reference with" +Point);
            }
            catch (KeyNotFoundException ex)
            {               
                Console.WriteLine("Key " + " not found !!");
            }
        }

        //Find 1D index of each cell in the cell for downsampling
        private int FindPointIndexInCell(Vector3 point)
        {
            int xPart = (int)Math.Floor((point.x - CellminX) / (gridSize / gridCount));
            int yPart = (int)((Math.Floor((point.z - CellminZ) / (gridSize / gridCount))) * gridCount);
            int zPart = (int)Math.Floor((point.y - CellminY) / (gridSize / gridCount)) * gridCount * gridCount;
            int index = xPart + yPart + zPart;
            
            return index;
        }

        public void CalcDownSampledPoints()
        {

            int i = 0;
            if (cellPointsDownSampled.Count == 0)
            {
                foreach (var list in DownSampleDict.Values)
                {

                    while (i < list.Count)
                    {
                        cellPointsDownSampled.Add(list[i]);
                        i += downSampleRatio;
                    }
                    i = 0;
                }
            }

        }
    }
}