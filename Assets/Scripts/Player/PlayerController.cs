using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Tutorial: https://www.youtube.com/watch?v=YnwOoxtgZQI

    private CombatScript combat;
    private PlayerMovement controls;
    //private Rigidbody2D rb2D;
    private SpriteRenderer PlayerSprite;
    private Vector2 direction;
    [HideInInspector] Vector3 MoveTo;

    [SerializeField] private PlayerAnimationSO[] PASO;
    [SerializeField] private int PlayerSpriteLevel;

    [SerializeField] private bool MovementCost;
    [SerializeField] private LayerMask IgnoreLayer;

    private void Awake()
    {
        PlayerSprite = GetComponent<SpriteRenderer>();
        combat = GetComponent<CombatScript>();
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
        //rb2D = GetComponent<Rigidbody2D>();
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direct)
    {

        direction = new Vector2(Mathf.RoundToInt(direct.x), Mathf.RoundToInt(direct.y));

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) { direction.y = 0; } else { direction.x = 0; }

        LookAt();

        MoveTo = transform.position + (Vector3)direction; // a quick refernce for this equation

        // RaycastHit check if enemy is there -- need to ignore player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector3)direction, 1f, ~IgnoreLayer);

        if (hit.collider != null)
        {
            CheckHit(hit);
        }
        else
        {
            ActionMove();
        }

        if (MovementCost) { } //Loose move point
    }

    private void ActionMove()
    {
        transform.position = MoveTo;

        if (MovementCost) { if (Stats.MovePt != 0) { transform.position = MoveTo; } } //Loose move point
        else { transform.position = MoveTo; } //Free Move
    }

    void LookAt()
    {
        PlayerAnimationSO playerSprite = PASO[PlayerSpriteLevel];

        if (direction.y < 0) { PlayerSprite.sprite = playerSprite.LookDown; } //Look Down
        if (direction.y > 0) { PlayerSprite.sprite = playerSprite.LookUp; } //Look Up
        if (direction.x < 0) { PlayerSprite.sprite = playerSprite.LookLeft; } //Look Left
        if (direction.x > 0) { PlayerSprite.sprite = playerSprite.LookRight; } //Look Right
    }

    private void CheckHit(RaycastHit2D hit)
    {
        string tag = hit.transform.tag;
        Debug.Log("Found Tag: " + tag);

        //Comper Tags if raycast get hit
        switch (tag)
        {
            case "Enemy":
                Debug.Log("Enemy Found");
                //Combat
                combat.Fight(hit.transform.GetComponent<EnemyStats>());
                break;

            case "Wall":
                Debug.Log("Wall Found");
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
