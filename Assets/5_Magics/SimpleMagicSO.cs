using UnityEngine;

[CreateAssetMenu(fileName = "New Simple Magic", menuName = "Magics/Simple")]
public class SimpleMagicSO : MagicData
{
    public GameObject effectPrefab;
    public override string Name => magicName;

    public override void Cast(Vector3 position)
    {
        if (effectPrefab != null)
            Instantiate(effectPrefab, position, Quaternion.identity);
    }
}