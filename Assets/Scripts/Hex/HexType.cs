using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.HexMap
{
    [CreateAssetMenu(menuName = "Hex/Tile")]
    public class HexType : ScriptableObject
    {
        public string ID = "hex";
        public Sprite Sprite;
        public List<HexDirection.Direction> ConnectedDirections;
        public bool SpinAble  = false;
        public HexCategory Category;
    }
}

