using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 cellPos = new Vector3(0, 0, 0);
    public List<Vector3> cellPoints = new List<Vector3>();
    public List<Vector3> cellPointsDownSampled = new List<Vector3>();
    public float gridSize;
    public int downSampleRatio;
    private int count = 0;
    private int index;
    private Dictionary<int, List<Vector3>> DownSampleDict = new Dictionary<int, List<Vector3>>();

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
        this.gridSize = size;
        this.cellPos = pos;
        this.index = index;
        this.CellmaxX = cellPos.x + (gridSize / 2);
        this.CellminX = cellPos.x - (gridSize / 2);
        this.CellmaxY = cellPos.y + (gridSize / 2);
        this.CellminY = cellPos.y - (gridSize / 2);
        this.CellmaxZ = cellPos.z + (gridSize / 2);
        this.CellminZ = cellPos.z - (gridSize / 2);

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
            DownSampleDict[FindPointIndexInCell(Point)].Add(Point);

        }

        catch (NullReferenceException ex)
        {
            Debug.Log("Null reference");
        }
        catch (KeyNotFoundException ex)
        {
            Debug.LogWarning("cell position is" + cellPos);
            Debug.LogWarning("Point pos is" + Point);
            Debug.LogWarning("key is " + FindPointIndexInCell(Point));
            Debug.LogError("Key " + FindPointIndexInCell(Point) + " not found !!");
            Application.Quit();
        }
    }
    //private void calculateDict()
    //{
    //    for(int i =0;i<cellPoints.Count;i++)
    //    {
    //        DownSampleDict[FindPointIndexInCell(cellPoints[i])].Add(cellPoints[i]);
    //    }
    //}
    private int FindPointIndexInCell(Vector3 point)
    {

        int index = Mathf.FloorToInt((point.x - CellminX) / ((float)gridSize / gridCount)) + ((Mathf.FloorToInt((point.z - CellminZ) / ((float)gridSize / gridCount))) * gridCount)
               + Mathf.FloorToInt((point.y - CellminY) / ((float)gridSize / gridCount)) * gridCount * gridCount;
        if (index < 0)
        {
            index = 0;
        }
        //Debug.LogWarning(" cell position is " + cellPos + "cell index is" + this.index);
        //Debug.LogWarning("cell x values" + CellminX + " " + CellmaxX);
        //Debug.LogWarning("cell y values" + CellminY + " " + CellmaxY);
        //Debug.LogWarning("cell z values" + CellminZ + " " + CellmaxZ);
        //Debug.Log("Point is " + point);
        //Debug.Log("down sample cell size is" + ((float)gridSize / gridCount));
        //Debug.Log(" x part " + Mathf.FloorToInt((point.x - CellminX) / ((float)gridSize / gridCount)));
        //Debug.Log("z part" + ((Mathf.FloorToInt((point.z - CellminZ) / ((float)gridSize / gridCount))) * gridCount));
        //Debug.Log("y part " + Mathf.FloorToInt((point.y - CellminY) / ((float)gridSize / gridCount)) * gridCount * gridCount);
        //Debug.Log(" resulting index" + index);
        return index;
    }
    public void CalcDownSampledPoints()
    {

        int i = 0;
        //Debug.Log("count for " + count+" "+this.cellPoints.Count);

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