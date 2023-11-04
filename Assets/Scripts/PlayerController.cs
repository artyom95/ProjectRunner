using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Action PlayerOnPosition;
    public Action PlayerMoved;

    [SerializeField] private UnityEvent _appearPlayer;
    [SerializeField] private UnityEvent _hidePlayer;

    [SerializeField] private PositionCalculator _positionCalculator;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private float _movingPlayerDuration;
    [SerializeField] private float _movingPlayerDownOrUpDuration;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private GameObject _baseUnderPlayer;


    private Player _player;
    private GameObject _basePlayer;
    private Vector3 _nextDownPosition;
    private Vector3 _finishPlayerPosition;

    private const int _yPlayerAngle = -90;
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

    public void MoveToDestinationPlace(Vector3 destinationPosition, Vector3 nextPosition, Vector3 finishPosition,
        Action<bool> onPlayerFinishPosition)
    {
        SaveNextPositionForPlayerMoving(nextPosition);
        var sequence = DOTween.Sequence();
        // чтобы игрок не проваливался под тайл ногой(плыл горизонтально)
        var destinationPoint =
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
                sequence.AppendCallback(() => PlayerOnPosition?.Invoke());
                sequence.AppendCallback(() => OnPlayerFinished(finishPosition, onPlayerFinishPosition));
                sequence.OnComplete(() => PlayerMoved?.Invoke());
            }
        }
        else
        {
            PlayerOnPosition?.Invoke();
        }
    }

    private void OnPlayerFinished(Vector3 finishPosition, Action<bool> onPlayerFinishPosition)
    {
        if (IsPlayerOnFinishPosition(finishPosition))
        {
            onPlayerFinishPosition?.Invoke(IsPlayerOnFinishPosition(finishPosition));
        }
    }

    private bool IsPlayerOnFinishPosition(Vector3 finishPosition)
    {
        if (_player.transform.position == finishPosition)
        {
            return true;
        }

        return false;
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
            _nextDownPosition = _positionCalculator.CalculateNextDownPosition(nextPosition);
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
        var lowerPosition = _positionCalculator.CalculateLowerPositionForPlayerMoveDown(_player.transform.position);
        sequence.Append(_player.transform.DOMove(lowerPosition, _movingPlayerDownOrUpDuration));
        sequence.AppendCallback(HidePlayer);
        _hidePlayer?.Invoke();
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
        var nextUpPosition = _positionCalculator.CalculateNextUpPositionForPlayerMoveUp(_nextDownPosition);
        var sequence = DOTween.Sequence();
        sequence.Append(_player.transform.DOMove(nextUpPosition, _movingPlayerDownOrUpDuration));
        sequence.AppendCallback(() => PlayerOnPosition.Invoke());
        sequence.AppendCallback(() => _appearPlayer?.Invoke());
        sequence.AppendCallback(() => PlayerMoved?.Invoke());
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