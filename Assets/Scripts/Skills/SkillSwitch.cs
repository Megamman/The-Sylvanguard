using UnityEngine;

public class SkillSwitch : MonoBehaviour
{
    private bool SkillPurchase = false;
    public SkillSystem LeadSkill;
    public GameObject Skill;
    public GameObject[] Serios;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckSkill();
    }

    private void Update()
    {
        if(LeadSkill.currentLevel >= 1 ) {SkillPurchase = true; } else {SkillPurchase = false; }
        if (SkillPurchase && !Skill.activeSelf) { CheckSkill(); }
    }

    public void CheckSkill()
    {
        
        int i = 0;

        foreach (GameObject s in Serios)
        {
            if (s.activeSelf)
            {
                i++;
            }
        }

        if (i == (Serios.Length) && SkillPurchase)
        {
            Skill.SetActive(true);

        }
        else
        {
            Skill.SetActive(false);
        }
    }
}
