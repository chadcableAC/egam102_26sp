using UnityEngine;
using UnityEngine.InputSystem;

public class JumpCharacter : MonoBehaviour
{
    InputAction jumpAction;

    public Animator animator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.WasPressedThisFrame())
        {
            animator.SetTrigger("IsJump");
        }
    }
}
