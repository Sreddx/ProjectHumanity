using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAnimated : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _comboDelay = 1.2f;

    private int _numberOfClicks = 0;
    private float _lastClickTime = 1.0f;


    void Update(){
        if(Time.time - _lastClickTime > _comboDelay)
        {
            _numberOfClicks = 0;
            _animator.SetBool("Idle", true);
        }

        if(Input.GetMouseButtonDown(0)){
            _lastClickTime = Time.time;
            _numberOfClicks++;

            if(_numberOfClicks == 1)
            {
                _animator.SetTrigger("Hit1");
                _animator.SetBool("Idle", false);
                _numberOfClicks = Mathf.Clamp(_numberOfClicks, 0, 3);
            }
            if(_numberOfClicks == 2)
            {
                _animator.SetTrigger("Hit2");
                _animator.SetBool("Idle", false);
                _numberOfClicks = Mathf.Clamp(_numberOfClicks, 0, 3);
            }
            if(_numberOfClicks == 3)
            {
                _animator.SetTrigger("Hit3");
                _animator.SetBool("Idle", false);
                _numberOfClicks = Mathf.Clamp(_numberOfClicks, 0, 3);
            }
        }
    }
}
