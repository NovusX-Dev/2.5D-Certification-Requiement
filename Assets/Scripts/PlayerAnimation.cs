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

    void Start()
    {
        
    }

    void Update()
    {
        var zHorizontal = Input.GetAxisRaw("Horizontal");
        _anim.SetFloat("xMove", Mathf.Abs(zHorizontal));

        _anim.SetBool("jumping", _player.IsJumping);

    }

}
