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
        public float MoveSensitivity = 0.1F;
        public InputActionReference MouseAction;
        private CinemachineVirtualCamera _camera;
        private bool _isCursorOnTile = false;
        private bool _isDragging = false;

        private Vector2 _dragOrigin;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            MouseAction.action.Enable();
        }

        private void Update()
        {
            DragCamera();
            ZoomCamera();
        }

        public void DragCamera()
        {
            Vector2 difference = CursorManager.Instance.GetMousePosition() - _camera.Follow.transform.position;
            if (!_isCursorOnTile && _isDragging)
            {
                _camera.Follow.transform.position = Vector3.Lerp(_camera.Follow.transform.position, (Vector2)_dragOrigin - difference, MoveSensitivity);
            }
        }

        public void ZoomCamera()
        {
            Vector2 mouseInput = -MouseAction.action.ReadValue<Vector2>();
            if (Mathf.Abs(mouseInput.y) > 0.1F)
                _camera.m_Lens.OrthographicSize += ZoomSensitivity * (Mathf.Sign(mouseInput.y) * Time.deltaTime);
        }

        public void OnLeftMouseClick()
        {
            _isDragging = true;
            _dragOrigin = CursorManager.Instance.GetMousePosition();
        }

        public void OnLeftMouseRelease()
        {
            _isDragging = false;
        }

        public void OnTileHovered(HexTile tile)
        {
            if (!_isDragging)
                _isCursorOnTile = true;
            MouseAction.action.Disable();
        }

        public void OnTileExit(HexTile tile)
        {
            _isCursorOnTile = false;
            MouseAction.action.Enable();
        }

        protected void OnEnable()
        {
            HexTileEventList.Instance.TileHovered.OnEventRaised += OnTileHovered;
            HexTileEventList.Instance.TileExit.OnEventRaised += OnTileExit;
            InputReader.Instance.LeftMousePressed += OnLeftMouseClick;
            InputReader.Instance.LeftMouseCancelled += OnLeftMouseRelease;
        }

        protected void OnDisable()
        {
            HexTileEventList.Instance.TileHovered.OnEventRaised -= OnTileHovered;
            HexTileEventList.Instance.TileExit.OnEventRaised -= OnTileExit;
            InputReader.Instance.LeftMousePressed -= OnLeftMouseClick;
            InputReader.Instance.LeftMouseCancelled -= OnLeftMouseRelease;
        }
    }
}

