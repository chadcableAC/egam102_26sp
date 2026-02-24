using UnityEngine;

public class ShakeController : MonoBehaviour
{
    public Animator animator;

    public void Shake()
    {
        animator.SetTrigger("IsShake");
    }
}
