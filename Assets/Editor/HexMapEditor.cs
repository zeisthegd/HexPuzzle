using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Penwyn.HexMap;

namespace Penwyn.Editor
{
    [CustomEditor(typeof(HexMapGen))]
    public class HexMapEditor : UnityEditor.Editor
    {
        private HexMapGen _hexMap;
        public void OnSceneGUI()
        {
            _hexMap = target as HexMapGen;

            foreach (Hex hex in _hexMap.Map)
            {
                Handles.color = Color.white;
                DrawHexConnections(hex);

                if (Handles.Button(Hex.HexToPixelWorldPos(hex), Quaternion.identity, 0.1F, 0.2F, Handles.SphereHandleCap))
                {
                    _hexMap.GetTile(hex).SpinLeft();
                    DrawHexConnections(hex, true);
                    foreach (int direction in hex.ConnectedDirs)
                    {
                        Hex neighBor = hex.Neighbor(direction);
                    }
                }

            }
        }

        private void DrawHexConnections(Hex hex, bool chosen = false)
        {
            foreach (int direction in hex.ConnectedDirs)
            {
                Hex neighBor = hex.Neighbor(direction);
                if (!chosen)
                    Handles.color = _hexMap.Find(neighBor) != null ? Color.white : Color.red;
                else
                    Handles.color = Color.green;
                Handles.DrawLine(Hex.HexToPixelWorldPos(hex), Hex.HexToPixelWorldPos(neighBor));
            }
        }
    }

}