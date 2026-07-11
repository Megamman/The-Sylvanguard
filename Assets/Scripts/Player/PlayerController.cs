using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Tutorial: https://www.youtube.com/watch?v=YnwOoxtgZQI

    private CombatScript combat;
    private PlayerMovement controls;
    //private Rigidbody2D rb2D;

    public GameObject PunchEffect;
    public GameObject SlashEffect;

    bool usingSword;

    private SpriteRenderer PlayerSprite;
    private Vector2 direction;
    [HideInInspector] Vector3 MoveTo;
    [SerializeField] private GameObject DungeanPannel;
    public GameObject Curser;

    [SerializeField] private PlayerAnimationSO[] PASO;
    [SerializeField] private int PlayerSpriteLevel;

    [SerializeField] private bool MovementCost;
    [SerializeField] private LayerMask IgnoreLayer;

    [SerializeField] private IntoDungean intoDungean;
    EndGame endGame;

    float HpQuorter;
    int MaxHP;
    int MagicSteps;
    int HealthSteps;

    bool Deadlock;
    bool Turnout;

    private void Awake()
    {
        PlayerSprite = GetComponent<SpriteRenderer>();
        combat = GetComponent<CombatScript>();
        endGame = GetComponent<EndGame>();

        controls = new PlayerMovement();
        

        MaxHP = Stats.HP;
        HpQuorter = Stats.HP / 4f;
        HealthSteps = Stats.StepsToHPRegen;
        MagicSteps = Stats.StepsToMPRegen;
    }

    private void OnEnable()
    { if (controls != null) controls.Enable(); }

    private void OnDisable()
    { 
        if (controls != null) controls.Disable(); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainStaticData.HoldMovement = true;
        controls.Enable();
        //rb2D = GetComponent<Rigidbody2D>();
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        if (DungeanPannel != null ) DungeanPannel.SetActive(false);


    }
    void Update()
    {
        Curser.SetActive(MainStaticData.CurserContol);


        if (Stats.MovePt <= 0 || Stats.HP <= 0) Deadlock = true;

        if (Deadlock && !Turnout)
        {
            
            if (Stats.MovePt <= 0) { endGame.GetEndGame("Out of Steps"); Curser.SetActive(true); }
            if (Stats.HP <= 0) { endGame.GetEndGame("Health too Low"); Curser.SetActive(true); }
            Turnout = true; //calls this function once
        }
    }

    private void Move(Vector2 direct)
    {
        if (MainStaticData.HoldMovement) // Stops player from moving
        {
            direction = new Vector2(Mathf.RoundToInt(direct.x), Mathf.RoundToInt(direct.y));

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) { direction.y = 0; } else { direction.x = 0; }

            LookAt();

            MoveTo = transform.position + (Vector3)direction; // a quick refernce for this equation

            // RaycastHit check if enemy is there -- need to ignore player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector3)direction, 1f, ~IgnoreLayer);

            if (hit.collider != null) { CheckHit(hit); } else { ActionMove(); }
        }        
    }

    private void ActionMove()
    {

        DungeanPannel.SetActive(false);
        //transform.position = MoveTo;

        StartCoroutine(AnimMove(MoveTo));

        if (Stats.QuickPotion) { if (Stats.HP < HpQuorter) { UsePotion(); } } //use potion
        if (Stats.HPRegenIsOn) { if (HealthSteps == 0) { Stats.HP += Stats.HPRegen; HealthSteps = Stats.StepsToHPRegen; }
            else { HealthSteps--; } }
        if (Stats.MPRegenIsOn) { if (MagicSteps == 0) { Stats.HP += 1; MagicSteps = Stats.StepsToMPRegen; }
            else { MagicSteps--; } }
        if (MovementCost) { if (Stats.MovePt != 0) { StartCoroutine(AnimMove(MoveTo)); } } //Loose move point
            else { StartCoroutine(AnimMove(MoveTo)); } //Free Move

        if (MovementCost) { Stats.MovePt--; } //Loose move point
    }


    public void UsePotion()
    {
        int totalGeneratrd = Stats.HP + Stats.PotionHeal;

        if(totalGeneratrd > MaxHP) { Stats.HP = MaxHP; }
        else { Stats.HP += Stats.PotionHeal; }

        Stats.MaxPotions--;
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
        //Debug.Log("Found Tag: " + tag);

        //Comper Tags if raycast get hit
        switch (tag)
        {
            case "Enemy":
                //Debug.Log("Enemy Found");
                //Combat
                StartCoroutine(AttackAnime(MoveTo, hit));
                break;

            case "Wall":
                //Debug.Log("Wall Found");
                break;

            case "NPC":
                //Debug.Log("NPC Found");
                break;

            case "Dungean":
                //Curser.SetActive(true);
                MainStaticData.CurserContol = true;
                GoToDungean goToDungean = hit.transform.GetComponent<GoToDungean>();
                intoDungean.go = goToDungean;
                MainStaticData.HoldMovement = false;
                DungeanPannel.SetActive(true);
                //Debug.Log("Dungean Found");

                break;

            case "Blacksmith":
                //Curser.SetActive(true);
                MainStaticData.CurserContol = true;
                hit.transform.GetComponent<BlacksmithDialoge>().StartDialogue();
                break;

            case "Alchamist":
                //Curser.SetActive(true);
                MainStaticData.CurserContol = true;
                hit.transform.GetComponent<AlchamisthDialogue>().StartDialogue();
                break;
            case "Sign":
                break;

            default:
                ActionMove();
                break;
        }

    }

    bool inPos = true;
    float duration = 0.2f;
    bool isMoving;

    IEnumerator AttackAnime(Vector3 destination, RaycastHit2D hit)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;

        // 1. Move to target position
        yield return StartCoroutine(MoveCharacter(destination));

        combat.Fight(hit.transform.GetComponent<EnemyStats>());


        if (usingSword) { Instantiate(SlashEffect, hit.transform.position, Quaternion.identity); }
        else { Instantiate(PunchEffect, hit.transform.position, Quaternion.identity); }

// 2. Wait at target position
yield return new WaitForSeconds(duration);

        // 3. Move back to start position
        yield return StartCoroutine(MoveCharacter(startPosition));

        isMoving = false;
    }

    IEnumerator AnimMove(Vector3 pos)
    {
        // Double-check: If we aren't standing still, ignore this spam input completely
        if (!inPos) yield break;

        Debug.Log("Move");

        inPos = false; // Instantly lock movement
        yield return StartCoroutine(MoveCharacter(pos));
    }

    IEnumerator MoveCharacter(Vector3 destination)
    {
        Vector3 origin = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentageComplete = elapsed / duration;
            float smoothPercentage = Mathf.SmoothStep(0f, 1f, percentageComplete);

            transform.position = Vector3.Lerp(origin, destination, smoothPercentage);
            yield return null;
        }

        transform.position = destination;

        // ONLY unlock movement here, when the character has fully arrived
        inPos = true;
    }



}
