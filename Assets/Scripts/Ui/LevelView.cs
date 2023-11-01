using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _levelTextMeshProUGUI;

   public void UpdateLevel(int level)
   {
      _levelTextMeshProUGUI.text = level.ToString();
   }
}
