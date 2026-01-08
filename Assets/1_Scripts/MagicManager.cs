using UnityEngine;
using System.Collections.Generic;

public class MagicManager : MonoBehaviour
{
    // 인스펙터에서 MagicData(SimpleMagicSO 또는 CompositeMagicSO)를 드래그 앤 드롭 가능
    public List<MagicData> myMagics;
    public GameObject[] targets;
    public TargetDetector detector;
    
    private PlayerControls _controls;
    private Queue<int> elements = new(); // electricity: 1, fire: 3, ice: 7

    void Awake()
    {
        _controls = new PlayerControls();
    }

    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();

    void Update()
    {
        if (_controls.Player.Electricity.triggered)
        {
            Debug.Log("E");
            elements.Enqueue(1);
        }
        else if (_controls.Player.Fire.triggered)
        {
            Debug.Log("F");
            elements.Enqueue(3);
        }
        else if (_controls.Player.Ice.triggered)
        {
            Debug.Log("I");
            elements.Enqueue(7);
        }
        if (elements.Count > 2)
        {
            elements.Dequeue();
        }
        if (_controls.Player.Casting.triggered)
        {
            Debug.Log("C");
            Casting();
        }
    }

    private void CastAll()
    {
        foreach (var m in myMagics)
        {
            m.Cast(transform.position);
        }
    }

    private void Casting()
    {
        int w = 0;
        for (int i = 0; i < 2; i++)
        {
            if (elements.Count > 0) w += elements.Dequeue();
        }
        Vector3 target;
        if (detector.GetClosestEnemy() != null)
        {
            Debug.Log($"{detector.GetClosestEnemy().name}");
            target = detector.GetClosestEnemy().position;
        }
        else
            target = targets[0].transform.position;
        switch (w)
        {
            case 1:
                {
                    myMagics[0].Cast(targets[0].transform.position);
                } break; // Elec
            case 3:
                {
                    myMagics[1].Cast(targets[0].transform.position);
                } break; // Fire
            case 7:
                {
                    myMagics[2].Cast(targets[0].transform.position);
                } break; // Ice
            case 2:
                {
                    myMagics[3].Cast(targets[1].transform.position);
                    myMagics[4].Cast(targets[1].transform.position);
                } break; // ElecElec
            case 4:
                {
                    myMagics[5].Cast(targets[0].transform.position);
                } break; // ElecFire
            case 8:
                {
                    myMagics[6].Cast(targets[0].transform.position);
                    myMagics[7].Cast(targets[0].transform.position);
                } break; // ElecIce
            case 6:
                {
                    myMagics[8].Cast(targets[1].transform.position);
                } break; // FireFire
            // case 10: break; // FireIce
            // case 14: break; // IceIce
        }
    }
}