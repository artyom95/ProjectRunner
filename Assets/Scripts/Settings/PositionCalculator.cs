using UnityEngine;

namespace Settings
{
    public class PositionCalculator : MonoBehaviour
    {
        [SerializeField] private Grid _grid;

        private const float _playerInstanceOffsetY = 0.41f;
        private const float _doorInstanceOffset = 0.3f;
        private const float _playerMoveUpOffset = 1.15f;
        private const float _playerMoveDownOffset = 1.0f;
        private const float _offsetNextDownPositionY = 0.7f;

        public Vector3 ConvertToWorldPosition(int x, int y, int z)
        {
            var position = _grid.CellToWorld(new Vector3Int(x, y, z));

            return position;
        }

        public Vector2Int ConvertToCellPosition(Vector3 vector3)
        {
            var vector2Int = _grid.WorldToCell(vector3);
            var vector2In = new Vector2Int(vector2Int.x, vector2Int.z);
            return vector2In;
        }

        public Vector3 CalculatePlayerPositionForInstance(GameObject tile)
        {
            var playerPosition = tile.transform.position;
            playerPosition.y = (playerPosition.y + _playerInstanceOffsetY);
            return playerPosition;
        }

        public Vector3 CalculateDoorPositionForInstance(Vector3 tilePosition)
        {
            var doorPosition = new Vector3((tilePosition.x - _doorInstanceOffset),
                (tilePosition.y + _doorInstanceOffset),
                (tilePosition.z - _doorInstanceOffset));
            return doorPosition;
        }

        public Vector3 CalculateNextUpPositionForPlayerMoveUp(Vector3 nextDownPosition)
        {
            var nextUpPosition = new Vector3(nextDownPosition.x, nextDownPosition.y + _playerMoveUpOffset,
                nextDownPosition.z);
            return nextUpPosition;
        }

        public Vector3 CalculateLowerPositionForPlayerMoveDown(Vector3 playerPosition)
        {
            var lowerPosition = new Vector3(playerPosition.x, playerPosition.y - _playerMoveDownOffset,
                playerPosition.z);
            return lowerPosition;
        }

        public Vector3 CalculateDestinationPositionForPlayerMove(Vector3 destinationPosition, Vector3 playerPosition)
        {
            var newDestinationPoint = new Vector3(destinationPosition.x, playerPosition.y, destinationPosition.z);

            return newDestinationPoint;
        }

        public Vector3 CalculateNextDownPosition(Vector3 nextPosition)
        {
            var nextDownPosition =
                new Vector3(nextPosition.x, nextPosition.y - _offsetNextDownPositionY, nextPosition.z);
            return nextDownPosition;
        }

        public Vector3 CalculateFinishPlayerPosition(Vector3 positionFinishTile)
        {
            var nextDownPosition = CalculateNextDownPosition(positionFinishTile);
            var nextUpPlayerPosition = CalculateNextUpPositionForPlayerMoveUp(nextDownPosition);
            return nextUpPlayerPosition;
        }
    }
}