using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LevelViewPosioner : MonoBehaviour
{
    [SerializeField] private Vector3 _offste;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void UpdatePosition(Transform playerTransform, RectTransform levelViewPrefab)
    {
        var screenPoint = RectTransformUtility.WorldToScreenPoint(_camera, playerTransform.position + _offste);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, screenPoint, null,
            out var localPoint);
        levelViewPrefab.anchoredPosition = localPoint;
    }
}