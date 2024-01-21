using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{


    [SerializeField] protected Animator anim;

    private string currentAnimName;



    protected virtual void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }

}
