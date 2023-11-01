using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelViewManager : MonoBehaviour
{
     [SerializeField] private LevelViewPosioner _levelViewPosioner;

    [SerializeField] private LevelView _levelViewPrefab;

    private Player _player;
    private LevelView _levelVew;
    
    [UsedImplicitly]
    public void Initialize(int value, Player player)
    {
        _levelVew = Instantiate(_levelViewPrefab, transform);
        _levelVew.UpdateLevel(value);
        _player = player;
        
    }

    /// <summary>
    /// подписать метод в момент появления игрока на событие (вызывать метод в момент  появления игрока) 
    /// </summary>
    [UsedImplicitly]
    public void ShowLevelFrame()
    {
        _levelVew.gameObject.SetActive(true);
    }

    /// <summary>
    /// подписать метод в момент скрытия игрока на событие (вызывать метод в момент скрытия игрока) 
    /// </summary>
    [UsedImplicitly]
    public void HideLevelFrame()
    {
        _levelVew.gameObject.SetActive(false);
    }

    private void Update()
    {
        _levelViewPosioner.UpdatePosition(_player.transform, _levelVew.transform as RectTransform);
    }
}