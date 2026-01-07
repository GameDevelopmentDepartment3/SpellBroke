using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.InputSystem;

public class HitEffect : MonoBehaviour
{
    public Volume globalVolume;

    Bloom bloom;
    Vignette vignette;
    DepthOfField dof;

    Coroutine hitCoroutine;

    void Awake()
    {
        globalVolume.profile.TryGet(out bloom);
        globalVolume.profile.TryGet(out vignette);
        globalVolume.profile.TryGet(out dof);
    }

    /// <summary>
    /// 외부에서 호출하는 피격 함수
    /// </summary>
    public void PlayHitEffect()
    {
        if (hitCoroutine != null)
            StopCoroutine(hitCoroutine);

        hitCoroutine = StartCoroutine(HitEffectCoroutine());
    }

    IEnumerator HitEffectCoroutine()
    {
        float duration = 0.1f;   // 켜지는 시간
        float holdTime = 0.2f;    // 유지 시간
        float time = 0f;

        // 목표 수치
        float targetBloom = 0f;
        float targetVignette = 0.3f;
        float targetBlur = 0.4f;

        // ON
        while (time < duration)
        {
            float t = time / duration;

            bloom.intensity.value = Mathf.Lerp(0f, targetBloom, t);
            vignette.intensity.value = Mathf.Lerp(0f, targetVignette, t);
            dof.gaussianMaxRadius.value = Mathf.Lerp(0f, targetBlur, t);

            time += Time.deltaTime;
            yield return null;
        }

        // 유지
        yield return new WaitForSeconds(holdTime);

        time = 0f;

        // OFF
        while (time < duration)
        {
            float t = time / duration;

            bloom.intensity.value = Mathf.Lerp(targetBloom, 0f, t);
            vignette.intensity.value = Mathf.Lerp(targetVignette, 0f, t);
            dof.gaussianMaxRadius.value = Mathf.Lerp(targetBlur, 0f, t);

            time += Time.deltaTime;
            yield return null;
        }

        // 안전 초기화
        bloom.intensity.value = 0f;
        vignette.intensity.value = 0f;
        dof.gaussianMaxRadius.value = 0f;
    }
}