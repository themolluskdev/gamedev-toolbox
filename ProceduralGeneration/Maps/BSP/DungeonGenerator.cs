using System;
using System.Collections.Generic;
using System.Xml.Xsl;

public class DungeonGenerator : GenerateMap
{
    public HashSet<List<Array>> RoomAtlas = new HashSet<List<Array>>();
    
    private int[,] _map;
    private int _mapSize = 10000;
    private int _mapWidth, _mapDepth;
    private readonly List<Vector2> _corridors = new List<Vector2>();
    private Leaf _root;
    private int _leafDepth;
    
    private void _CreateNewLevel()
    {
        _mapWidth = _mapDepth = (int) Math.Sqrt(_mapSize);
        _root = new Leaf(0, 0, _mapWidth, _mapDepth);
        _map = GenerateGrid(_mapSize);
        _BSP(_root, _leafDepth);
        
        _AddCorridors();
        GenerateNewMap(_mapSize);
    }

    public override void GenerateNewMap(int dimension)
    {
        for (var x = 0; x < _map.GetLength(0); x++)
        {
            for (var z = 0; z < _map.GetLength(0); z++)
            {
                if (_map[x, z] == 1)
                {
                    
                }

                if (_map[x, z] == 2)
                {
                    gridMap.SetCellItem(x, 0, z, 0);
                }
            }
        }
    }

    private void _BSP(Leaf leaf, int treeDepth)
    {
        if (leaf == null) return;
        if (treeDepth <= 0)
        {
            leaf.CarveRoom(_map, RoomAtlas);
            // We add the divided by 2 inorder to get the middle of the rooms
            _corridors.Add(new Vector2(leaf.XPos + leaf.Width / 2, leaf.ZPos + leaf.Depth / 2));
            return;
        };
        
        if (leaf.Split())
        {
            _BSP(leaf.LeftChild, treeDepth - 1);
            _BSP(leaf.RightChild, treeDepth - 1);
        }
        else
        {
            leaf.CarveRoom(_map, RoomAtlas);
            _corridors.Add(new Vector2(leaf.XPos + leaf.Width / 2, leaf.ZPos + leaf.Depth / 2));
        }
    }

    private void _AddCorridors()
    {
        for (var i = 1; i < _corridors.Count; i++)
        {
            // if our lines are straight
            if ((int) _corridors[i].x == (int) _corridors[i - 1].x ||
                (int) _corridors[i].y == (int) _corridors[i - 1].y)
            {
                _line((int) _corridors[i].x, (int) _corridors[i].y, (int) _corridors[i - 1].x,
                    (int) _corridors[i - 1].y);
            }
            // if lines are diagonal
            else
            {
                _line((int) _corridors[i].x, (int) _corridors[i].y, (int) _corridors[i].x,
                    (int) _corridors[i - 1].y);
                _line((int) _corridors[i].x, (int) _corridors[i].y, (int) _corridors[i - 1].x,
                    (int) _corridors[i].y);
            }
        }
    }

    private void _line(int x, int z, int x2, int z2)
    {
        int w = x2 - x;
        int h = z2 - z;

        int dx1 = 0, dz1 = 0, dx2 = 0, dz2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dz1 = -1; else if (h > 0) dz1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;

        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dz2 = -1; else if (h > 0) dz2 = 1;
            dx2 = 0;
        }

        int numerator = longest >> 1;
        for (var i = 0; i <= longest; i++)
        {
            if (_map[x, z] != 1)
            {
                _map[x, z] = 2;
            }
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                z += dz1;
            }
            else
            {
                x += dx2;
                z += dz2;
            }
        }

    }
}
