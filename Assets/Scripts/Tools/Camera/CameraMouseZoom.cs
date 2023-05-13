using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Cinemachine;

using Penwyn.Game;
using Penwyn.HexMap;

namespace Penwyn.Tools
{
    public class CameraMouseZoom : SingletonMonoBehaviour<CameraMouseZoom>
    {
        public float ZoomSensitivity = 0.1F;
        public InputActionReference MouseAction;
        private CinemachineVirtualCamera _camera;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            MouseAction.action.Enable();
        }

        private void Update()
        {
            Vector2 mouseInput = -MouseAction.action.ReadValue<Vector2>();
            if (Mathf.Abs(mouseInput.y) > 0.1F)
                _camera.m_Lens.OrthographicSize += ZoomSensitivity * (Mathf.Sign(mouseInput.y) * Time.deltaTime);
        }

        public void OnTileHovered(HexTile tile)
        {
            MouseAction.action.Disable();
        }

        public void OnTileExit(HexTile tile)
        {
            MouseAction.action.Enable();
        }

        protected void OnEnable()
        {
            HexTileEventList.Instance.TileHovered.OnEventRaised += OnTileHovered;
            HexTileEventList.Instance.TileExit.OnEventRaised += OnTileExit;
        }

        protected void OnDisable()
        {
            HexTileEventList.Instance.TileHovered.OnEventRaised -= OnTileHovered;
            HexTileEventList.Instance.TileExit.OnEventRaised -= OnTileExit;
        }
    }
}

