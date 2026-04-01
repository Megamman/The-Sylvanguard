using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Tutorial: https://www.youtube.com/watch?v=YnwOoxtgZQI

    private PlayerMovement controls;

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collicionTilemap;

    private void Awake()
    {
        controls = new PlayerMovement();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {
            transform.position = (Vector3)direction;
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPostition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);

        // if there is no ground tile or colliction with wall tile return false imidiatly == no movement
        if (!groundTilemap.HasTile(gridPostition) || collicionTilemap.HasTile(gridPostition)) 
            return false;
        return true;
    }
}
