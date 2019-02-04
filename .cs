using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScanArea
{

    public Vector3[] Points;
    public List<Cell> cells = new List<Cell>();

    public int nPoints;
    public float gridSize;
    public int downSampleRatio = 75;
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

    //GUI


    public ScanArea(int gridSize, int nPoints, int DSratio)
    {

        this.gridSize = gridSize;
        this.downSampleRatio = DSratio;
        this.nPoints = nPoints;
        this.Points = new Vector3[nPoints];
    }
    ~ScanArea()
    {
        Debug.LogError("Scan Area Destructor is called");
        //this.Points = null;
        //this.cells = null;
    }

    //public void AddPoint(Vector3 Point)
    //{
    //    Points.Add(Point);

    //    //Finding max and min values of scan area using given point coordinates
    //    if (Point.x < minX)
    //        minX = Point.x;
    //    else if (Point.x > maxX)
    //        maxX = Point.x;
    //    if (Point.y < minY)
    //        minY = Point.y;
    //    else if (Point.y > maxY)
    //        maxY = Point.y;
    //    if (Point.z < minZ)
    //        minZ = Point.z;
    //    else if (Point.z > maxZ)
    //        maxZ = Point.z;

    //}
    public void AddPoint(Vector3 Point, int index)
    {
        Points[index] = Point;

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
    private Vector3 CalcCellPos(int index)
    {
        Vector3 pos;
        pos.x = minX + (((index % gridCountZ) + 0.5f) * gridSize);
        pos.y = minY + ((Mathf.Ceil(index / (gridCountX * gridCountZ)) + 0.5f) * gridSize);
        pos.z = minZ + ((Mathf.CeilToInt((index % (gridCountX * gridCountZ)) / gridCountX) + 0.5f) * gridSize);
        //Debug.Log("Cell position is" + pos);
        return pos;
    }
    // Calculating required values for Grid
    private void CalGridValues()
    {
        deltaX = maxX - minX;
        deltaY = maxY - minY;
        deltaZ = maxZ - minZ;

        gridCountX = Mathf.CeilToInt(deltaX / gridSize);
        gridCountY = Mathf.CeilToInt(deltaY / gridSize);
        gridCountZ = Mathf.CeilToInt(deltaZ / gridSize);

        Resolution = gridCountX * gridCountY * gridCountZ;
        Debug.Log("Resolution is" + Resolution + "grid count x is" + gridCountX + "gridcount y is" + gridCountY + "grid count z is" + gridCountZ);
    }
    // Creating grid cells and assigning points to corresponding cell
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
            Debug.Log("Cells are allocated" + cells.Count);
            for (i = 0; i < Points.Length; i++)
            {
                //Debug.Log(FindPointIndex(Points[i])+"  "+Points[i]);
                //Debug.Log(cells[FindPointIndex(Points[i])]);
                cells[FindPointIndex(Points[i])].AddElement(Points[i]);



            }

            Debug.Log("Points are assigned");
            //Points = null;
            //GC.Collect();

        }
        catch (InsufficientMemoryException ex)
        {
            Debug.LogError("Out of memory" + ex.Data);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Null reference");
        }
        finally
        {
            Debug.LogWarning("Unexpected issue with " + i);
        }

    }

    private int FindPointIndex(Vector3 point)
    {

        int index = Mathf.FloorToInt((point.x - minX) / gridSize) + ((Mathf.FloorToInt((point.z - minZ) / gridSize)) * gridCountX)
               + Mathf.FloorToInt((point.y - minY) / gridSize) * gridCountX * gridCountZ;
        if (index < 0)
        {
            index = 0;
        }

        return index;
    }



}
