using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using Penwyn.Tools;

namespace Penwyn.HexMap
{
    public class HexMapEditor : HexMapGen
    {
        [Header("Editor")]
        public HexType EmptyType;

        protected override void InitHexMap()
        {
            for (int x = -MapSize; x < MapSize; x++)
            {
                for (int y = MapSize; y > -MapSize; y--)
                {
                    if (Mathf.Abs(x) < MapSize && Mathf.Abs(y) < MapSize && Mathf.Abs(-x - y) < MapSize)
                    {
                        CreateNewHex(x, y, EmptyType);
                    }
                }
            }
        }
    }

}

