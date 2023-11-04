using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Settings
{
    public class Player : MonoBehaviour
    {
        public UnityEvent StoppedDance;

        [SerializeField] private Animator _animator;

        private static readonly int IsGoing = Animator.StringToHash("IsGoing");
        private static readonly int IsTimeToDance = Animator.StringToHash("IsTimeToDance");


        public void Walk()
        {
            _animator.SetBool(IsGoing, true);
        }

        public void StopWalking()
        {
            _animator.SetBool(IsGoing, false);
        }

        public void Dance()
        {
            _animator.SetBool(IsTimeToDance, true);
        }

        [UsedImplicitly]
        public async Task StopDancing()
        {
            _animator.SetBool(IsTimeToDance, false);
            await Task.Delay(9000);
            StoppedDance?.Invoke();
        }
    }
}