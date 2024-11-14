using UnityEngine;

public class BackgroundGrid : MonoBehaviour
{
    [SerializeField] private Sprite[] backgroundSprites;
    private int gridWidth = 200;
    private int gridHeight = 200;
    private float tileSize = 1f;
    
    private void Start()
    {
        GenerateBackgroundGrid();
    }

    private void GenerateBackgroundGrid()
    {
        for (var x = -gridWidth / 2; x <= gridWidth / 2; x++)
        {
            for (var y = -gridHeight / 2; y <= gridHeight / 2; y++)
            {
                var randomSprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];

                var tile = new GameObject("Tile_" + x + "_" + y);
                tile.transform.position = new Vector2(x * tileSize, y * tileSize);
                tile.transform.parent = transform;

                var renderer = tile.AddComponent<SpriteRenderer>();
                renderer.sprite = randomSprite;
            }
        }
    }
}
