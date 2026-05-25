using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntoDungean : MonoBehaviour
{

    public GoToDungean go;
    private PlayerMovement controls;


    private void Awake()
    {

        controls = new PlayerMovement();
        controls.Enable();
    }

    private void Start()
    {
        controls.Main.Interect.performed += ctx => GoInDungean();
        controls.Main.Back.performed += ctx => LeaveDungean();
    }
    public void GoInDungean()
    {
        MainStaticData.SpawnPosition = go.point;

        SceneManager.LoadScene(go.Dungean);
    }

    public void LeaveDungean()
    {
        MainStaticData.HoldMovement = false;
        gameObject.SetActive(false);
    }
}
