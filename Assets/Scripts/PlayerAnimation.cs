using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            _anim.SetBool("Turn_Left", true);
            _anim.SetBool("Turn_Right", false);
        }
        
        if (Input.GetKeyUp(KeyCode.A)) {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", true);
        } else if (Input.GetKeyUp(KeyCode.D)) {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }
    }
}
