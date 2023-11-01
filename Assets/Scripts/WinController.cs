using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WinController : MonoBehaviour
{
    
    [SerializeField]
    private UnityEvent _onCelebrateStart;
    
    public void CelebrateWin(Player player, DoorBehaviour door)
    {
        door.RotateDoor();
        player.Dance();
        
        _onCelebrateStart?.Invoke();
    }
}