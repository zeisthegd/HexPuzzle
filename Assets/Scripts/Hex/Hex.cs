using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.HexMap
{
    [System.Serializable]
    public class Hex
    {
        public static float HexSize = 1;
        [SerializeField] private int _q;
        [SerializeField] private int _r;
        [SerializeField] private int _rotatedAngle;
        [SerializeField] private HexType _type;

        public int S { get => -_q - _r; }//Slice

        public Hex(int q, int r)
        {
            this._q = q;
            this._r = r;
        }

        public Hex(int q, int r, HexType type)
        {
            this._q = q;
            this._r = r;
            Load(type);
        }

        public void Load(HexType type)
        {
            this.Type = type;
            _connectedDirs = new HexDirection.Direction[type.ConnectedDirections.Count];
            type.ConnectedDirections.CopyTo(this._connectedDirs);
            RotatedAngle = 0;
        }

        public Hex Add(Hex b)
        {
            return new Hex(this._q + b._r, this._r + b._r);
        }

        public Hex Subtract(Hex b)
        {
            return new Hex(this._q - b._r, this._r - b._r);
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
            return (int)((Mathf.Abs(this._q) + Mathf.Abs(this._r) + Mathf.Abs(S)) / 2);
        }

        public int Distance(Hex b)
        {
            return Subtract(b).Length();
        }
        #endregion

        #region  #CONNECTION
        [SerializeField] private HexDirection.Direction[] _connectedDirs = new HexDirection.Direction[] { };

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
                RotateAngle(-60);
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
                RotateAngle(60);
            }
        }

        private void RotateAngle(int value)
        {
            RotatedAngle += value;
            if (RotatedAngle < 0)
            {
                RotatedAngle = 360 - RotatedAngle;
            }
            if (RotatedAngle >= 360)
            {
                RotatedAngle -= 360;
            }
        }

        #endregion

        public static Vector3 HexToPixelWorldPos(Hex hex)
        {
            float offSet = hex._r != 0 ? 1F * HexSize / 2.0F : 0;
            if (hex._r < 0)
                offSet = -offSet;

            var col = (hex._q * HexSize) + offSet * Mathf.Abs(hex._r);
            var row = ((HexSize * Mathf.Sqrt(3) / 2 * 1F) - 0.01F) * hex._r;
            return new Vector3(col, row);
        }

        public static Vector3 HexToWorldPos(Hex hex)
        {
            float offSet = hex._r != 0 ? 1F * HexSize / 2.0F : 0;
            if (hex._r < 0)
                offSet = -offSet;

            var col = ((hex._q * HexSize) + offSet * Mathf.Abs(hex._r));
            var row = HexSize * Mathf.Sqrt(3) / 2 * hex._r;
            return new Vector3(col, 0, row);
        }
        public HexDirection.Direction[] ConnectedDirs { get => _connectedDirs; }
        public int RotatedAngle { get => _rotatedAngle; set => _rotatedAngle = value; }
        public HexType Type { get => _type; set => _type = value; }
        public int Q { get => _q; set => _q = value; }
        public int R { get => _r; set => _r = value; }
    }
}

