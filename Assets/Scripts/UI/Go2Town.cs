using UnityEngine;

public class Go2Town : MonoBehaviour
{
    public void GoHome()
    {
        DungeanSaveData.SaveData();
        //change scene
    }

    public void CloseThisWindow()
    {
        this.gameObject.SetActive(false);
    }
}
