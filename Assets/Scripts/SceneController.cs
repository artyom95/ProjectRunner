using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
   public UnityAction PlayerWin;
   private int _numberScene;

   public void LoadNextScene()
   {
      _numberScene = SceneManager.GetActiveScene().buildIndex;
      var nextScene = _numberScene + 1;
      if (nextScene < SceneManager.sceneCountInBuildSettings)
      {
         
         SceneManager.LoadSceneAsync(nextScene);
      }
      else
      {
         PlayerWin?.Invoke();
      }
    
   }

   public int GetSequenceNumberScene()
   {
      var numberScene = SceneManager.GetActiveScene().buildIndex;
      return numberScene;
   }
}
