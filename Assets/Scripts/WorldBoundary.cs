using UnityEngine;

public class WorldBoundary : MonoBehaviour
{
    const float HalfSize = 100f;
    const float WallThickness = 1f;
    const float WallLength = 2f * HalfSize + 2f * WallThickness; // overlap at corners

    void Awake()
    {
        CreateWall("Wall_Top",    new Vector2(0f, HalfSize + WallThickness / 2f),  new Vector2(WallLength, WallThickness));
        CreateWall("Wall_Bottom", new Vector2(0f, -HalfSize - WallThickness / 2f), new Vector2(WallLength, WallThickness));
        CreateWall("Wall_Left",   new Vector2(-HalfSize - WallThickness / 2f, 0f), new Vector2(WallThickness, WallLength));
        CreateWall("Wall_Right",  new Vector2(HalfSize + WallThickness / 2f, 0f),  new Vector2(WallThickness, WallLength));
    }

    void CreateWall(string name, Vector2 position, Vector2 size)
    {
        GameObject wall = new GameObject(name);
        wall.transform.parent = transform;
        wall.transform.position = new Vector3(position.x, position.y, 0f);

        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.size = size;
    }
}
