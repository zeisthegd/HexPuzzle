using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;
using Penwyn.Game;

using DG.Tweening;

namespace Penwyn.HexMap
{
    public class HexSpinner : SingletonMonoBehaviour<HexSpinner>
    {
        private HexTile _chosenTile;

        private void Start()
        {

        }

        public void SelectTile(HexTile tile)
        {
            _chosenTile = tile;
            _chosenTile.transform.DOComplete();
            _chosenTile.transform.DOMoveY(_chosenTile.transform.position.y + 0.075F, 0.1F);
            _chosenTile.SpriteRenderer.sortingOrder += 3;
        }

        public void UnselectTile()
        {
            _chosenTile.transform.DOComplete();
            _chosenTile.transform.DOMove(Hex.HexToPixelWorldPos(_chosenTile.Hex), 0.1F);
            _chosenTile.SpriteRenderer.sortingOrder -= 3;
            _chosenTile = null;
        }

        public void SpinChosenLeft()
        {
            if (CanSpin())
                _chosenTile?.SpinLeft();
            else if (_chosenTile)
            {
                _chosenTile.transform.DOComplete();
                _chosenTile.transform.DOPunchRotation(Vector3.forward * 8, 0.2F);
            }
        }

        public void SpinChosenRight()
        {
            if (CanSpin())
                _chosenTile?.SpinRight();
            else if (_chosenTile)
            {
                _chosenTile.transform.DOComplete();
                _chosenTile.transform.DOPunchRotation(Vector3.forward * 8, 0.2F);
            }
        }

        private bool CanSpin()
        {
            return _chosenTile != null && _chosenTile.SpinAble;
        }

        private void OnEnable()
        {
            InputReader.Instance.SpinLeftPressed += SpinChosenLeft;
            InputReader.Instance.SpinRightPressed += SpinChosenRight;
        }

        private void OnDisable()
        {
            InputReader.Instance.SpinLeftPressed -= SpinChosenLeft;
            InputReader.Instance.SpinRightPressed -= SpinChosenRight;
        }

        public HexTile ChosenTile { get => _chosenTile; }
    }

}