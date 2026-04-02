using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSpriteData", menuName = "ScriptableObjects/PlayerSpriteData", order = 1)]
public class PlayerAnimationSO : ScriptableObject
{
    public Sprite LookUp;
    public Sprite LookDown;
    public Sprite LookLeft;
    public Sprite LookRight;
}
