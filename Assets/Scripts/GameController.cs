using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private UnityEvent<int, Player> _playerInitialised;

    [SerializeField] private MapBuilder _mapBuilder;
    [SerializeField] private NextPositionProvider _nextPositionProvider;
    [SerializeField] private PositionCalculator _positionCalculator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MousePositionController _mousePositionController;
    [SerializeField] private CameraBehaviour _cameraBehaviour;
    [SerializeField] private SceneController _sceneController;

    [SerializeField] private Color _finishColor;
    [SerializeField] private WinController _winController;
    [SerializeField] private WinPanelView _winPanelView;

    private Color[] _arrayColors;
    private TileSettings[,] _tileSettingsArray;
    private Vector3 _playerPosition;
    private Vector3 _positionFinishTile;
    private Vector3 _finishPlayerPosition;
    private Player _player;
    private bool _isPlayerOnFinishPosition;
    private int _currentLevel = 0;

    void Start()
    {
      
      var level = _sceneController.GetSequenceNumberScene();
      _currentLevel = level+1;
      StartCurrentLevel();
        _sceneController.PlayerWin+=_winPanelView.ShowWinPanel;
        
    }
  
    
    private void StartCurrentLevel()
    {
       

        _mapBuilder.Initialize(_currentLevel, GetTileSettingsArray, GetPlayerPosition);
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
            _playerController.PlayerIsPosition?.Invoke();
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
        _player.StoppedDance.AddListener( _sceneController.LoadNextScene) ;

        _cameraBehaviour.Initialise(_player);
        _playerInitialised?.Invoke(_currentLevel, _player);
    }

    private void OnDestroy()
    {
        _sceneController.PlayerWin -=_winPanelView.ShowWinPanel;

        _player.StoppedDance.RemoveListener( _sceneController.LoadNextScene) ;
    }
}