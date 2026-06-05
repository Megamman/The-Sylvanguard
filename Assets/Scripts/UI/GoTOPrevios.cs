using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTOPrevios : MonoBehaviour
{

    public void previosSceen()
    {
        MainStaticData.loadScreen.LoadScene(MainStaticData.OldSceen);
    }
}
