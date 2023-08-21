using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private Action _getTheDestinationPositionForPlayerMove;
    private Action _getRaycastColliderColorCordinate;
    private Vector3 _positionForPlayerMove ;
    private bool _isPlayerOnPosition = false;
    public void Initialize(Action getTheDestinationPositionForPlayerMove)
    {
        _getTheDestinationPositionForPlayerMove = getTheDestinationPositionForPlayerMove;
       
    }
    // Start is called before the first frame update
    private  void Start()
    {
        _playerController.ChangePlayerState += ChangePlayerState;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !_isPlayerOnPosition)
        {
            _isPlayerOnPosition = true;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                
                if (raycastHit.collider)
                {
                    
                    _positionForPlayerMove = raycastHit.collider.transform.position;
                    _getTheDestinationPositionForPlayerMove?.Invoke();
                }
                

               // if (raycastHit.collider.name.Equals("TileVariant"))
               // {
               //     _positionForPlayerMove= new Vector3(raycastHit.collider.transform.position.x, (float)(raycastHit.collider.transform.position.y + 0.3), raycastHit.collider.transform.position.z);
               //     _getTheDestinationPositionForPlayerMove?.Invoke();
               // }
            }
        }
       
    }

    public Vector3 GetTheDestinationPositionForPlayerMove()
    {
       return _positionForPlayerMove ;
    }

    private void ChangePlayerState()
    {
        _isPlayerOnPosition = false;
    }

    private void OnDestroy()
    {
        _playerController.ChangePlayerState -= ChangePlayerState;

    }
}
