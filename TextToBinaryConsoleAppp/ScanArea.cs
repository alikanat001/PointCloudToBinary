using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TextToBinaryConsoleAppp
{
    internal class ScanArea
    {
        public List<Vector3> Points = new List<Vector3>();       
        public List<Cell> cells = new List<Cell>();

       
        public float gridSize;
        public int downSampleRatio;
        //Number of grids
        public int Resolution;
        // max values of coordinates of Point Cloud
        public float maxX = float.MinValue;
        public float maxY = float.MinValue;
        public float maxZ = float.MinValue;
        public float minX = float.MaxValue;
        public float minY = float.MaxValue;
        public float minZ = float.MaxValue;

        private float deltaX, deltaY, deltaZ;

        // grid count in each axis
        private int gridCountX, gridCountY, gridCountZ;

        public ScanArea(float gridSize/*, int nPoints*/, int DSratio)
        {

            this.gridSize = gridSize;
            downSampleRatio = DSratio;
            
        }
        ~ScanArea()
        {         
            Points = null;
            cells = null;
        }

        public void AddPoint(Vector3 Point)
        {
            Points.Add (Point);

            //Finding max and min values of scan area using given point coordinates
            if (Point.x < minX)
                minX = Point.x;
            else if (Point.x > maxX)
                maxX = Point.x;
            if (Point.y < minY)
                minY = Point.y;
            else if (Point.y > maxY)
                maxY = Point.y;
            if (Point.z < minZ)
                minZ = Point.z;
            else if (Point.z > maxZ)
                maxZ = Point.z;

        }

        //Calculate cell position using minimum, maximum and index value of cells
        private Vector3 CalcCellPos(int index)
        {
            Vector3 pos = new Vector3
            {
                x = minX + ((index % gridCountX) + 0.5f) * gridSize,
                y = minY + ((((int)Math.Ceiling((double)(index / (gridCountX * gridCountZ)))) + 0.5f) * gridSize),
                z = minZ + (float)((Math.Ceiling((double)((index % (gridCountX * gridCountZ)) / gridCountX))+0.5)*gridSize),
            };           
            return pos;
        }

        private void CalGridValues()
        {
            deltaX = maxX - minX;
            deltaY = maxY - minY;
            deltaZ = maxZ - minZ;           

            gridCountX = (int)Math.Ceiling(deltaX / gridSize);
            gridCountY = (int)Math.Ceiling(deltaY / gridSize);
            gridCountZ = (int)Math.Ceiling(deltaZ / gridSize);

            Resolution = gridCountX * gridCountY * gridCountZ;          
        }

        //Assign points to cells based on index information
        public void AssignPointsToCells()
        {
            int i = 0;
            try
            {
                CalGridValues();
               
                for (i = 0; i < Resolution; i++)
                {
                    cells.Add(new Cell(CalcCellPos(i), gridSize, i));
                    cells[i].downSampleRatio = downSampleRatio;
                }
                
                for (i = 0; i < Points.Count; i++)
                {
                    cells[FindPointIndex(Points[i])].AddElement(Points[i]);
                }             
            }
            catch (InsufficientMemoryException ex)
            {
                Console.WriteLine("Out of memory" + ex.Data);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Null reference");
            }
            finally
            {
                Console.WriteLine("Unexpected issue with " + i);
            }

        }

        //Finding 1 dimensional index of the cell in 3D
        private int FindPointIndex(Vector3 point)
        {
            int xPart = (int)Math.Floor((point.x - minX) / gridSize);
            int yPart = (int)((Math.Floor((point.z - minZ) / gridSize)) * gridCountX);
            int zPart = (int)Math.Floor((point.y - minY) / gridSize) * gridCountX * gridCountZ;

            int index = xPart + yPart + zPart;
            if (index < 0)
            {
                index = 0;
            }

            return index;
        }
    }
}