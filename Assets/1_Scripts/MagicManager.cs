using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net;

public class MagicManager : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip iceSound;
    public AudioClip elecSound;
    public AudioClip elcelcSound;
    public AudioClip elcefireSound;
    public AudioClip elceiceSound;
    public AudioClip firefireSound;
    public AudioClip fireiceSound;
    public AudioClip iceiceSound;

    [Header("Magics")]
    // 인스펙터에서 MagicData(SimpleMagicSO 또는 CompositeMagicSO)를 드래그 앤 드롭 가능
    public List<MagicData> myMagics;
    public GameObject[] targets;
    public TargetDetector detector;

    [Header("UI Settings")]
    // 인스펙터에서 전기(0), 불(1), 얼음(2) 순서대로 Image를 넣어주세요.
    public Image[] uiIcons; 
    public Color defaultColor = new Color(0, 0, 0, 0);
    public Color firstColor = new Color(137, 209, 137);
    public Color secondColor = new Color(245, 233, 54);
    public Color combinedColor = new Color(231, 144, 14); // 초록+노랑이 겹칠 때 색상 (주황색 예시)
    
    [Header("Cooldown")]
    public float singleCooldownTime = 0.5f; // 입력 무시 시간
    public float doubleCooldownTime = 1f;
    private float singleLastInputTime = 0f; // 마지막 입력 시점 저장
    private float doubleLastInputTime = 0f;

    private PlayerControls _controls;
    private int firstSelect = 3; // electricity: 1, fire: 3, ice: 7
    private int secondSelect = 3;
    
    

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
            if (_controls.Player.Electricity.triggered) firstSelect = 1;
            else if (_controls.Player.Fire.triggered) firstSelect = 3;
            else if (_controls.Player.Ice.triggered) firstSelect = 7;

            if (_controls.Player.Casting.triggered && Time.time >= singleLastInputTime + singleCooldownTime)
            {
                singleLastInputTime = Time.time;
                Casting(1);
            } 
        }
        else
        {
            if (_controls.Player.Electricity.triggered) secondSelect = 1;
            else if (_controls.Player.Fire.triggered) secondSelect = 3;
            else if (_controls.Player.Ice.triggered) secondSelect = 7;

            if (_controls.Player.Casting.triggered && Time.time >= doubleLastInputTime + doubleCooldownTime)
            {
                doubleLastInputTime = Time.time;
                Casting(2);
            }
        }

        UpdateUI(); // 입력이 없더라도 매 프레임 UI 상태를 갱신합니다.
    }

    private void UpdateUI()
    {
        // 1: 전기, 3: 불, 7: 얼음 인덱스 매핑
        int firstIdx = GetIndexFromValue(firstSelect);
        int secondIdx = GetIndexFromValue(secondSelect);

        for (int i = 0; i < uiIcons.Length; i++)
        {
            bool isFirst = (i == firstIdx);
            bool isSecond = (i == secondIdx);

            if (isFirst && isSecond)
                uiIcons[i].color = combinedColor; // 두 선택이 겹칠 때
            else if (isFirst)
                uiIcons[i].color = firstColor;    // 초록색
            else if (isSecond)
                uiIcons[i].color = secondColor;   // 노란색
            else
                uiIcons[i].color = defaultColor;  // 기본 투명
        }
    }

    private int GetIndexFromValue(int value)
    {
        if (value == 1) return 0; // Elec
        if (value == 3) return 1; // Fire
        if (value == 7) return 2; // Ice
        return -1;
    }

    IEnumerator PlaySoundDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
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
                    myMagics[0].Cast(targets[0].transform.position, Quaternion.identity);
                    audioSource.PlayOneShot(elecSound);
                } break; // Elec
            case 3:
                {
                    if (target == Vector3.zero)
                    {
                        myMagics[1].Cast(targets[0].transform.position, Quaternion.identity);
                        audioSource.PlayOneShot(fireSound);
                    }
                    else
                    {
                        myMagics[1].Cast(target, Quaternion.identity);
                        audioSource.PlayOneShot(fireSound);
                    }
                } break; // Fire
            case 7:
                {
                    myMagics[2].Cast(targets[0].transform.position, Quaternion.identity);
                    audioSource.PlayOneShot(iceSound);
                } break; // Ice
            case 2:
                {
                    myMagics[3].Cast(targets[1].transform.position, Quaternion.identity);
                    myMagics[4].Cast(targets[1].transform.position, Quaternion.identity);
                    audioSource.PlayOneShot(elcelcSound);
                } break; // ElecElec
            case 4:
                {
                    myMagics[5].Cast(targets[0].transform.position, Quaternion.identity);
                    audioSource.PlayOneShot(elcefireSound);
                } break; // ElecFire
            case 8:
                {
                    myMagics[6].Cast(targets[0].transform.position, Quaternion.identity);
                    myMagics[7].Cast(targets[0].transform.position, Quaternion.identity);
                    audioSource.PlayOneShot(elceiceSound);
                } break; // ElecIce
            case 6:
                {
                    myMagics[8].Cast(targets[1].transform.position, Quaternion.identity);
                    StartCoroutine(PlaySoundDelay(firefireSound, 0.5f));
                } break; // FireFire
            case 10:
                {
                    myMagics[9].Cast(targets[2].transform.position, targets[2].transform.rotation);
                    audioSource.PlayOneShot(fireiceSound);
                } break; // FireIce
            case 14:
                {
                    myMagics[10].Cast(targets[2].transform.position, Quaternion.identity);
                    audioSource.PlayOneShot(iceiceSound);
                } break; // IceIce
        }
    }
}