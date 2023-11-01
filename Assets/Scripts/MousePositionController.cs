using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private Action _getTheDestinationPositionForPlayerMove;
    private Action _getRaycastColliderColorCoordinate;
    private Vector3 _positionForPlayerMove ;
    private bool _isMouseLocked ;
    public void Initialize(Action getTheDestinationPositionForPlayerMove = null)
    {
        _getTheDestinationPositionForPlayerMove = getTheDestinationPositionForPlayerMove;
       
    }
    // Start is called before the first frame update
    private  void Start()
    {
        _playerController.PlayerIsPosition += ChangeMouseState;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !_isMouseLocked)
        {
            _isMouseLocked = true;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                
                if (raycastHit.collider)
                {
                    
                    _positionForPlayerMove = raycastHit.collider.transform.position;
                    _getTheDestinationPositionForPlayerMove?.Invoke();
                }
            }
        }
       
    }

    public Vector3 GetTheDestinationPositionForPlayerMove()
    {
       return _positionForPlayerMove ;
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
