using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Button tutorialEndButton;
    private bool isTutorialEnded;

    private void Start()
    {
        if (tutorialPanel.activeSelf)
        {
            tutorialEndButton.onClick.AddListener(TutorialEnd);
            Time.timeScale = 0;
        }
    }

    private void TutorialEnd()
    {
        isTutorialEnded = true;
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
