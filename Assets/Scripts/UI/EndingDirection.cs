using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingDirection : MonoBehaviour
{
    public GameObject endingTexts;
    public CanvasGroup canvasGroup;
    private bool isEnding = false;
    private bool isReach1 = false;

    private void Start()
    {
        Invoke("StartEndingSequence", 3f); // 0.5초 후 코루틴 시작
    }

    private void StartEndingSequence()
    {
        Debug.Log("엔딩시작");
        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        var value = endingTexts.transform.DOLocalMoveY(this.transform.position.y + 480, 10f).SetEase(Ease.Linear);
        yield return value.WaitForCompletion(); // 트윈 완료까지 대기
        isEnding = true; // 완료 후 true 설정
    }

    private void Update()
    {
        if (isEnding)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("게임종료");
                Application.Quit();
            }
            canvasGroup.alpha = Mathf.PingPong(Time.time * 0.5f, 1);
        }
    }
}
