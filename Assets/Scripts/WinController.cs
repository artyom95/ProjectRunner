using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
  
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private DoorBehaviuor _doorBehaviuor;

    public void CelebrateWin()
    {
       _doorBehaviuor.RotateDoor();
        _animatorController.Dance();
      
    }
   
}
