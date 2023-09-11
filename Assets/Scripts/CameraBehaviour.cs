using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private Player _player;

    public void Initialise(Player player)
    {
        _player = player;
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}