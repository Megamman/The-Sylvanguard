using UnityEngine;

public class SkillSwitch : MonoBehaviour
{
    public bool SkillPurchase = false;
    public GameObject Skill;
    public GameObject[] Serios;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckSkill();
    }

    private void Update()
    {
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
