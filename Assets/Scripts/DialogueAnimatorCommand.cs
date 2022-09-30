using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueAnimatorCommand : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    [YarnCommand("PlayAnimation")]
    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }
}
