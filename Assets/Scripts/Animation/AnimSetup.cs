using UnityEngine;

public class AnimSetup : MonoBehaviour
{  
    public void PauseMovement()
    {
        MainStaticData.HoldMovement = false;
    }
    public void ReseumMovement()
    {
        MainStaticData.HoldMovement = true;
    }
}
