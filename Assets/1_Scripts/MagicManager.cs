using UnityEngine;
using System.Collections.Generic;

public class MagicManager : MonoBehaviour
{
    // 인스펙터에서 MagicData(SimpleMagicSO 또는 CompositeMagicSO)를 드래그 앤 드롭 가능
    public List<MagicData> myMagics;

    public void CastAll()
    {
        foreach (var m in myMagics)
        {
            m.Cast(transform.position);
        }
    }

    void Start()
    {
        CastAll();
    }
}
