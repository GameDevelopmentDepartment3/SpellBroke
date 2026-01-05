using UnityEngine;

public class OrbitalTest : MonoBehaviour
{
    private OrbitalManager _manager;
    private PlayerControls _controls;

    void Awake()
    {
        _manager = GetComponent<OrbitalManager>();
        _controls = new PlayerControls();

        // 'O' 키를 눌렀을 때 (인덱스 0번 블루 크리스탈 추가)
        _controls.Player.AddOrbital.performed += ctx => _manager.AddOrbital(0);

        // 'P' 키를 눌렀을 때 (제거 및 강화)
        _controls.Player.RemoveOrbital.performed += ctx => _manager.RemoveOrbital();
    }

    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();
}