using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sprite Slider
// 산하에 Fill 컴포넌트 구현
// Fill 이미지 원본 Pivot 위치 조정 필요
// 기본값: 세로

public class TimeBar : MonoBehaviour
{
    private const float TIME = Define.CUSTOMER_WAIT_TIME;
    private const float TICK = 0.1f;
    private Vector3 initScale = new Vector3(1.0f, 1.0f, 1.0f);

    private float delta;
    private float currentScale;
    private Transform fill;
    public Action timeOut;

    private void Start()
    {
        delta = TICK / TIME;
        fill = transform.GetChild(0);
        currentScale = initScale.y;
        fill.localScale = initScale;
        StartCoroutine(CoTimeCheck());
    }

    public void Reset()
    {
        fill.localScale = initScale;
        StartCoroutine(CoTimeCheck());
    }

    public void Pause() { StopAllCoroutines(); }
    public void Resume() { StartCoroutine(CoTimeCheck()); }

    private IEnumerator CoTimeCheck()
    {
        while (true)
        {
            currentScale -= delta;
            fill.localScale = new Vector3(1, currentScale, 1);
            if (currentScale <= 0)
            {
                if (timeOut != null)
                    timeOut.Invoke();
                break;
            }
            yield return new WaitForSeconds(TICK);
        }
    }
}