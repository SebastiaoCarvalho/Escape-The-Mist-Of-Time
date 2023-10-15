using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    public bool isDisplayed = false;

    public Camera mainCam;
    [SerializeField] private GameObject[] uiPanels;
    [SerializeField] private TextMeshProUGUI[] promptTexts;

    private int count = 0;

    private void Start()
    {
        foreach (var uiPanel in uiPanels)
        {
            uiPanel.SetActive(false);
        }
    }

    public bool CanDisplay() {
        return count < uiPanels.Length;
    }

    public void SetUp(string text)
    {
        if (count >= uiPanels.Length) return;
        promptTexts[count].text = text;
        uiPanels[count].SetActive(true);
        isDisplayed = true;
        count++;
    }

    public void Close()
    {
        foreach (var uiPanel in uiPanels)
        {
            uiPanel.SetActive(false);
        }
        count = 0;
        isDisplayed = false;
    }
}
