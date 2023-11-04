using System;
using UnityEngine;

namespace Settings
{
    public class NextPositionProvider : MonoBehaviour
    {
        private TileSettings[,] _tileSettingsArray;
        private Color _color;
        private Vector2Int _currentPosition;


        public void Initialize(TileSettings[,] tileSettingsArray, Color finishColor,
            Action<Vector3> onFindFinishPlayerPosition = null)
        {
            _tileSettingsArray = tileSettingsArray;
            FillFinishPositionInArray(finishColor, onFindFinishPlayerPosition);
        }

        public bool IsItColorTile(Vector2Int position)
        {
            var tile = _tileSettingsArray[position.x, position.y];
            _currentPosition = position;
            if (tile != null)
            {
                _color = tile.Color;
                return true;
            }

            return false;
        }

        public Vector3 GetNextPositionInArray()
        {
            var nextPosition = Vector3.zero;

            for (var i = 0; i < _tileSettingsArray.GetLength(0); i++)
            {
                for (var i1 = 0; i1 < _tileSettingsArray.GetLength(1); i1++)
                {
                    if (_tileSettingsArray[i, i1] != null && _tileSettingsArray[i, i1].Color == _color &&
                        !IsTilePositionAreEqual(i, i1))
                    {
                        nextPosition = _tileSettingsArray[i, i1].transform.position;
                        return nextPosition;
                    }
                }
            }

            return Vector3.zero;
        }

        private void FillFinishPositionInArray(Color finishColor, Action<Vector3> onFindFinishPlayerPosition = null)
        {
            var finishPosition = Vector3.zero;

            for (var i = 0; i < _tileSettingsArray.GetLength(0); i++)
            {
                for (var i1 = 0; i1 < _tileSettingsArray.GetLength(1); i1++)
                {
                    if (_tileSettingsArray[i, i1] != null && _tileSettingsArray[i, i1].Color == finishColor)
                    {
                        finishPosition = _tileSettingsArray[i, i1].transform.position;
                        onFindFinishPlayerPosition?.Invoke(finishPosition);
                        return;
                    }
                }
            }
        }

        private bool IsTilePositionAreEqual(int i, int i1)
        {
            if (_currentPosition.x != i || _currentPosition.y != i1)
            {
                return false;
            }

            return true;
        }
    }
}