using Unity.VisualScripting;
using UnityEngine;

public class VSSkillScript : MonoBehaviour
{
    SkillSystem _skillSystem;
    public SkillSystem[] otherSkill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _skillSystem = GetComponent<SkillSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_skillSystem != null)
        {
            if(_skillSystem.currentLevel > 0)
            {
                for (int i = 0; i < otherSkill.Length; i++)
                {
                    otherSkill[i].LevelIsBlocked = true;
                }
            }
        }
    }
}
