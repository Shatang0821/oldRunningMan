using System.Collections;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float jumpInputBufferTime = 0.5f;

    WaitForSeconds waitJumpInputBufferTime;
    PlayerInputActions playerInputActions;

    Vector2 axes => playerInputActions.GamePlay.Axes.ReadValue<Vector2>();

    //ジャンプ関連 キーを押したらtrueになる
    public bool HasJumpInputBuffer { get; set; }

    public bool Jump => playerInputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool StopJump => playerInputActions.GamePlay.Jump.WasReleasedThisFrame();

    public bool Climb => playerInputActions.GamePlay.Climb.WasPressedThisFrame();
    public bool StopClimb => playerInputActions.GamePlay.Climb.WasReleasedThisFrame();

    

    public bool Move => AxisX != 0f;

    public float AxisX => axes.x;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();

        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }

    void OnEnable()
    {
        playerInputActions.GamePlay.Jump.canceled += delegate
        {
            HasJumpInputBuffer = false;
        };
    }

    void OnGUI()
    {
        Rect rect = new Rect(500, 500, 200, 200);
        string message = "Climb :" + Climb;
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.fontStyle = FontStyle.Bold;
        GUI.Label(rect, message, style);
    }

    public void EnableGamePlayInput()
    {
        playerInputActions.GamePlay.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;

        yield return waitJumpInputBufferTime;

        HasJumpInputBuffer = false;
    }
}
