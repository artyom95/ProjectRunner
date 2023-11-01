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
    [SerializeField] private DoorBehaviour _prefabDoor;

    private Action _getTileSettingsArray;
    private Action _getPlayerPosition;

    private int[,] _map;
    private int _arrayValue;
    
    private TileSettings[,] _tileSettingsArray;
    
    private bool _isItEmptyPlace ;
    private bool _wasTheDoorInstalled ;
    private bool _isPointForInstanceDoorDetected ;

    private Vector3 _playerPosition;

    private DoorBehaviour _door;
    // Start is called before the first frame update
    private void Start()
    {
    }

    public void Initialize(int currentLevel, Action GetTileSettingsArray = null, Action GetPlayerPosition = null)
    {
        _getPlayerPosition = GetPlayerPosition;
        _getTileSettingsArray = GetTileSettingsArray;
        _map = _mapTypeProvider.GetTypeMap(currentLevel);
        _tileSettingsArray = new TileSettings[_map.GetLength(0), _map.GetLength(1)];
        _playerPosition = new Vector3();
        BuildingMap();
    }

    public DoorBehaviour GetDoor()
    {
        return _door;
    }
    private void BuildingMap()
    {
        var locationTileOnHeight = 0;
        for (var row = 0; row < _map.GetLength(0); row++)
        {//
            if (row % 2 == 0)
            {
                locationTileOnHeight = Random.Range(1, 3); 
            }


            for (var column = 0; column < _map.GetLength(1); column++)
            {
                var typeObjectForInstantiate = FindTypeObject(row, column);

                if (typeObjectForInstantiate == null && _isItEmptyPlace)
                {
                    var tile = Instantiate(_prefabTileWithoutPlaceForLocation);
                    tile.transform.position =
                        _positionCalculator.ConvertToWorldPosition(row, locationTileOnHeight, column);
                    _playerPosition = _positionCalculator.CalculatePlayerPositionForInstance(tile);
                   
                    if (_isPointForInstanceDoorDetected && !_wasTheDoorInstalled)
                    {
                      InstanceDoor(tile);
                    }
                }
                else if (typeObjectForInstantiate == _prefabTileWithPlaceForHeroLocation)

                {
                    var tile = Instantiate(typeObjectForInstantiate);

                    tile.transform.position =
                        _positionCalculator.ConvertToWorldPosition(row, locationTileOnHeight, column);
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
        _door = Instantiate(_prefabDoor);
       _door.transform.position = _positionCalculator.CalculateDoorPositionForInstance(tile.transform.position);
        _wasTheDoorInstalled = true;
        _door.transform.Rotate(0, -90, 0);
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
        if (_tileSettingsArray[position.x,position.y] != null && _tileSettingsArray[position.x,position.y].Color == color )
        {
            return true;
        }

        return false;
    }
}