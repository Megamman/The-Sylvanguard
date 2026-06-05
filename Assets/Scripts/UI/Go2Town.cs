using UnityEngine;
using UnityEngine.SceneManagement;

public class Go2Town : MonoBehaviour
{

    private PlayerMovement controls;

    private void Awake()
    {
        controls = new PlayerMovement();
        controls.Enable();
    }

    private void Start()
    {
        controls.Main.Interect.performed += ctx => GoHome();
        controls.Main.Back.performed += ctx => CloseThisWindow();

    }

    public void GoHome()
    {
        DungeanSaveData.LoadData(); //loading back data to not save collected resourses

        MainStaticData.loadScreen.LoadScene(11);
        //SceneManager.LoadScene("Town");
        //change scene
    }

    public void CloseThisWindow()
    {
        this.gameObject.SetActive(false);
    }
}
