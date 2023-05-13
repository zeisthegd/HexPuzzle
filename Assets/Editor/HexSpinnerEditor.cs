using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Penwyn.HexMap
{
    [CustomEditor(typeof(HexSpinner))]
    public class HexSpinnerEditor : UnityEditor.Editor
    {
        void OnSceneGUI()
        {
            HexSpinner spinner = target as HexSpinner;
            HexTile chosen = spinner.ChosenTile;
            if (chosen != null)
            {
                Handles.color = Color.green;
                foreach (int dir in chosen.Hex.ConnectedDirs)
                {
                    Handles.DrawLine(Hex.HexToPixelWorldPos(chosen.Hex), Hex.HexToPixelWorldPos(chosen.Hex.Neighbor((int)dir)), 5);
                }
            }
        }
    }
}

