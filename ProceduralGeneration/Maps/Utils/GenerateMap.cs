using Godot;
using System;

public abstract class GenerateMap : Node
{
    
    public abstract void Generate2DMap(int dimension);
    public abstract string[,] Generate2DDemoMap(int dimension);

    protected string[,] Generate2DGrid(int dimension)
    {
        var mapGrid = new string[dimension, dimension];
        for (var i = 0; i < dimension; i++)
        {
            for (var j = 0; j < dimension; j++)
            {
                mapGrid[i, j] = ".";
            }
        }
        return mapGrid;
    }

    protected static void Print2DGrid(string[,] grid)
    {
        var row = grid.GetLength(0);
        var column = grid.GetLength(0);
        var printingRow = "";
        
        for (var i = 0; i < row; i++)
        {
            for (var j = 0; j < column; j++)
            {
                var cellString = grid[i,j].ToString();
                printingRow += cellString;
            }
            GD.Print(printingRow);
            printingRow = "";
        }   
    }
}
