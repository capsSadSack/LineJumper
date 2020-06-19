using System;
using UnityEngine;

public class MenuButtonBehaviour : MonoBehaviour
{
    public Animator anim;

    public void OnMouseEnter()
    {
        anim.SetBool("isSelected", true);
    }

    public void OnMouseExit()
    {
        anim.SetBool("isSelected", false);
        anim.SetBool("isPressed", false);
    }

    public void OnMouseDown()
    {
        anim.SetBool("isPressed", true);
    }
}
