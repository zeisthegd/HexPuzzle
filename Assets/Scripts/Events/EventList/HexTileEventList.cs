using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class HexTileEventList
    {
        public HexTileEventChannel TileHovered;
        public HexTileEventChannel TileExit;
        public HexTileEventChannel TileSelected;
        public HexTileEventChannel TileUnselected;

        public static HexTileEventList Instance;

        public static void CreateInstance()
        {
            Instance = new HexTileEventList();
        }

        public HexTileEventList()
        {
            TileHovered = new HexTileEventChannel();
            TileExit = new HexTileEventChannel();
            TileSelected = new HexTileEventChannel();
            TileUnselected = new HexTileEventChannel();
        }

    }
}
