using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.HexMap
{
    [System.Serializable]
    public class HexLevelData
    {
        public string Name = "";
        public List<Hex> HexMap;

        public HexLevelData() { }

        public HexLevelData(string name)
        {
            Name = name;
        }

        public HexLevelData(string name, List<Hex> hexMap)
        {
            Name = name;
            HexMap = hexMap;
        }

        public override string ToString()
        {
            string s = "";
            foreach (Hex hex in HexMap)
            {
                s += hex.ToString();
            }
            return s;
        }
    }
}

