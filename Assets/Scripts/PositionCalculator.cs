using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCalculator : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 ConvertToWorldPosition(int x, int y, int z)
    {
        Vector3 position = _grid.CellToWorld(new Vector3Int(x, y, z));
       
        return position;
    }

    public Vector2Int ConvertToCellPosition(Vector3 vector3)
    {
        Vector3Int vector2Int = _grid.WorldToCell(vector3);
        Vector2Int vector2In = new Vector2Int(vector2Int.x, vector2Int.z);
        return vector2In;
    }

    public Vector3 CalculatePlayerPositionForInstance(GameObject tile)
    {
        Vector3 playerPosition = tile.transform.position;
        playerPosition.y = (float)(playerPosition.y + 0.41);
        return playerPosition;
    }

    public Vector3 CalculateDoorPositionForInstance(Vector3 tilePosition)
    {
        Vector3 doorPosition = new Vector3((float)(tilePosition.x - 0.3),
            (float)(tilePosition.y + 0.3),
            (float)(tilePosition.z - 0.32));
       return doorPosition;
    }

    public Vector3 CalculateNextUpPositionForPlayerMoveUp(Vector3 nextDownPosition)
    {
        Vector3 nextUpPosition = new Vector3(nextDownPosition.x, nextDownPosition.y + 1.15f, nextDownPosition.z);
        return nextUpPosition;

    }

    public Vector3 CalculateLowerPositionForPlayerMoveDown(Vector3 playerPosition)
    {
        Vector3 lowerPosition = new Vector3(playerPosition.x, playerPosition.y - 1.0f,
            playerPosition.z);
        return lowerPosition;
    }

    public Vector3 CalculateDestinationPositionForPlayerMove(Vector3 destinationPosition,Vector3 playerPosition)
    {
        Vector3 newDestinationPoint = new Vector3(destinationPosition.x, playerPosition.y, destinationPosition.z);

        return newDestinationPoint;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
