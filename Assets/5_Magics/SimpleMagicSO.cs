using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Simple Magic", menuName = "Magics/Simple")]
public class SimpleMagicSO : MagicData
{
    public GameObject effectPrefab;
    public override string Name => magicName;
    public float duration = 0f;
    public float scale = 2f;

    public override int Cast(Vector3 position, Quaternion rotation)
    {
        if (effectPrefab != null)
        {
            UnityEngine.Debug.Log(Name);
            GameObject effect = Instantiate(effectPrefab, position, rotation);
            effect.transform.localScale = new Vector3(scale, scale, scale);
            if (duration > 0)
            {
                Destroy(effect, duration); // 지정된 시간 뒤에 자동 삭제
            }
        }

        return 0;
    }
}