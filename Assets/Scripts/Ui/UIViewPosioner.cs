using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Ui;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class UIViewPosioner : MonoBehaviour
{
   [SerializeField] private Vector3 _levelOffset;
   [SerializeField] private Vector3 _attemptsOffset;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void UpdatePosition(Transform playerTransform, RectTransform uiViewPrefab, string typePrefab)
    {
        if (typePrefab.Equals("LevelView"))
        {
            var screenPoint = RectTransformUtility.WorldToScreenPoint(_camera, playerTransform.position + _levelOffset);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, screenPoint, null,
                out var localPoint);
            uiViewPrefab.anchoredPosition = localPoint;
        }
        else if (typePrefab.Equals("AttemptsView"))
        {
            var screenPoint = RectTransformUtility.WorldToScreenPoint(_camera, playerTransform.position + _attemptsOffset);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, screenPoint, null,
                out var localPoint);
            uiViewPrefab.anchoredPosition = localPoint;
        }
       
       
    }
}