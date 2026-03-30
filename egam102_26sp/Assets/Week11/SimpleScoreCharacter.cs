using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleScoreCharacter : MonoBehaviour
{
    InputAction moveAction;
    InputAction powerupAction;

    public ScorePopup popupPrefab;
    int popupCounter = 0;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        powerupAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        Vector2 moveThisFrame = moveAction.ReadValue<Vector2>() * Time.deltaTime;
        transform.position += (Vector3) moveThisFrame;

        if (powerupAction.WasPressedThisFrame())
        {
            popupCounter += 1;

            ScorePopup popup = Instantiate(popupPrefab);
            popup.transform.position = transform.position;

            popup.SetScore(popupCounter * 100);
        }
    }
}
