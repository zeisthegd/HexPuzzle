using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using Penwyn.Tools;

namespace Penwyn.HexMap
{
    public class HexMapGen : MonoBehaviour
    {
        [Header("Map Settings")]
        public float HexSize = 1;
        public int MapSize = 3;

        [Header("Hex Prefab")]
        public HexTile HexTilePrefab;

        [Header("Hex Tile Data")]
        public List<HexType> HexTypes;
        public HexType StartHexData;
        public HexType EndHexData;

        [Header("Map Arrays")]
        [ReadOnly] public List<Hex> Map = new List<Hex>();
        [ReadOnly] public List<HexTile> TileMap = new List<HexTile>();
        private List<Hex> _startHexes;
        private List<Hex> _endHexes;

        private void Awake()
        {
            Hex.HexSize = HexSize;
        }

        private void Start()
        {
            Generate();
        }

        public virtual void Generate()
        {
            Map.Clear();
            DestroyAllObjs();
            InitHexMap();
        }

        public virtual void Load(List<Hex> hexes)
        {
            HexSpinner hexSpinner = FindObjectOfType<HexSpinner>();
            foreach (Hex hex in hexes)
            {
                HexTile tile;
                if (GetTile(hex) == null)
                {
                    tile = CreateHexTile(hex, hex.Type);
                }
                else
                {
                    tile = GetTile(hex);
                }
                tile.ResetRotation();

                tile.Create(hex, hex.Type);
                hexSpinner?.RotateToAngle(tile);

            }
            _startHexes = hexes.FindAll(x => x.Type.Category == HexCategory.START);
            _endHexes = hexes.FindAll(x => x.Type.Category == HexCategory.END);
            Map = hexes;
        }

        protected virtual void InitHexMap()
        {
            for (int x = -MapSize; x < MapSize; x++)
            {
                for (int y = MapSize; y > -MapSize; y--)
                {
                    if (IsInsideMapSize(x, y))
                    {
                        Hex newHex = CreateNewHex(x, y, HexTypes[Randomizer.Uniform(0, HexTypes.Count)]);
                    }
                }
            }
        }

        protected virtual Hex CreateNewHex(int x, int y, HexType type)
        {
            Hex newHex = new Hex(x, y, type);
            Map.Add(newHex);
            return newHex;
        }

        protected virtual HexTile CreateHexTile(Hex hex, HexType type)
        {
            HexTile newTile;
            newTile = Instantiate(HexTilePrefab);
            newTile.Create(hex, type);
            newTile.transform.SetParent(this.transform);
            TileMap.Add(newTile);
            return newTile;
        }

        public bool IsInsideMapSize(int x, int y)
        {
            return IsInside(x, y, MapSize, MapSize);
        }

        public bool IsInside(int x, int y, int xConstraint, int yConstraint)
        {
            return Mathf.Abs(x) < xConstraint && Mathf.Abs(y) < yConstraint && Mathf.Abs(-x - y) < (xConstraint > yConstraint ? xConstraint : yConstraint);
        }

        public HexTile GetTile(Hex hex)
        {
            return TileMap.Find(x => (x.Hex.Q == hex.Q) && (x.Hex.R == hex.R));
        }

        public Hex Find(Hex b)
        {
            return Find(b.Q, b.R);
        }
        public Hex Find(int q, int r)
        {
            return Map.Find(x => (x.Q == q) && (x.R == r));
        }

        public Hex GetRandomHexInMap()
        {
            return Map[Randomizer.Uniform(0, Map.Count)];
        }

        protected void DestroyAllObjs()
        {
            foreach (HexTile tile in TileMap)
            {
                Destroy(tile.gameObject);
            }
            TileMap.Clear();
        }

        public List<Hex> StartHexes { get => _startHexes; set => _startHexes = value; }
        public List<Hex> EndHexes { get => _endHexes; set => _endHexes = value; }
    }

}

