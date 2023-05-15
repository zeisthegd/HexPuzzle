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

        private bool _isFlooded = false;

        public int S { get => -_q - _r; }//Slice

        public Hex(int q, int r)
        {
            this._q = q;
            this._r = r;
            RotatedAngle = 0;
        }

        public Hex(int q, int r, HexType type)
        {
            this._q = q;
            this._r = r;
            RotatedAngle = 0;
            Load(type);
        }

        public void Load(HexType type)
        {
            this.Type = type;
            _connectedDirs = new HexDirection.Direction[type.ConnectedDirections.Count];
            type.ConnectedDirections.CopyTo(this._connectedDirs);
        }

        public Hex Add(Hex b)
        {
            return new Hex(this._q + b._q, this._r + b._r);
        }

        public Hex Subtract(Hex b)
        {
            return new Hex(this._q - b._q, this._r - b._r);
        }

        #region  DISTANCE
        public int Length()
        {
            return (int)((Mathf.Abs(this._q) + Mathf.Abs(this._r) + Mathf.Abs(S)) / 2);
        }

        /// <summary>
        /// Return distance from this hex to b.
        /// </summary>
        public int Distance(Hex b)
        {
            return Subtract(b).Length();
        }
        #endregion

        #region  #CONNECTION
        [SerializeField] private HexDirection.Direction[] _connectedDirs = new HexDirection.Direction[] { };

        /// <summary>
        /// Spin hex to the left. Angle -= 60.
        /// </summary>
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

        /// <summary>
        /// Spin hex to the right. Angle += 60.
        /// </summary>
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

        /// <summary>
        /// Rotate the hex back to 0.
        /// </summary>
        public void ResetRotatedAngle()
        {
            RotateToAngle(0);
        }

        /// <summary>
        /// Rotate hex to a certain angle. If input angle is bigger than current angle, rotate right. Else rotate left.
        /// Each increment of 60 equals one rotation.
        /// </summary>
        public void RotateToAngle(int angle)
        {
            int totalSpin = Mathf.Abs(angle - RotatedAngle) / 60;
            for (int i = 0; i < totalSpin; i++)
            {
                if (angle > RotatedAngle)
                    SpinConnectRight();
                else
                    SpinConnectLeft();
            }
            _rotatedAngle = angle;
        }

        /// <summary>
        /// Add value to current rotating angle. If bigger than 360, new angle equals 360 - |new angle|.
        /// </summary>
        private void RotateAngle(int value)
        {
            _rotatedAngle += value;
            if (_rotatedAngle < 0)
            {
                _rotatedAngle = 360 - Mathf.Abs(_rotatedAngle);
            }
            if (_rotatedAngle >= 360)
            {
                _rotatedAngle -= 360;
            }
        }

        /// <summary>
        /// Create a new hex in each connected direction and return the list of them.
        /// </summary>
        public List<Hex> NeighborList()
        {
            List<Hex> neighbors = new List<Hex>();
            foreach (int direction in ConnectedDirs)
            {
                Hex neighBor = Neighbor(direction);
                neighbors.Add(neighBor);
            }
            return neighbors;
        }

        /// <summary>
        /// Get a hex in the chosen direction. Return null if the chosen direction is not opened.
        /// </summary>
        /// <param name="direction">Hex Direction</param>
        public Hex Neighbor(int direction)
        {
            return Add(HexDirection.Get(direction));
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
        public bool IsFlooded { get => _isFlooded; set => _isFlooded = value; }
        public Vector2 Position { get => new Vector2(Q, R); }
    }
}

