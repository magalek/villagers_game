using System;
using Map.Tiles;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Grid = Map.Grid;

namespace Managers
{
    public class CameraManager : MonoManager<CameraManager>
    {
        [SerializeField] private Vector2 borderSensitivity;
        [SerializeField] private float speed;

        public static Camera Camera => CameraManager.Current.camera;
        
        private Camera camera;

        private Vector2Int lastPosition;
        private MapTile lastTile;
        
        protected override void OnAwake()
        {
            camera = GetComponent<Camera>();
            //camera.transform.position = Vector3.zero + (Vector3.back * 10);
        }

        private void Update()
        {
            var mousePosition = Mouse.current.position.value;
            MoveCamera(mousePosition);
            ProcessMouseInputs(mousePosition);
        }

        private void ProcessMouseInputs(Vector2 mousePosition)
        {
            var worldMousePosition = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);
            var normalizedPosition = Grid.NormalizePosition(worldMousePosition);
            if (normalizedPosition == lastPosition) return;
            lastPosition = normalizedPosition;
            
            var tile = MapManager.Current.Grid.GetTile(normalizedPosition);
            if (tile != null)
            {
                tile.StartHover();
                
            }
            if (lastTile != null) lastTile.StopHover();
            lastTile = tile;
        }

        private void MoveCamera(Vector2 mousePosition)
        {
            if (!Application.isPlaying) return;
            if (mousePosition.x < 0 || mousePosition.x > Screen.width || mousePosition.y < 0 ||
                mousePosition.y > Screen.height) mousePosition = Vector2.zero;
            
            if (mousePosition == Vector2.zero) return;

            
            var viewportMousePosition = (Vector2)camera.ScreenToViewportPoint(mousePosition);
            float xMove = 0, yMove = 0;
            if (viewportMousePosition.x <= borderSensitivity.x)
            {
                xMove = -1;
            }
            else if (viewportMousePosition.x >= 1 - borderSensitivity.x)
            {
                xMove = 1;
            }
            
            if (viewportMousePosition.y <= borderSensitivity.y)
            {
                yMove = -1;
            }
            else if (viewportMousePosition.y >= 1 - borderSensitivity.y)
            {
                yMove = 1;
            }

            camera.transform.position += (new Vector3(xMove, yMove, 0) * (Time.unscaledDeltaTime * speed));
        }
    }
}