using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
     private Animator _animator;

    private static readonly int IsGoing = Animator.StringToHash("IsGoing");
    private static readonly int IsTimeToDance = Animator.StringToHash("IsTimeToDance");
    

   

    public void Walk()
    {
        _animator.SetBool(IsGoing, true);
    }
    public void StopWalking()
    {
        _animator.SetBool(IsGoing, false);
    }

    public void Dance()
    {
      _animator.SetBool(IsTimeToDance,true);
      
    }
    [UsedImplicitly]
    public void StopDancing()
    {
        _animator.SetBool(IsTimeToDance,false);
      
    }
}
