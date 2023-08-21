using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PositionCalculator _positionCalculator;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private float _movingPlayerDuration;
    [SerializeField] private float _movingPlayerDownOrUpDuration;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private GameObject _baseUnderPlayer;
    [SerializeField] private AnimatorController _animatorController;
    private GameObject _basePlayer;
    private GameObject _player;
    private Vector3 _nextDownPosition;
    public Action ChangePlayerState;

    public void PlaceAPlayerOnScene(Vector3 positionForPlacing)
    {
        _player = Instantiate(_playerPrefab);
        _player.transform.position = positionForPlacing;
        _player.transform.Rotate(0, -90, 0);
       InstanceBaseUnderPlayer();
    }

    public GameObject GetPlayer()
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
            _animatorController.Walk();
           sequence.Append( _player.transform.DOMove(destinationPoint, _movingPlayerDuration));
           
            if (nextPosition != Vector3.zero)
            {
                
                sequence.AppendCallback(_animatorController.StopWalking);
                sequence.OnComplete(MoveDown);
            }
            else
            {
                sequence.AppendCallback(_animatorController.StopWalking);
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
        _basePlayer.transform.localPosition = new Vector3(0, 0, 0);
        InstallAnimationForBaseUnderPlayer();
    }

    private void SaveNextPositionForPlayerMoving(Vector3 nextPosition)
    {
        if (nextPosition != Vector3.zero)
        {
            _nextDownPosition = nextPosition;
            _nextDownPosition.y -= 0.7f;
        }
    }

    private bool AreThePositionsNearest(Vector3 destinationPlace)
    {
        if ((Math.Abs((_player.transform.position - destinationPlace).magnitude)) <= 1.2 &&
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

        Debug.Log("lower point");
        Debug.Log(lowerPosition);


        sequence.Append(_player.transform.DOMove(lowerPosition, _movingPlayerDownOrUpDuration));
         //  sequence.SetEase(Ease.OutSine);
        sequence.AppendCallback(HidePlayer);
        //  sequence.SetEase(Ease.OutSine);
        sequence.OnComplete(MoveToNextDownPosition);
    }

    private void MoveToNextDownPosition()
    {
        var sequence = DOTween.Sequence();
        Debug.Log("next Down Position");
        Debug.Log(_nextDownPosition);
        sequence.Append(_player.transform.DOMove(_nextDownPosition, _movingPlayerDuration));
        //  sequence.SetEase(Ease.OutSine);
       sequence.Append(_player.transform.DORotate(new Vector3(0,-90,0),_rotateDuration));

        sequence.AppendCallback(ShowPlayer);
        // sequence.SetEase(Ease.OutSine);
        sequence.OnComplete(MoveUp);
    }

    private void MoveUp()
    {
        Vector3 nextUpPosition = _positionCalculator.CalculateNextUpPositionForPlayerMoveUp(_nextDownPosition);

        Debug.Log("next UP Position");
        Debug.Log(nextUpPosition);

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

    // Start is called before the first frame update
    void Start()
    {
    }

    private void InstallAnimationForBaseUnderPlayer()
    {
        _basePlayer.transform.DOScale(new Vector3(1.4f,0,1.4f), _scaleDuration)
            .SetEase(Ease.OutQuad)
            .SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
    }
}