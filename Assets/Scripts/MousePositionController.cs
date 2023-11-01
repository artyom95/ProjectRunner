using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private Action _getTheDestinationPositionForPlayerMove;
    private Action _getRaycastColliderColorCoordinate;
    private Vector3 _positionForPlayerMove;
    private Vector3 _currentPositionForPlayerMove;
    private Player _player;

    private bool _isMouseLocked;


    public void Initialize(Player player, Action getTheDestinationPositionForPlayerMove = null)
    {
        _player = player;

        _getTheDestinationPositionForPlayerMove = getTheDestinationPositionForPlayerMove;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _playerController.PlayerIsPosition += ChangeMouseState;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !_isMouseLocked)
        {
            ChangeMouseState();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                if (raycastHit.collider)
                {
                    var positionUnderPlayer = GetPositionUnderPlayer();
                    _positionForPlayerMove = raycastHit.collider.transform.position;
                    if (positionUnderPlayer.Equals(_positionForPlayerMove))
                    {
                        ChangeMouseState();
                        return;
                    }

                    _getTheDestinationPositionForPlayerMove?.Invoke();
                }
            }
        }
    }

    private Vector3 GetPositionUnderPlayer()
    {
        var ray = new Ray(_player.transform.position, Vector3.down);

        if (Physics.Raycast(ray, out var raycastHit))
        {
            if (raycastHit.collider)
            {
                return raycastHit.collider.transform.position;
            }
        }

        return default;
    }

    public Vector3 GetTheDestinationPositionForPlayerMove()
    {
        return _positionForPlayerMove;
    }

    private void ChangeMouseState()
    {
        _isMouseLocked = !_isMouseLocked;
    }

    private void OnDestroy()
    {
        _playerController.PlayerIsPosition -= ChangeMouseState;
    }
}