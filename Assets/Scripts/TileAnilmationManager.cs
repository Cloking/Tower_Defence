using UnityEngine;

public class TileAnilmationManager : MonoBehaviour
{
    private TileAnimation[] tiles;

    void Start()
    {
        tiles = FindObjectsOfType<TileAnimation>();
    }

    void Update()
    {
        foreach (TileAnimation tile in tiles)
        {
            tile.OnUpdate();  
        }
    }
}
