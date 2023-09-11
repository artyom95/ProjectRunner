using DG.Tweening;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _hinge;
    [SerializeField] private float _rotateDuration ;

    private const int _doorAngleY = -180;
    public void RotateDoor()
    {
        _hinge.transform.DORotate(new Vector3(0, _doorAngleY, 0), _rotateDuration);
    }
  
}
