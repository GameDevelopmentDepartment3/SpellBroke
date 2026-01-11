using UnityEngine;
using System.Collections.Generic;
using System.Net;

public class MagicManager : MonoBehaviour
{
    // 인스펙터에서 MagicData(SimpleMagicSO 또는 CompositeMagicSO)를 드래그 앤 드롭 가능
    public List<MagicData> myMagics;
    public GameObject[] targets;
    public TargetDetector detector;
    
    private PlayerControls _controls;
    private int firstSelect = 1; // electricity: 1, fire: 3, ice: 7
    private int secondSelect = 0;

    void Awake()
    {
        _controls = new PlayerControls();
    }

    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();

    void Update()
    {
        if (!_controls.Player.SecondSelect.IsPressed())
        {
            if (_controls.Player.Electricity.triggered)
            {
                firstSelect = 1;
            }
            else if (_controls.Player.Fire.triggered)
            {
                firstSelect = 3;
            }
            else if (_controls.Player.Ice.triggered)
            {
                firstSelect = 7;
            }
            if (_controls.Player.Casting.triggered)
            {
                Casting(1);
            }
        }
        else
        {
            if (_controls.Player.Electricity.triggered)
            {
                secondSelect = 1;
            }
            else if (_controls.Player.Fire.triggered)
            {
                secondSelect = 3;
            }
            else if (_controls.Player.Ice.triggered)
            {
                secondSelect = 7;
            }
            if (_controls.Player.Casting.triggered)
            {
                Casting(2);
            }
        }
    }

    private void Casting(int type)
    {
        int w = firstSelect;
        if (type == 2) w += secondSelect;
        Vector3 target = Vector3.zero;
        if (detector.GetClosestEnemy() != null)
        {
            Debug.Log($"{detector.GetClosestEnemy().name}");
            target = detector.GetClosestEnemy().position + (Vector3.up * 3);
        }
        switch (w)
        {
            case 1:
                {
                    myMagics[0].Cast(targets[0].transform.position);
                } break; // Elec
            case 3:
                {
                    if (target == Vector3.zero)
                    {
                        myMagics[1].Cast(targets[0].transform.position);
                    }
                    else
                    {
                        myMagics[1].Cast(target);
                    }
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
            case 10:
                {
                    myMagics[9].Cast(targets[2].transform.position);
                } break; // FireIce
            case 14:
                {
                    myMagics[10].Cast(targets[2].transform.position);
                } break; // IceIce
        }
    }
}