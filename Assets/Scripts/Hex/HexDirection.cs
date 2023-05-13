using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.HexMap
{
    public class HexDirection : MonoBehaviour
    {
        private static List<Hex> _directions = new List<Hex>() { new Hex(1, 0), new Hex(1, -1), new Hex(0, -1), new Hex(-1, 0), new Hex(-1, 1), new Hex(0, 1) };//Counter clockwise starting from East (aka Right).


        public static Hex Get(int direction)
        {
            if (direction >= _directions.Count)
            {
                Debug.LogError($"Hex direction must be from 0 to 5.");
                return null;
            }
            return _directions[direction];
        }

        public enum Direction
        {
            E, SE, SW, W, NW, NE
        }

        public static List<Hex> Directions { get => _directions; }
    }

}
