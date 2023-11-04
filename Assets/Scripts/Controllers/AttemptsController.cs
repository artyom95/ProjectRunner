using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class AttemptsController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<int> _attemptsChanged;

        [SerializeField] private UnityEvent _attemptsOver;

        private int _amountAttempts;

        public void Initialize(int amountAttempts)
        {
            _amountAttempts = amountAttempts;
        }


        public void DecreaseAmountAttempts()
        {
            _amountAttempts--;
            if (AreAttemptsOver())
            {
                _attemptsOver?.Invoke();
            }

            _attemptsChanged?.Invoke(_amountAttempts);
        }

        public bool AreAttemptsOver()
        {
            if (_amountAttempts == 0)
            {
                return true;
            }

            return false;
        }
    }
}