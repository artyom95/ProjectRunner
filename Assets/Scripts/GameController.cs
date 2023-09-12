using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MapBuilder _mapBuilder;
    [SerializeField] private NextPositionProvider _nextPositionProvider;
    [SerializeField] private PositionCalculator _positionCalculator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MousePositionController _mousePositionController;
    [SerializeField] private CameraBehaviour _cameraBehaviour;

    [SerializeField] private Color _finishColor;
    [SerializeField] private WinController _winController;
    private Color[] _arrayColors;
    private TileSettings[,] _tileSettingsArray;
    private Vector3 _playerPosition;
    private Vector3 _positionFinishTile;
    private Vector3 _finishPlayerPosition;
    private Player _player;
    private bool _isPlayerOnFinishPosition;

    void Start()
    {
        _mapBuilder.Initialize(GetTileSettingsArray, GetPlayerPosition);
        _mousePositionController.Initialize(FindDestinationPositionForPlayerMove);
        _nextPositionProvider.Initialize(_tileSettingsArray, _finishColor, FindFinishPlayerPosition);
    }

    private void FindFinishPlayerPosition(Vector3 finishPlayerPosition)
    {
        _positionFinishTile = finishPlayerPosition;
        _finishPlayerPosition = _positionCalculator.CalculateFinishPlayerPosition(_positionFinishTile);
    }

    private void FindDestinationPositionForPlayerMove()
    {
        var destinationPosition = _mousePositionController.GetTheDestinationPositionForPlayerMove();
        var lastPosition = Vector3.zero;
        var position = _positionCalculator.ConvertToCellPosition(destinationPosition);
        if (_nextPositionProvider.IsItColorTile(position))
        {
            lastPosition = _nextPositionProvider.GetNextPositionInArray();
        }

        _playerController.MoveToDestinationPlace(destinationPosition, lastPosition, _finishPlayerPosition,
            IsPlayerOnFinish);
    }

    private void IsPlayerOnFinish(bool isPlayerOnFinish)
    {
        if (isPlayerOnFinish) 
        {
            _winController.CelebrateWin(_player, door: _mapBuilder.GetDoor());
        }
    }

    private void GetTileSettingsArray()
    {
        _tileSettingsArray = _mapBuilder.GetArrayTileSettings();
    }

    private void GetPlayerPosition()
    {
        _playerPosition = _mapBuilder.GetPlayerPosition();
        PlacePlayer();
    }

    private void PlacePlayer()
    {
        _playerController.PlacePlayerOnScene(_playerPosition);
        GetPlayerFromPlayerController();
    }

    private void GetPlayerFromPlayerController()
    {
        _player = _playerController.GetPlayer();
        _cameraBehaviour.Initialise(_player);
    }
}