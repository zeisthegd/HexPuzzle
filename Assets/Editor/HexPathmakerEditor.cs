using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Penwyn.HexMap
{
    [CustomEditor(typeof(HexPathmaker))]
    public class HexPathmakerEditor : UnityEditor.Editor
    {
        private HexPathmaker _pathmaker;

        private void OnSceneGUI()
        {
            _pathmaker = target as HexPathmaker;
            foreach (Hex hex in _pathmaker.Path)
            {
                Handles.color = Color.green;
                foreach (Hex neighbor in hex.NeighborList())
                {
                    Handles.DrawLine(Hex.HexToPixelWorldPos(hex), Hex.HexToPixelWorldPos(neighbor));
                }
            }
        }
    }
}
