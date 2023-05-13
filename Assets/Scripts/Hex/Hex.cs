using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.HexMap
{
    public class Hex
    {
        public static float HexSize = 1;
        public int Q { get; set; }//Column
        public int R { get; set; }//Row
        public int S { get => -Q - R; }//Slice

        public Hex(int q, int r)
        {
            Q = q;
            R = r;
        }

        public Hex(int q, int r, HexType type)
        {
            Q = q;
            R = r;
            Load(type);
        }

        public void Load(HexType type)
        {
            _connectedDirs = new HexDirection.Direction[type.ConnectedDirections.Count];
            type.ConnectedDirections.CopyTo(this._connectedDirs);
        }

        public Hex Add(Hex b)
        {
            return new Hex(Q + b.Q, R + b.R);
        }

        public Hex Subtract(Hex b)
        {
            return new Hex(Q - b.Q, R - b.R);
        }
        #region Neighbor

        public Hex Neighbor(int direction)
        {
            return Add(HexDirection.Get(direction));
        }

        #endregion


        #region  DISTANCE
        public int Length()
        {
            return (int)((Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2);
        }

        public int Distance(Hex b)
        {
            return Subtract(b).Length();
        }
        #endregion

        #region  #CONNECTION
        private HexDirection.Direction[] _connectedDirs = new HexDirection.Direction[] { };

        public void SpinConnectLeft()
        {
            if (_connectedDirs.Length > 0)
            {
                for (int i = 0; i < _connectedDirs.Length; i++)
                {
                    if (_connectedDirs[i] - 1 < 0)
                        _connectedDirs[i] = (HexDirection.Direction)HexDirection.Directions.Count - 1;
                    else
                        _connectedDirs[i] = _connectedDirs[i] - 1;
                }
            }
        }

        public void SpinConnectRight()
        {
            if (_connectedDirs.Length > 0)
            {
                for (int i = 0; i < _connectedDirs.Length; i++)
                {
                    if (_connectedDirs[i] + 1 >= (HexDirection.Direction)HexDirection.Directions.Count)
                        _connectedDirs[i] = 0;
                    else
                        _connectedDirs[i] = _connectedDirs[i] + 1;
                }
            }
        }

        #endregion

        public static Vector3 HexToPixelWorldPos(Hex hex)
        {
            float offSet = hex.R != 0 ? 1F * HexSize / 2.0F : 0;
            if (hex.R < 0)
                offSet = -offSet;

            var col = (hex.Q * HexSize) + offSet * Mathf.Abs(hex.R);
            var row = ((HexSize * Mathf.Sqrt(3) / 2 * 1F) - 0.01F) * hex.R;
            return new Vector3(col, row);
        }

        public static Vector3 HexToWorldPos(Hex hex)
        {
            float offSet = hex.R != 0 ? 1F * HexSize / 2.0F : 0;
            if (hex.R < 0)
                offSet = -offSet;

            var col = ((hex.Q * HexSize) + offSet * Mathf.Abs(hex.R));
            var row = HexSize * Mathf.Sqrt(3) / 2 * hex.R;
            return new Vector3(col, 0, row);
        }

        public override string ToString()
        {
            return $"Hex: [{Q},{R},{S}]";
        }

        public HexDirection.Direction[] ConnectedDirs { get => _connectedDirs; }

    }
}

