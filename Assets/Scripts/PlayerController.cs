using System;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action ChangePlayerState;
    [SerializeField] private PositionCalculator _positionCalculator;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private float _movingPlayerDuration;
    [SerializeField] private float _movingPlayerDownOrUpDuration;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private GameObject _baseUnderPlayer;


    private Player _player;
    private GameObject _basePlayer;
    private Vector3 _nextDownPosition;

    private const int _yPlayerAngle = -90;
    private const float _offsetNextDownPositionY = 0.7f;
    private const float _deltaBetweenPositions = 1.2f;

    public void PlacePlayerOnScene(Vector3 positionForPlacing)
    {
        _player = Instantiate(_playerPrefab);
        _player.transform.position = positionForPlacing;
        _player.transform.Rotate(0, _yPlayerAngle, 0);
        InstanceBaseUnderPlayer();
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public void MoveToTheDestinationPlace(Vector3 destinationPosition, Vector3 nextPosition)
    {
        SaveNextPositionForPlayerMoving(nextPosition);
        var sequence = DOTween.Sequence();
        // чтобы игрок не проваливался под тайл ногой(плыл горизонтально)
        Vector3 destinationPoint =
            _positionCalculator.CalculateDestinationPositionForPlayerMove(destinationPosition,
                _player.transform.position);

        if (AreThePositionsNearest(destinationPosition))
        {
            Debug.Log("Destination point");
            Debug.Log(destinationPoint);

            _player.transform.LookAt(destinationPoint);
            _player.Walk();
            sequence.Append(_player.transform.DOMove(destinationPoint, _movingPlayerDuration));

            if (nextPosition != Vector3.zero)
            {
                sequence.AppendCallback(_player.StopWalking);
                sequence.OnComplete(MoveDown);
            }
            else
            {
                sequence.AppendCallback(_player.StopWalking);
                sequence.OnComplete(() => ChangePlayerState.Invoke());
            }
        }
        else
        {
            ChangePlayerState?.Invoke();
        }
    }

    private void InstanceBaseUnderPlayer()
    {
        _basePlayer = Instantiate(_baseUnderPlayer, _player.transform, true);
        _basePlayer.transform.localPosition = Vector3.zero;
    }

    private void SaveNextPositionForPlayerMoving(Vector3 nextPosition)
    {
        if (nextPosition != Vector3.zero)
        {
            _nextDownPosition = nextPosition;
            _nextDownPosition.y -= _offsetNextDownPositionY;
        }
    }

    private bool AreThePositionsNearest(Vector3 destinationPlace)
    {
        if (Math.Abs((_player.transform.position - destinationPlace).magnitude) <= _deltaBetweenPositions &&
            destinationPlace != _player.transform.position)
        {
            return true;
        }

        return false;
    }

    private void MoveDown()
    {
        var sequence = DOTween.Sequence();
        Vector3 lowerPosition = _positionCalculator.CalculateLowerPositionForPlayerMoveDown(_player.transform.position);
        sequence.Append(_player.transform.DOMove(lowerPosition, _movingPlayerDownOrUpDuration));
        sequence.AppendCallback(HidePlayer);
        sequence.OnComplete(MoveToNextDownPosition);
    }

    private void MoveToNextDownPosition()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_player.transform.DOMove(_nextDownPosition, _movingPlayerDuration));
        sequence.Append(_player.transform.DORotate(new Vector3(0, _yPlayerAngle, 0), _rotateDuration));
        sequence.AppendCallback(ShowPlayer);
        sequence.OnComplete(MoveUp);
    }

    private void MoveUp()
    {
        Vector3 nextUpPosition = _positionCalculator.CalculateNextUpPositionForPlayerMoveUp(_nextDownPosition);
        _player.transform.DOMove(nextUpPosition, _movingPlayerDownOrUpDuration)
            .OnComplete(() => ChangePlayerState.Invoke());
    }

    private void HidePlayer()
    {
        _player.gameObject.SetActive(false);
    }

    private void ShowPlayer()
    {
        _player.gameObject.SetActive(true);
    }
}