using UnityEngine;
using UnityEngine.SceneManagement;

public class BlacksmithDialoge : MonoBehaviour
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

    public void GoBlacksmith()
    {
        MainStaticData.HoldMovement = true;
        //SceneManager.LoadScene("Blacksmith SkillTree");
        MainStaticData.loadScreen.LoadScene(9);
    }
}
