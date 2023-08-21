using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Color = System.Drawing.Color;

public class NextPositionProvider : MonoBehaviour
{
    private TileSettings[,] _tileSettingsArray;
    private UnityEngine.Color _color;
    private Vector2Int _currentPosition;

    public void Initialize(TileSettings[,] tileSettingsArray)
    {
        _tileSettingsArray = tileSettingsArray;
    }
    public bool IsItColorTile( Vector2Int position)
    {
        var tile = _tileSettingsArray[position.x, position.y];
        _currentPosition = position;
        if (tile != null)
        {
            _color = tile.color;
            return true;
        }

        return false;
    }

    public Vector3 GetNextPositionInArray()
    {
        Vector3 nextPosition = Vector3.zero;

        // List<Vector2Int> allowPositions = new List<Vector2Int>();
        for (var i = 0; i < _tileSettingsArray.GetLength(0); i++)
        {
            for (var i1 = 0; i1 < _tileSettingsArray.GetLength(1); i1++)
            {
                if (_tileSettingsArray[i, i1] != null && _tileSettingsArray[i, i1].color == _color && !IsTilePositionAreEqual( i,i1) )
                    
                    
                {
                    nextPosition = _tileSettingsArray[i, i1].transform.position;
                    // allowPositions.Add(new Vector2Int(i,i1)); 
                }
            }
        }

        // var number = Random.Range(0, allowPositions.Count);
        //  Vector2Int nextPosition = allowPositions.First();
          return nextPosition;
    }

    private bool IsTilePositionAreEqual( int i, int i1)
    {
        if (_currentPosition.x != i || _currentPosition.y != i1)
        {
            return false;
        }

        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}