using UnityEngine;

public class CurserSetup : MonoBehaviour
{
    public bool curser;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cursor.visible = false;
    }

    private void Update()
    {
        if (!curser && MainStaticData.CurserContol) { MainStaticData.CurserContol = true; }
    }
}
