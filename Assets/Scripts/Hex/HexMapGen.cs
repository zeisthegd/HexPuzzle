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
        public readonly List<Hex> Map = new List<Hex>();
        public readonly List<HexTile> TileMap = new List<HexTile>();
        private Hex _startHex;
        private Hex _endHex;



        private void Awake()
        {
            Hex.HexSize = HexSize;
        }

        private void Start()
        {
            Generate();
        }

        [Button("Generate")]
        public virtual void Generate()
        {
            Map.Clear();
            DestroyAllObjs();
            InitHexMap();
            GenerateRandomStartEnd();
        }

        protected virtual void InitHexMap()
        {
            for (int x = -MapSize; x < MapSize; x++)
            {
                for (int y = MapSize; y > -MapSize; y--)
                {
                    if (Mathf.Abs(x) < MapSize && Mathf.Abs(y) < MapSize && Mathf.Abs(-x - y) < MapSize)
                    {
                        CreateNewHex(x, y, HexTypes[Randomizer.Uniform(0, HexTypes.Count)]);
                    }
                }
            }
        }

        protected virtual void CreateNewHex(int x, int y, HexType type)
        {
            Hex newHex = new Hex(x, y, type);
            Map.Add(newHex);

            HexTile newTile;
            newTile = Instantiate(HexTilePrefab);
            newTile.Create(newHex, type);
            newTile.transform.SetParent(this.transform);
            TileMap.Add(newTile);
        }

        protected void GenerateRandomStartEnd()
        {
            if (MapSize <= 1)
            {
                Debug.LogWarning("Cannot generate START and END if MapSize is lower than 2.");
                return;
            }
            _startHex = GetRandomHexInMap();
            do
            {
                _endHex = GetRandomHexInMap();
            }
            while (_startHex.Distance(_endHex) < 2);

            _startHex.Load(StartHexData);
            _endHex.Load(EndHexData);

            GetTile(StartHex).Create(_startHex, StartHexData);
            GetTile(EndHex).Create(_endHex, EndHexData);
        }

        public HexTile GetTile(Hex hex)
        {
            return TileMap.Find(x => x.Hex.Q == hex.Q && x.Hex.R == hex.R);
        }

        public Hex Find(Hex b)
        {
            return Find(b.Q, b.R);
        }
        public Hex Find(int q, int r)
        {
            return Map.Find(x => x.Q == q && x.R == r);
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

        public Hex StartHex { get => _startHex; }
        public Hex EndHex { get => _endHex; }
    }

}

