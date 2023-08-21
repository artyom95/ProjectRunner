using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private GameObject _player;

    public void Initialise(GameObject player)
    {
        _player = player;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
