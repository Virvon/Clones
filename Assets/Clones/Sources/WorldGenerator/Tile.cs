using UnityEngine;

public class Tile
{
    public readonly Vector3Int GridPosition;
    public readonly GameObject Template;

    public Tile(Vector3Int gridPosition, GameObject template)
    {
        GridPosition = gridPosition;
        Template = template;
    }
}