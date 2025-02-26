using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneDirection : MonoBehaviour
{
    public Canvas titleTextCanvas;
    private CanvasGroup titleTextCanvasGroup;
    public Canvas buttonCanvas;
    private CanvasGroup ButtonCanvasGroup;

    private void Start()
    {
        titleTextCanvasGroup = titleTextCanvas.GetComponent<CanvasGroup>();
        ButtonCanvasGroup = buttonCanvas.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        DisplayTitleText();
        if (titleTextCanvasGroup.alpha >= 0.6)
            DisplayButtons();
    }
    private void DisplayTitleText()
    {
        if (titleTextCanvasGroup.alpha < 1)
            titleTextCanvasGroup.alpha += Time.deltaTime * 0.3f;
        else
            titleTextCanvasGroup.alpha = 1;
    }

    private void DisplayButtons()
    {
        if (ButtonCanvasGroup.alpha < 1)
            ButtonCanvasGroup.alpha += Time.deltaTime * 0.5f;
        else
            ButtonCanvasGroup.alpha = 1;
    }

}
