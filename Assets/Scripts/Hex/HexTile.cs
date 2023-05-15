using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;
using Penwyn.Tools;
using DG.Tweening;

using UnityEngine.EventSystems;

namespace Penwyn.HexMap
{
    public class HexTile : MonoBehaviour
    {
        private Hex _hex;
        private SpriteRenderer _sprRenderer;
        private bool _spinAble;
        private HexType _type;

        private void Awake()
        {
            _sprRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Load a new HexType but reuse the current Hex.
        /// </summary>
        public void Create(HexType type)
        {
            Create(this.Hex, type);
        }

        /// <summary>
        /// Load a new Hex and HexType.
        /// </summary>
        public void Create(Hex hex, HexType type)
        {
            this._hex = hex;
            this._type = type;
            this.transform.position = Hex.HexToPixelWorldPos(hex);
            this.gameObject.name = $"[{hex.Q},{hex.R}]";
            _spinAble = type.SpinAble;
            _sprRenderer.sprite = type.Sprite;
            hex.Load(type);
        }

        /// <summary>
        /// Spin the connected directions to the left. DoTween the rotation. Play sound.
        /// </summary>
        public void SpinLeft()
        {
            _hex.SpinConnectLeft();
            this.transform.DOComplete();
            this.transform.DORotate(this.transform.eulerAngles + RotateVector, 0.1f);
            AudioPlayer.Instance.PlayCardHoveredSFX();
        }

        /// <summary>
        /// Spin the connected directions to the right. DoTween the rotation. Play sound.
        /// </summary>

        public void SpinRight()
        {
            _hex.SpinConnectRight();
            this.transform.DOComplete();
            this.transform.DORotate(this.transform.eulerAngles - RotateVector, 0.1f);
            AudioPlayer.Instance.PlayCardHoveredSFX();
        }

        public void OnHovered()
        {
            HexTileEventList.Instance.TileHovered.RaiseEvent(this);
        }

        public void OnHoverExitted()
        {
            HexTileEventList.Instance.TileExit.RaiseEvent(this);
        }

        public void OnClick(BaseEventData eventData)
        {
            PointerEventData data = (PointerEventData)eventData;
            if (data.button == PointerEventData.InputButton.Left)
            {
                HexTileEventList.Instance.TileSelected.RaiseEvent(this);
            }
            if (data.button == PointerEventData.InputButton.Right)
            {
                HexTileEventList.Instance.TileUnselected.RaiseEvent(this);

            }
        }

        public Hex Hex { get => _hex; }
        public HexType Type { get => _type; }
        public Vector3 RotateVector => Vector3.forward * 60;
        public SpriteRenderer SpriteRenderer { get => _sprRenderer; }
        public bool SpinAble => _spinAble;

    }

}