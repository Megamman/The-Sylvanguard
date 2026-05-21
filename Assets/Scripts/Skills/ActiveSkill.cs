using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    public GameObject MainTarget;

    public GameObject[] ActiveTargets;
    public GameObject[] ActiveSkillTargets;

    private int Counter = 0;
    private int MaxCounter;

    // ActiveTargets are Empty GameObjects that are activated when a condition is met
    // Example if dungean3 is available then skills in Tier 3 can be unlocked

    // Once the tier is unloacked togheter wuith the previos skill that MainTarget Coms from
    // MainTarget can be activated

    // This scripts needs to be in an object and connecetd to each activeTarget to triger CheckCounter

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckCounter();
    }

    private void Update()
    {
        if (Counter == MaxCounter) { MainTarget.SetActive(true); }
        else { MainTarget.SetActive(false); }
    }

    public void CheckCounter()
    {
        MaxCounter = 0;
        Counter = ActiveTargets.Length + ActiveSkillTargets.Length;

        for (int i = 0; i < ActiveTargets.Length; i++)
        {
            if (ActiveTargets[i].activeSelf)
            {
                Counter++;
            }
        }
    }
}
