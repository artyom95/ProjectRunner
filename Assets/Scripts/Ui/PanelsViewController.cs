using JetBrains.Annotations;
using UnityEngine;

namespace Ui
{
    public class PanelsViewController : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;

        [UsedImplicitly]
        public void ShowWinPanel()
        {
            _winPanel.SetActive(true);
        }

        [UsedImplicitly]
        public void ShowLosePanel()
        {
            _losePanel.SetActive(true);
        }
    }
}