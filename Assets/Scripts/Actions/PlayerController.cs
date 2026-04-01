using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Tutorial: https://www.youtube.com/watch?v=YnwOoxtgZQI

    private PlayerMovement controls;
    private CombatScript combat;

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collicionTilemap;
    [SerializeField] private bool MovementCost;
    [SerializeField] private LayerMask IgnoreLayer;

    public Transform test;

    Rigidbody2D rb2D;
    [HideInInspector] Vector3 MoveTo;

    private void Awake()
    {
        controls = new PlayerMovement();
        controls.Enable();
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
        rb2D = GetComponent<Rigidbody2D>();
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {

            MoveTo = transform.position + (Vector3)direction; // a quick refernce for this equation

            // RaycastHit check if enemy is there -- need to ignore player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector3)direction, 1f, ~IgnoreLayer);

            if (hit.collider != null)
            {
                //test.position = hit.collider.transform.position;
                //Debug.Log("Hit: " + hit.transform.tag);

                CheckHit(hit);
            }
            else
            {
                ActionMove();
            }

            if (MovementCost) { } //Loose move point
        }
    }

    private bool CanMove(Vector2 direction)
    {
        // This is the same as MoveTo but it send an error when replaced
        Vector3Int gridPostition = groundTilemap.WorldToCell(transform.position + (Vector3)direction); 

        // if there is no ground tile or colliction with wall tile return false imidiatly == no movement
        if (!groundTilemap.HasTile(gridPostition) || collicionTilemap.HasTile(gridPostition))
        { Debug.Log("Cannot Move"); return false; }
        Debug.Log("Move"); return true;
    }

    private void ActionMove()
    {
        transform.position = MoveTo;


        if (MovementCost) { } //Loose move point
    }

    private void CheckHit(RaycastHit2D hit)
    {
        string tag = hit.transform.tag;
        Debug.Log("Found Tag " + tag);

        //Comper Tags if raycast get hit
        switch (tag)
        {
            case "Enemy":
                Debug.Log("Enemy Found");
                //Combat
                break;

            case "NPC":
                Debug.Log("NPC Found");
                break;

            case "Dungean":
                Debug.Log("Dungean Found");
                break;

            default:
                ActionMove();
                break;
        }
    }
}
