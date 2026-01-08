using UnityEngine;

public class AugmentButton : MonoBehaviour
{
    public enum AugmentType
    {
        HP,
        Attack,
        Speed
    }
    public AugmentType type;

    [SerializeField] private PlayerStatsManager player;
    [SerializeField] private AugmentUI augmentUI;
    public void SelectAugment()
    {
        switch (type)
        {
            case AugmentType.HP:
                player.AddHP(110000f);
                break;
            
            case AugmentType.Attack:
                player.AddAttack(5f);
                break;

            case AugmentType.Speed:
                player.AddSpeed(11000f);
                break; 
        }
        augmentUI.Close(); // 선택 후 닫기
    }
}