using System.Threading.Tasks;
using Behaviours;
using JetBrains.Annotations;
using Settings;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class WinController : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onCelebrateStart;

        [SerializeField] private UnityEvent _gameWin;

        [SerializeField] private UnityEvent _gameLose;

        [SerializeField] private SceneController _sceneController;
        [SerializeField] private AttemptsController _attemptsController;

        public void CelebrateWin(Player player, DoorBehaviour door)
        {
            door.RotateDoor();
            player.Dance();
            _onCelebrateStart?.Invoke();
        }

        public void CheckGameState()
        {
            if (_sceneController.AreThereOthersGameScenes() && !_attemptsController.AreAttemptsOver())
            {
                _sceneController.LoadNextScene();
            }

            if (!_sceneController.AreThereOthersGameScenes() && !_attemptsController.AreAttemptsOver())
            {
                _gameWin.Invoke();
            }
        }

        [UsedImplicitly]
        public async void  FinishGame()
        {
            await Task.Delay(1000);
            _gameLose.Invoke();
        }
    }
}