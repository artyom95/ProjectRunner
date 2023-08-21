using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private MapTypeProvider _mapTypeProvider;
    [SerializeField] private TileSettings _prefabTileWithPlaceForHeroLocation;
    [SerializeField] private GameObject _prefabTileWithoutPlaceForLocation;
    [SerializeField] private PositionCalculator _positionCalculator;
    [SerializeField] private GameObject _prefabDoor;

    private Action _getTileSettingsArray;
    private Action _getPlayerPosition;

    private int[,] _map;
    private int _arrayValue;
    
    private TileSettings[,] _tileSettingsArray;
    
    private bool _isItEmptyPlace = false;
    private bool _wasTheDoorInstaled = false;
    private bool _isPointForInstanceDoorDetected = false;

    private Vector3 _playerPosition;
    // Start is called before the first frame update
    private void Start()
    {
    }

    public void Initialize(Action GetTileSettingsArray, Action GetPlayerPosition)
    {
        _getPlayerPosition = GetPlayerPosition;
        _getTileSettingsArray = GetTileSettingsArray;
        _map = _mapTypeProvider.GetTypeOfMap();
        _tileSettingsArray = new TileSettings[_map.GetLength(0), _map.GetLength(1)];
        _playerPosition = new Vector3();
        BuildingMap();
    }

    private void BuildingMap()
    {
        var locationTileOnHeigh = 0;
        for (var row = 0; row < _map.GetLength(0); row++)
        {//
            if (row % 2 == 0)
            {
                locationTileOnHeigh = Random.Range(1, 3); 
            }


            for (var column = 0; column < _map.GetLength(1); column++)
            {
                var typeObjectForInstantiate = FindTypeObject(row, column);

                if (typeObjectForInstantiate == null && _isItEmptyPlace)
                {
                    var tile = Instantiate(_prefabTileWithoutPlaceForLocation);
                    tile.transform.position =
                        _positionCalculator.ConvertToWorldPosition(row, locationTileOnHeigh, column);
                    _playerPosition = _positionCalculator.CalculatePlayerPositionForInstance(tile);
                   
                    if (_isPointForInstanceDoorDetected && !_wasTheDoorInstaled)
                    {
                      InstanceDoor(tile);
                    }
                }
                else if (typeObjectForInstantiate == _prefabTileWithPlaceForHeroLocation)

                {
                    var tile = Instantiate(typeObjectForInstantiate);

                    tile.transform.position =
                        _positionCalculator.ConvertToWorldPosition(row, locationTileOnHeigh, column);
                    tile.SetColor(_arrayValue);
                    _tileSettingsArray[row, column] = tile;
                }

                _isItEmptyPlace = false;
            }
        }
        _getPlayerPosition.Invoke();
        _getTileSettingsArray.Invoke();
    }

    private void InstanceDoor(GameObject tile)
    {
        var door = Instantiate(_prefabDoor);
       door.transform.position = _positionCalculator.CalculateDoorPositionForInstance(tile.transform.position);
        _wasTheDoorInstaled = true;
        door.transform.Rotate(0, -90, 0);

    }
    private TileSettings FindTypeObject(int row, int column)
    {
        TileSettings gameObjectForInstantiate = default;

        if (_map[row, column] == -1)
        {
            _isItEmptyPlace = true;
            if (!_isPointForInstanceDoorDetected)
            {
                _isPointForInstanceDoorDetected = true;
            }
        }
        else if (_map[row, column] != 0)
        {
            _arrayValue = _map[row, column];
            gameObjectForInstantiate = _prefabTileWithPlaceForHeroLocation;
        }

        return gameObjectForInstantiate;
    }

    public TileSettings[,] GetArrayTileSettings()
    {
        return _tileSettingsArray;
    }

    public Vector3 GetPlayerPosition()
    {
        return _playerPosition;
    }

    public bool IsItFinishColor(Color color, Vector2Int position)
    {
        if (_tileSettingsArray[position.x,position.y] != null && _tileSettingsArray[position.x,position.y].color == color )
        {
            return true;
        }

        return false;
    }
}