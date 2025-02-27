using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EndingDirection : MonoBehaviour
{
    public GameObject Text;
    public CanvasGroup blinkText;
    private bool isEnd = false;

    public void Start()
    {
        Invoke("StartEndingSequence", 3f);
    }

    private void StartEndingSequence()
    {
        StartCoroutine("EndingSequence");
    }

    protected IEnumerator EndingSequence()
    {
        var delay = Text.transform.DOLocalMoveY(this.transform.position.y + 480, 10f).SetEase(Ease.Linear);
        yield return delay.WaitForCompletion();
        isEnd = true;
    }

    private void Update()
    {
        if (isEnd)
        {
            blinkText.alpha = Mathf.PingPong(Time.time * 0.5f, 1);
        }
    }
}
