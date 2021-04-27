using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Player _player;
    Animator _anim;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _anim = GetComponent<Animator>();    
    }

    void Update()
    {
        var zHorizontal = Input.GetAxisRaw("Horizontal");
        _anim.SetFloat("xMove", Mathf.Abs(zHorizontal));

        var yVertical = Input.GetAxisRaw("Vertical");

        _anim.SetBool("isClimbingLadder", yVertical != 0);

        _anim.SetBool("jumping", _player.IsJumping);

        if (Input.GetKeyDown(KeyCode.W) && _player.GrabbedLedge)
        {
            _anim.SetTrigger("climbUp");
        }

        _anim.SetBool("grabbedLedge", _player.GrabbedLedge);

        _anim.SetBool("climbingLadder", _player.OnLadder);

        _anim.SetBool("push", _player.IsPushing);
    }


    public void BlowAKiss()
    {
        _anim.SetTrigger("kiss");
    }

    public void ClimbUpFromLadder()
    {
        _anim.SetTrigger("climbUpLadderTrigger");
    }

    public void DodgePlayer()
    {
        _anim.SetTrigger("roll");
    }

}
