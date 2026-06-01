using UnityEngine;
using UnityEngine.SceneManagement;

public class AlchamisthDialogue : MonoBehaviour
{
    public GameObject SelectBox;

    private void Start()
    {
        SelectBox.SetActive(false);
    }

    public void StartDialogue()
    {
        SelectBox.SetActive(true);
        MainStaticData.HoldMovement = false;
    }

    public void GoBack()
    {

        MainStaticData.HoldMovement = true;
    }

    public void GoAlchamist()
    {
        MainStaticData.HoldMovement = true;
        SceneManager.LoadScene("Alchemist SkillTree");
    }
}
