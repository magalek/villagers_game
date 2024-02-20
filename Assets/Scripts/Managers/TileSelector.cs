using System;
using Map.Tiles;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Grid = Map.Grid;

namespace Managers
{
    public class TileSelector : MonoManager<TileSelector>
    {
        public static MapTile CurrentTile { get; private set; }
        
        private Camera camera;
        
        private Vector2Int lastPosition;
        private MapTile lastTile;
        
        private void Start()
        {
            camera = CameraManager.Camera;
        }

        private void Update()
        {
            SetCurrentTile();
            ProcessSelectedTile();
        }

        private void ProcessSelectedTile()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                lastTile.StopHover();
                return;
            }
            
            if (lastTile != null) lastTile.StopHover();
            if (CurrentTile != null)
            {
                CurrentTile.StartHover();
            }
            lastTile = CurrentTile;
        }

        private void SetCurrentTile()
        {
            var worldMousePosition = (Vector2)camera.ScreenToWorldPoint(Mouse.current.position.value);
            var normalizedPosition = Grid.NormalizePosition(worldMousePosition);
            if (normalizedPosition == lastPosition) return;
            lastPosition = normalizedPosition;
            
            CurrentTile = MapManager.Current.Grid.GetTileFromPosition(normalizedPosition);
        }
    }
}