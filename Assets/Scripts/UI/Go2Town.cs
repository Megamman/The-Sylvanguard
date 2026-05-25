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
        DungeanSaveData.SaveData();

        SceneManager.LoadScene("Town");
        //change scene
    }

    public void CloseThisWindow()
    {
        this.gameObject.SetActive(false);
    }
}
