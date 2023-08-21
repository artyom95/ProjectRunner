using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
     private Animator _animator;

    private static readonly int IsGoing = Animator.StringToHash("IsGoing");
    private static readonly int IsItTimeToDance = Animator.StringToHash("IsItTimeToDance");

    // Start is called before the first frame update
    public void Initialize(GameObject player)
    {
        _animator = player.GetComponent<Animator>();
    }

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
        _animator.SetBool(IsItTimeToDance, true);
    }
    public void StopDancing()
    {
        _animator.SetBool(IsItTimeToDance, false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
