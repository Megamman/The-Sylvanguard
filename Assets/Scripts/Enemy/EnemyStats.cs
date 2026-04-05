using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Data")]
    public string Name;
    public string Decription;

    //Main Stats
    [Header("Enemy Main Stats")]
    public int HP;
    private int Health;
    public int Attack;
    public bool UsingMagicAttack;

    //Collection
    [Header("Defeat Rewards")]
    public int CoinsMin;
    public int CoinsMax;
    public int XPMin;
    public int XPMax;

    //Secondary Stats
    [Header("Enemy Secondary Stats")]
    public int DodgeChance;
    public int CritChance;
    public int CritMaxDamage;
    public bool SneakAttack;

    [Header("Animation")]
    public bool isAnimated;
    private Animator m_Animator;

    //Weakness
    [Header("Enemy Weakness")]
    public bool WeakToPhysical;
    public bool WeakToMagic;

    //Resistant
    [Header("Enemy Resistant")]
    public bool ResistToPhysical;
    public bool ResistToMagic;

    [Header("UI")]
    public Slider HPSlider;
    //Info


    private void Start()
    {
        if (isAnimated) { m_Animator = GetComponent<Animator>(); }
        Health = HP;
        HPSlider.maxValue = HP;

        HPSlider.transform.gameObject.SetActive(false);
    }

    private void Update()
    {
        HPSlider.value = HP;

        if (Health > HP)
        {
            ActivateAnimation();
        }
    }

    public void ActivateAnimation()
    {
        HPSlider.transform.gameObject.SetActive(true);
        if (isAnimated) { m_Animator.SetBool("Active", true); }
    }
}
