using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Settings;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;

public class UIViewManager : MonoBehaviour
{
    [SerializeField] private UIViewPosioner _uIViewPositioner;

    [SerializeField] private LevelView _levelViewPrefab;

    [SerializeField] private AttemptsView _attemptsViewPrefab;

    private Player _player;
    private LevelView _levelView;
    private AttemptsView _attemptsVew;

    [UsedImplicitly]
    public void Initialize(int amountAttempts, int levelValue, Player player)
    {
        CreateUIViewComponents(amountAttempts, levelValue);
        _player = player;
    }


    /// <summary>
    /// подписать метод в момент появления игрока на событие (вызывать метод в момент  появления игрока) 
    /// </summary>
    [UsedImplicitly]
    public void ShowUIFrames()
    {
        _levelView.gameObject.SetActive(true);
       // _attemptsVew.gameObject.SetActive(true);
    }

    /// <summary>
    /// подписать метод в момент скрытия игрока на событие (вызывать метод в момент скрытия игрока) 
    /// </summary>
    [UsedImplicitly]
    public void HideUIFrames()
    {
        _levelView.gameObject.SetActive(false);
       // _attemptsVew.gameObject.SetActive(false);
    }

   [UsedImplicitly]
    public void UpdateAttempts(int amountAttempts)
    {
        _attemptsVew.UpdateAttempts(amountAttempts);
    }
    private void Update()
    {
        _uIViewPositioner.UpdatePosition(_player.transform, _levelView.transform as RectTransform,"LevelView");
        _uIViewPositioner.UpdatePosition(_player.transform, _attemptsVew.transform as RectTransform, "AttemptsView");
    }

    private void CreateUIViewComponents(int amountAttempts, int levelValue)
    {
        _levelView = Instantiate(_levelViewPrefab, transform);
        _levelView.UpdateLevel(levelValue);

        
        _attemptsVew = Instantiate(_attemptsViewPrefab, transform);
        _attemptsVew.UpdateAttempts(amountAttempts);
    }
}