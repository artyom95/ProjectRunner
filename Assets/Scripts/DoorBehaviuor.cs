using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviuor : MonoBehaviour
{
    [SerializeField] private GameObject _hinge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RotateDoor()
    {
        _hinge.transform.Rotate(new Vector3(0,-90,0));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
