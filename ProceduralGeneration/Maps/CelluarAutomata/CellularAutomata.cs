using System;

public class CellularAutomata
{

        // TEMP CONSTANTS
    private const int Wall = 0;
    private const int Water = 1;
        
    private int _worldSizeX = 50;
    private int _worldSizeY = 50;
    private int[,] _noiseGrid;


    private int[,] GenerateNoiseGrid(int x, int y, int density)
    {
        var noiseGrid = new int[x, y];
        var random = new Random();

        for (var i = 0; i < x; i++)
        {
            for (var j = 0; j < y; j++)
            {
                var randomValue = random.Next(1, 100);
                if (randomValue > density)
                {
                    noiseGrid[i, j] = Wall;
                }
                else
                {
                    noiseGrid[i, j] = Water;
                }
            }
        }
        return noiseGrid;
    }

    private void GenerateMap(int[,] grid, int x, int y, int iterations)
    {
        for (var count = 0; count < iterations; count++)
        {
            var tempGrid = grid;
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    var neighborWallCount = 0;
                    var wallCount = CountNeighbourValues(neighborWallCount, tempGrid, i, j);
                    if (wallCount > 4)
                    {
                        tempGrid[i, j] = Wall;
                    }
                    else
                    {
                        tempGrid[i, j] = Water;
                    }
                }
            } 
        }
    }

    private int CountNeighbourValues(int count, int[,] tempGrid, int xLocation, int yLocation)
    {
        // from of cell count all numbers
        for (var i = -1; i < 1; i++)
        {
            for (var j = -1; j < 1; j++)
            {
                // TODO: Add check if we are not on a edge piece
                if (tempGrid[i + xLocation, j + yLocation] == Wall)
                {
                    count++;
                }
            }
        }

        return count;
    }
}
