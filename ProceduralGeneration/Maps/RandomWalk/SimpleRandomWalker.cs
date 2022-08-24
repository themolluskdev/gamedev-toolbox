using System;

public class RandomWalk : GenerateMap
{

    private int _dimension = 30;
    private int _maxIterations = 85;
    private int _maxSteps = 12;
    private float _percentCovered = 0.25f;

    public override void Generate2DMap(int dimension)
    {
        var gridMap = Generate2DGrid(_dimension);
        var rand = new Random();
        
        for (var i = 0; i < _maxIterations; i++)
        {
            var currentSteps = 0;
            var currentRow = rand.Next(1, dimension);
            var currentColumn = rand.Next(1, dimension);
            var directions = new int[4, 2] {{-1, 0}, {0, -1}, {1, 0}, {0, 1}};

            while (currentRow >= 0 && currentColumn  >= 0 && currentRow < dimension && currentColumn < dimension)
            {
                if (currentSteps > _maxSteps) { break; }
                gridMap[currentRow, currentColumn] = "#";
                
                var newDirection = rand.Next(0, 3);
                
                currentRow += directions[newDirection, 0];
                currentColumn += directions[newDirection, 1];
            } 
        } 
    }

    public override string[,] Generate2DDemoMap(int dimension)
    {
        var gridMap = Generate2DGrid(_dimension);
        var rand = new Random();
        
        for (var i = 0; i < _maxIterations; i++)
        {
            var currentSteps = 0;
            var currentRow = rand.Next(1, dimension);
            var currentColumn = rand.Next(1, dimension);
            var directions = new int[4, 2] {{-1, 0}, {0, -1}, {1, 0}, {0, 1}};

            while (currentRow >= 0 && currentColumn  >= 0 && currentRow < dimension && currentColumn < dimension)
            {
                if (currentSteps > _maxSteps) { break; }
                
                gridMap[currentRow, currentColumn] = "#";
                
                var newDirection = rand.Next(0, 3);
                
                currentRow += directions[newDirection, 0];
                currentColumn += directions[newDirection, 1];
                
                currentSteps++;
            } 
        }

        return gridMap;
    }
}