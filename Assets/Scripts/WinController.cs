using UnityEngine;

public class WinController : MonoBehaviour
{
    public void CelebrateWin(Player player, DoorBehaviour door)
    {
        door.RotateDoor();
        player.Dance();
    }
}