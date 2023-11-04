using TMPro;
using UnityEngine;

public class AttemptsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attemptsTextMeshProUGUI;

    public void UpdateAttempts(int amountAttempt)
    {
        _attemptsTextMeshProUGUI.text = amountAttempt.ToString();
    }
}