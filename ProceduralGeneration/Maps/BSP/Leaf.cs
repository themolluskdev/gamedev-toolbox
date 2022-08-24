using System;
using System.Collections.Generic;
using System.Linq;

public class Leaf : Node
{
    public Leaf LeftChild;
    public Leaf RightChild;
    public int XPos { get; }
    public int ZPos { get; }
    public int Width { get; }
    public int Depth { get; }
    private int _roomMin = 10;
    private int _roomMax;
    public Leaf(int x, int z, int width, int depth)
    {
        XPos = x;
        ZPos = z;
        Width = width;
        Depth = depth;
    }

    public bool Split()
    {
        if (Width <= _roomMin || Depth <= _roomMin) return false;
        
        var rng = new Random();
        bool splitHorizontal = rng.Next(0, 100) > 50;
        if (Width > Depth && Width / Depth >= 1.25)
        {
            splitHorizontal = false;
        } else if (Width < Depth && Depth / Width >= 1.25)
        {
            splitHorizontal = true;
        }

        _roomMax = (splitHorizontal ? Depth : Width) - _roomMin;
       
        if (_roomMax <= _roomMin) return false;
        
        if (splitHorizontal)
        {
            var cutDepth = rng.Next(_roomMin, _roomMax);
            LeftChild = new Leaf(XPos, ZPos, Width, cutDepth);
            RightChild = new Leaf(XPos, ZPos + cutDepth, Width, Depth - cutDepth);
        }
        else
        {
            var cutWidth = rng.Next(_roomMin, _roomMax);
            LeftChild = new Leaf(XPos, ZPos, cutWidth, Depth);
            RightChild = new Leaf(XPos + cutWidth, ZPos, Width - cutWidth, Depth);
        }

        return true;
    }
    
    public void CarveRoom(int[,] map, HashSet<List<Array>> roomAtlas)
    {
        for (var x = XPos + 1; x < Width + XPos - 1; x++)
        {
            for (var z = ZPos + 1; z < Depth + ZPos - 1; z++)
            {
                map[x, z] = 1;
            }
        }
        _StoreRoomInformation(roomAtlas);
    }

    private void _StoreRoomInformation(HashSet<List<Array>> roomAtlas)
    {
        var positions = new List<Array>();
        var xCarvedPositions = Enumerable.Range(XPos + 1, Width + XPos - 1).ToArray();
        var zCarvedPositions = Enumerable.Range(ZPos + 1, Depth + ZPos - 1).ToArray();
        positions.Add(xCarvedPositions);
        positions.Add(zCarvedPositions);
        roomAtlas.Add(positions);
    }
}
