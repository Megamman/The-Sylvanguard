using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class SkillCameraControl : MonoBehaviour
{
    //Tutorial: https://www.youtube.com/watch?v=Y3WNwl1ObC8

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private float cursorSpeed = 1000f;
    [SerializeField] private float padding = 40f;
    [SerializeField] private Rigidbody2D cameraRB;

    private bool previousMouseState;
    private Mouse virtualMouse;
    private Mouse virtualCamera;
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;

        GetVirtualMouse();
        GetVirtualCamera();

        InputSystem.onAfterUpdate += UpdateMotion;

        //Disable mouse viuals
    }

    private void GetVirtualMouse()
    {
        if (virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 postion = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, postion);
        }
    }

    private void GetVirtualCamera()
    {
        if (virtualCamera == null)
        {
            virtualCamera = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualCamera.added)
        {
            InputSystem.AddDevice(virtualCamera);
        }

        InputUser.PerformPairingWithDevice(virtualCamera, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 postion = cursorTransform.anchoredPosition;
            InputState.Change(virtualCamera.position, postion);
        }
    }

    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion()
    {
        if (virtualMouse == null || Gamepad.current == null) { return; }

        LeftStickMovment();
        RightStickMovment();
    }

    private void LeftStickMovment()
    {
        Vector2 StickValue = Gamepad.current.leftStick.ReadValue();
        StickValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + StickValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height - padding);

        if (StickValue != Vector2.zero)
        {
            //Debug.Log("Curser Changed");
            Mouse.current.WarpCursorPosition(newPosition);
        }
        else
        {
            newPosition = Mouse.current.position.ReadValue(); ;
        }

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, StickValue);

        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();

        if (previousMouseState != aButtonIsPressed)
        {

            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = aButtonIsPressed;
        }

            AnchorCurser(newPosition);
    }

    private void RightStickMovment()
    {
        float StickValueX = Gamepad.current.rightStick.ReadValue().x;
        float StickValueY = Gamepad.current.rightStick.ReadValue().y;

        StickValueX *= cursorSpeed * 2 * Time.deltaTime;
        StickValueY *= cursorSpeed * 2 * Time.deltaTime;

        cameraRB.linearVelocity = new Vector2(StickValueX, StickValueY);
    }

    private void AnchorCurser(Vector2 position)
    {
        Vector2 anchoredPostion;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode
            == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPostion);
        cursorTransform.anchoredPosition = anchoredPostion;

        //Debug.Log("Curser Changed");
    }
}
