using UnityEngine;
using UnityEngine.SceneManagement;

public class IntoDungean : MonoBehaviour
{

    public GoToDungean go;
    public void GoInDungean()
    {
        MainStaticData.SpawnPosition = go.point;

        SceneManager.LoadScene(go.Dungean);
    }
}
