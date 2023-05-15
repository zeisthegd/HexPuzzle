using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.HexMap
{
    public class HexPathmaker : MonoBehaviour
    {
        private HexMapGen _hexMapGen;
        private List<Hex> _path = new List<Hex>();

        private void Awake()
        {
            _hexMapGen = GetComponent<HexMapGen>();
        }

        public void MakeAllPaths()
        {
            _path.Clear();
            foreach (Hex hex in _hexMapGen.StartHexes)
            {
                hex.IsFlooded = true;
                MakePath(hex);
            }

            foreach (Hex hex in _path)
            {
                Debug.Log(hex.Position);
            }
        }

        public void MakePath(Hex startHex)
        {
            List<Hex> hexes = NeighborHexesOnMap(startHex, _hexMapGen.Map);
            foreach (Hex hex in hexes)
            {
                if (!_path.Contains(hex) && AreConnected(startHex, hex))
                {
                    _path.Add(hex);
                    MakePath(hex);
                }
            }
        }

        /// <summary>
        /// Returns true if this hex is connected to the input hex.
        /// </summary>
        public bool AreConnected(Hex a, Hex b)
        {
            return (a.NeighborList().Find(x => x.Position.Equals(b.Position)) != null) && (b.NeighborList().Find(x => x.Position.Equals(a.Position)) != null);
        }

        public List<Hex> NeighborHexesOnMap(Hex baseHex, List<Hex> hexMap)
        {
            List<Hex> neighborsOnMap = new List<Hex>();
            List<Hex> neighborHexes = baseHex.NeighborList();

            foreach (Hex tempNeighborHex in neighborHexes)
            {
                Hex queriedHex = hexMap.Find(x => x.Position.Equals(tempNeighborHex.Position));
                if (queriedHex != null)
                {
                    neighborsOnMap.Add(queriedHex);
                }
            }
            return neighborsOnMap;
        }

        public List<Hex> Path { get => _path; }
    }
}
