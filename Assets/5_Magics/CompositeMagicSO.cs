using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Composite Magic", menuName = "Magics/Composite")]
public class CompositeMagicSO : MagicData
{
    public List<MagicData> subMagics; // 다른 마법 데이터들을 리스트로 가짐
    public override string Name => magicName;

    public override int Cast(Vector3 position)
    {
        foreach (var magic in subMagics)
        {
            magic.Cast(position);
        }

        return 0;
    }
}
