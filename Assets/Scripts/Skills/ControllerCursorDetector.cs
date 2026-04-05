using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerCursorDetector : MonoBehaviour
{
    private SkillSystem _skillSystem;
    private GameObject currentHoverTarget;
    private SkillTreeMovement controls;


    public InputActionReference myAction; // Drag your Action here from the Inspector

    private void OnEnable() => myAction.action.Enable();
    private void OnDisable() => myAction.action.Disable();

    private void Start()
    {
        _skillSystem = GetComponent<SkillSystem>();
    }

    void Update()
    {
        // 1. Get the current mouse position (which your controller is warping)
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // 2. Fire a single ray from the camera to that point
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            // Only trigger if we moved to a NEW object
            if (hitObject != currentHoverTarget)
            {
                _skillSystem.OnMouseIsOver();
                currentHoverTarget = hitObject;
                // triggered when the button is fully pressed
                controls.Player.Interect.performed += ctx => _skillSystem.PurchaseSkill();
            }
        }
        else if (currentHoverTarget != null)
        {
            // We moved into empty space
            _skillSystem.OnMouseIsExit();
            currentHoverTarget = null;
        }
    }
}
