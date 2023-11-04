using TMPro;
using UnityEngine;

namespace Ui
{
   public class LevelView : MonoBehaviour
   {
      [SerializeField] private TextMeshProUGUI _levelTextMeshProUGUI;
     
      public void UpdateLevel(int level)
      {
         _levelTextMeshProUGUI.text = level.ToString();
      }
   
    
   }
}
