using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
namespace Penwyn.HexMap
{
    [System.Serializable]
    public class HexCellularGenData
    {
        public string Name;

        [HorizontalLine(1, EColor.Green)]
        [Header("Environment")]
        public int Width = 50;
        public int Height = 50;
        [Range(0, 100)] public int FillPercent = 50;
        public int ResmoothWallTimes = 4;
        public int MinNeighborWalls = 4;
        public string Seed;
        public bool UseRandomSeed;
        [Header("Tilemap")]
        public HexType GroundType;
        public HexType EmptyType;
    }

}

