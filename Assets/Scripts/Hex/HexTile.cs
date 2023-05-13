using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;
using DG.Tweening;

namespace Penwyn.HexMap
{
    public class HexTile : MonoBehaviour
    {
        private Hex _hex;
        private SpriteRenderer _sprRenderer;
        private bool _spinAble;

        private void Awake()
        {
            _sprRenderer = GetComponent<SpriteRenderer>();
        }

        public void Create(Hex hex, HexType type)
        {
            this._hex = hex;
            this.transform.position = Hex.HexToPixelWorldPos(hex);
            this.gameObject.name = $"[{hex.Q},{hex.R}]";
            _spinAble = type.SpinAble;
            _sprRenderer.sprite = type.Sprite;
        }


        public void SpinLeft()
        {
            _hex.SpinConnectLeft();
            this.transform.DOComplete();
            this.transform.DORotate(this.transform.eulerAngles + RotateVector, 0.1f);
            AudioPlayer.Instance.PlayCardHoveredSFX();
        }

        public void SpinRight()
        {
            _hex.SpinConnectRight();
            this.transform.DOComplete();
            this.transform.DORotate(this.transform.eulerAngles - RotateVector, 0.1f);
            AudioPlayer.Instance.PlayCardHoveredSFX();
        }

        public void OnHovered()
        {
            HexSpinner.Instance.SelectTile(this);
        }

        public void OnHoverExitted()
        {
            HexSpinner.Instance.UnselectTile();
        }



        public Hex Hex { get => _hex; }
        public Vector3 RotateVector => Vector3.forward * 60;
        public SpriteRenderer SpriteRenderer { get => _sprRenderer; }
        public bool SpinAble => _spinAble;
    }

}