using Behaviours;
using Controllers;
using JetBrains.Annotations;
using Settings;
using UnityEngine;
using UnityEngine.Events;

namespace EntryPoint
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<int, int, Player> _playerInitialised;

        [SerializeField] private MapBuilder _mapBuilder;
        [SerializeField] private NextPositionProvider _nextPositionProvider;
        [SerializeField] private PositionCalculator _positionCalculator;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MousePositionController _mousePositionController;
        [SerializeField] private CameraBehaviour _cameraBehaviour;
        [SerializeField] private SceneController _sceneController;

        [SerializeField] private Color _finishColor;
        [SerializeField] private WinController _winController;

        [SerializeField] private AttemptsController _attemptsController;

        [SerializeField] private int _amountAttempts;

        private TileSettings[,] _tileSettingsArray;
        private Vector3 _playerPosition;
        private Vector3 _positionFinishTile;
        private Vector3 _finishPlayerPosition;
        private Player _player;
        private bool _isPlayerOnFinishPosition;
        private int _currentLevel;


        private void Start()
        {
            _playerController.PlayerMoved += _attemptsController.DecreaseAmountAttempts;
            var level = _sceneController.GetSequenceNumberScene();
            _currentLevel = level + 1;
            StartCurrentLevel();
        }

        [UsedImplicitly]
        public void QuitGame()
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

        private void StartCurrentLevel()
        {
            _attemptsController.Initialize(_amountAttempts);
            _mapBuilder.Initialize(_currentLevel, ReceiveTileSettingsArray, FillPlayerPosition);
            _mousePositionController.Initialize(_player, FindDestinationPositionForPlayerMove);
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
                _playerController.PlayerOnPosition?.Invoke();
                _winController.CelebrateWin(_player, door: _mapBuilder.GetDoor());
            }
        }

        private void ReceiveTileSettingsArray()
        {
            _tileSettingsArray = _mapBuilder.GetArrayTileSettings();
        }

        private void FillPlayerPosition()
        {
            _playerPosition = _mapBuilder.GetPlayerPosition();
            PlacePlayer();
        }

        private void PlacePlayer()
        {
            _playerController.PlacePlayerOnScene(_playerPosition);
            ReceivePlayer();
        }

        private void ReceivePlayer()
        {
            _player = _playerController.GetPlayer();
            _player.StoppedDance.AddListener(_winController.CheckGameState);

            _cameraBehaviour.Initialise(_player);
            _playerInitialised?.Invoke(_amountAttempts, _currentLevel, _player);
        }

        private void OnDestroy()
        {
            _playerController.PlayerMoved -= _attemptsController.DecreaseAmountAttempts;

            _player.StoppedDance.RemoveListener(_winController.CheckGameState);
        }
    }
}