using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    [SerializeField] private AttemptsController _attemptsController;
    private int _numberScene;

    private void Start()
    {
        _numberScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene()
    {
        var nextScene = _numberScene + 1;

        SceneManager.LoadSceneAsync(nextScene);
        
    }

    public bool AreThereOthersGameScenes()
    {
        var nextScene = _numberScene + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            return true;
        }

        return false;
    }

    public int GetSequenceNumberScene()
    {
        var numberScene = SceneManager.GetActiveScene().buildIndex;
        return numberScene;
    }

    [UsedImplicitly]
    public void RestartFirstLevelScene()
    {
        SceneManager.LoadSceneAsync("FirstLevel");
    }
}