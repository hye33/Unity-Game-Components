using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubItem_Timer : MonoBehaviour
{
    private const float TIME = Define.DAY_TIME;
    private const float TICK = 1.0f;

    private TMP_Text timeText;
    private float currentTime;
    private bool isInit = false;

    public Action timeOut;

    private void Init()
    {
        timeText = GetComponentInChildren<TMP_Text>();
        timeText.text = TIME.ToString();
        currentTime = TIME;
        isInit = true;
    }

    private void Start()
    {
        Init();
        StartCoroutine(CoTimeCheck());
    }

    public void Restart()
    {
        if (!isInit)
            Init();
        StopAllCoroutines();
        timeText.text = TIME.ToString();
        currentTime = TIME;
        StartCoroutine(CoTimeCheck());
    }

    public void Pause() { StopAllCoroutines(); }
    public void Resume() { StartCoroutine(CoTimeCheck()); }

    private IEnumerator CoTimeCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(TICK);
            currentTime -= TICK;
            timeText.text = currentTime.ToString();
            if (currentTime <= 0)
            {
                if (timeOut != null)
                    timeOut.Invoke();
                break;
            }
        }
    }
}
