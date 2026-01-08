using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class OrbitalManager : MonoBehaviour
{
    [Header("Target")]
    public Transform playerTransform;

    [Header("Dynamic Settings (Base)")]
    public float baseRadius = 6f;       // 최소 반지름 (갯수가 적을 때)
    public float radiusPerObject = 0.3f;  // 갯수당 추가될 반지름
    public float maxRadius = 7f;          // 최대 반지름 제한

    public float baseSpeed = 200f;        // 최대 속도 (갯수가 적을 때)
    public float speedDecreasePerObject = 20f; // 갯수당 감소할 속도
    public float minSpeed = 50f;          // 최소 속도 제한

    [Header("Follow Settings")]
    public float followSmoothTime = 0.05f;

    [Header("Prefabs")]
    public GameObject[] crystalPrefabs;

    private List<GameObject> _activeOrbitals = new List<GameObject>();
    private float _currentRotationAngle;
    private Vector3 _followVelocity;

    // 현재 실시간 적용값
    private float _targetRadius;
    private float _targetSpeed;

    void Update()
    {
        if (playerTransform == null) return;

        // 1. 플레이어 추적
        transform.position = Vector3.SmoothDamp(transform.position, playerTransform.position, ref _followVelocity, followSmoothTime);

        // 2. 갯수에 따른 능력치 실시간 계산
        CalculateDynamicStats();

        // 3. 회전 각도 업데이트 (계산된 _targetSpeed 사용)
        _currentRotationAngle += _targetSpeed * Time.deltaTime;
        if (_currentRotationAngle >= 360f) _currentRotationAngle -= 360f;

        // 4. 위치 업데이트
        UpdateOrbitalPositions();
    }

    private void CalculateDynamicStats()
    {
        int count = _activeOrbitals.Count;

        if (count <= 1)
        {
            _targetRadius = baseRadius;
            _targetSpeed = baseSpeed;
        }
        else
        {
            // 갯수가 많아질수록 반지름은 커지고 (Base + (N-1) * 증가량)
            _targetRadius = Mathf.Min(baseRadius + (count - 1) * radiusPerObject, maxRadius);

            // 갯수가 많아질수록 속도는 느려짐 (Base - (N-1) * 감소량)
            _targetSpeed = Mathf.Max(baseSpeed - (count - 1) * speedDecreasePerObject, minSpeed);
        }
    }

    private void UpdateOrbitalPositions()
    {
        int count = _activeOrbitals.Count;
        if (count == 0) return;

        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = _currentRotationAngle + (i * angleStep);
            float radians = angle * Mathf.Deg2Rad;

            // 실시간으로 계산된 _targetRadius 사용
            Vector3 offset = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * _targetRadius;
            
            // Lerp를 사용하면 갯수 변동 시 위치 이동이 더 부드러워집니다.
            _activeOrbitals[i].transform.localPosition = Vector3.Lerp(_activeOrbitals[i].transform.localPosition, offset, Time.deltaTime * 5f);
            
            _activeOrbitals[i].transform.LookAt(transform.position);
        }
    }

    public void AddOrbital(int typeIndex)
    {
        if (_activeOrbitals.Count > 10) return;
        if (typeIndex >= crystalPrefabs.Length) return;
        GameObject newOrbital = Instantiate(crystalPrefabs[typeIndex], transform);
        _activeOrbitals.Add(newOrbital);
    }

    public void RemoveOrbital()
    {
        if (_activeOrbitals.Count > 0)
        {
            GameObject target = _activeOrbitals[_activeOrbitals.Count - 1];
            _activeOrbitals.RemoveAt(_activeOrbitals.Count - 1);
            Destroy(target);
        }
    }
}