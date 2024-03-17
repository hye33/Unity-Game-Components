using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// UI Slider

public class SubItem_TimeSlider : MonoBehaviour
{
    private const float TIME = Define.DAY_TIME;
    private const float TICK = 0.1f;

    private Slider slider;
    private bool isInit = false;
    public Action timeOut;

    private void Init()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = TIME;
        slider.value = TIME;
        isInit = true;
    }

    private void Start()
    {
        Init();
    }

    public void Restart()
    {
        if(!isInit)
            Init();
        StopAllCoroutines();
        slider.value = TIME;
        StartCoroutine(CoTimeCheck());
    }

    public void Pause() { StopAllCoroutines(); }
    public void Resume() { StartCoroutine(CoTimeCheck()); }

    private IEnumerator CoTimeCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(TICK);
            slider.value -= TICK;
            if (slider.value <= 0)
            {
                if (timeOut != null)
                    timeOut.Invoke();
                break;
            }
        }
    }
}