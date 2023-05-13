using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Penwyn.Tools;
using Penwyn.Game;

namespace Penwyn.HexMap
{
    public class HexMapEditor : HexMapGen
    {
        [Header("Editor")]
        public HexType EmptyType;
        public HexType HexToPlace;

        private List<HexTile> _placeHex = new List<HexTile>();

        private HexTile _chosenTile;
        private HexType _beforeChosenType;


        public override void Generate()
        {
            Map.Clear();
            DestroyAllObjs();
            InitHexMap();
        }
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

        private void OnTileHovered(HexTile tile)
        {
            if (HexToPlace != null)
            {
                _beforeChosenType = tile.Type;
                _chosenTile = tile;
                _chosenTile.Create(HexToPlace);
            }
        }

        /// <summary>
        /// Set the tile back to the previous type.
        /// </summary>
        /// <param name="tile"></param>
        private void OnTileExit(HexTile tile)
        {
            if (tile == _chosenTile)
            {
                _chosenTile.Create(_beforeChosenType);
                _chosenTile = null;
            }
        }

        private void OnTileSelected(HexTile tile)
        {
            if (tile != null && _chosenTile != null && tile == _chosenTile && HexToPlace != null)
            {
                _chosenTile.Create(HexToPlace);
                _beforeChosenType = tile.Type;
            }
        }

        /// <summary>
        /// Set the tile back to the previous type.
        /// </summary>
        /// <param name="tile"></param>
        private void OnTileUnselected(HexTile tile)
        {
            if (tile == _chosenTile)
            {
                _chosenTile.Create(EmptyType);
                _chosenTile = null;
            }
        }


        protected void OnEnable()
        {
            HexTileEventList.Instance.TileHovered.OnEventRaised += OnTileHovered;
            HexTileEventList.Instance.TileExit.OnEventRaised += OnTileExit;
            HexTileEventList.Instance.TileSelected.OnEventRaised += OnTileSelected;
            HexTileEventList.Instance.TileUnselected.OnEventRaised += OnTileUnselected;
        }

        protected void OnDisable()
        {
            HexTileEventList.Instance.TileHovered.OnEventRaised -= OnTileHovered;
            HexTileEventList.Instance.TileExit.OnEventRaised -= OnTileExit;
            HexTileEventList.Instance.TileSelected.OnEventRaised -= OnTileSelected;
            HexTileEventList.Instance.TileUnselected.OnEventRaised -= OnTileUnselected;
        }
        public HexTile ChosenTile { get => _chosenTile; }
    }

}

