using UnityEngine;

public class SyncCode : MonoBehaviour
{

    public GameObject UICanvas;
    public GameObject DigitalMouse;
    public LoadScreen loadScreen;

    private void Awake() {
        MainStaticData.loadScreen = loadScreen;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UICanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        DigitalMouse.SetActive(MainStaticData.DigitalMouse);
    }
}
