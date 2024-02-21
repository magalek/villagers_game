using System;
using Managers;
using UnityEngine;
using Grid = Map.Grid;

namespace Utility
{
    public class TextureBakerTest : MonoBehaviour
    {

        private void Start()
        {
            const int size = 4096;

            var newTexture = new Texture2D(size, size);

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    var tile = MapManager.Current.Grid.GeTileFromIndex(x, y);
                    var texture = tile.GetComponent<SpriteRenderer>().sprite.texture;

                    Debug.Log(texture.dimension);
                    
                    newTexture.SetPixels(128 * y, 128 * x, 128, 128, texture.GetPixels());
                }
            }
            newTexture.Apply();


            GetComponent<SpriteRenderer>().sprite = Sprite.Create(newTexture,
                new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), 128);
        }
    }
}