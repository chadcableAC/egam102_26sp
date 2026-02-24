using UnityEngine;

public class FlashController : MonoBehaviour
{
    public Animator animator;

    public void Flash()
    {
        animator.SetTrigger("IsFlash");
    }
}
