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
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private Color _finishColor;
    [SerializeField] private WinController _winController;
    private Color[] _arrayColors;
    private TileSettings[,] _tileSettingsArray;
    private Vector3 _playerPosition;
    private GameObject _player;

    // Start is called before the first frame update

    void Start()
    {
        _mapBuilder.Initialize(GetTileSettingsArray, GetPlayerPosition);
        _mousePositionController.Initialize(GetDestinationPositionForPlayerMove);
        _nextPositionProvider.Initialize(_tileSettingsArray);
        _animatorController.Initialize(_player);
    }

    private void GetDestinationPositionForPlayerMove()
    {
        var destinationPosition = _mousePositionController.GetTheDestinationPositionForPlayerMove();
        Vector3 lastPosition = Vector3.zero;
        Vector2Int position = _positionCalculator.ConvertToCellPosition(destinationPosition);
        if (_nextPositionProvider.IsItColorTile(position))
        {
            lastPosition = _nextPositionProvider.GetNextPositionInArray();
        }

        _playerController.MoveToTheDestinationPlace(destinationPosition, lastPosition);
        if (_mapBuilder.IsItFinishColor(_finishColor, position))
        {
            _winController.CelebrateWin();
        }
       //_animatorController.StopDancing();
    }

    private void GetTileSettingsArray()
    {
        _tileSettingsArray = _mapBuilder.GetArrayTileSettings();
    }

    private void GetPlayerPosition()
    {
        _playerPosition = _mapBuilder.GetPlayerPosition();
        PlaceAPlayer();
    }

    private void PlaceAPlayer()
    {
        _playerController.PlaceAPlayerOnScene(_playerPosition);
        GetPlayerFromPlayerController();
    }

    private void GetPlayerFromPlayerController()
    {
        _player = _playerController.GetPlayer();
        _cameraBehaviour.Initialise(_player);
    }

    // Update is called once per frame
    void Update()
    {
    }
}