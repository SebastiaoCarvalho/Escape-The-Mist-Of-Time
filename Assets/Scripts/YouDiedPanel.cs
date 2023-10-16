using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YouDiedPanel : MonoBehaviour
{
    public GameObject panelObject;
    public GameObject textObject;

    private Image panel;
    private TextMeshProUGUI text;
    private float timeToLerp = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        panelObject.SetActive(false);
        textObject.SetActive(false);
        panel = panelObject.GetComponent<Image>();
        text = textObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        panelObject.SetActive(true);
        textObject.SetActive(true);
        StartCoroutine(LerpPanelOpacity(0, 0.3921569f, timeToLerp));
        StartCoroutine(LerpTextSize(0, 36, timeToLerp));
    }

    public void Close()
    {
        StartCoroutine(LerpPanelOpacity(0.3921569f, 0, timeToLerp/4));
        StartCoroutine(LerpTextOpacity(1, 0, timeToLerp/4));
    }

    IEnumerator LerpPanelOpacity(float startValue, float endValue, float duration)
    {
        float time = 0;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, startValue);
        while (time < duration)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.Lerp(startValue, endValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, endValue);
        //panelObject.SetActive(false);
    }

    IEnumerator LerpTextOpacity(float startValue, float endValue, float duration)
    {
        float time = 0;
        text.color = new Color(text.color.r, text.color.g, text.color.b, startValue);
        while (time < duration)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(startValue, endValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        
        text.color = new Color(text.color.r, text.color.g, text.color.b, endValue);
        //panelObject.SetActive(false);
    }

    IEnumerator LerpTextSize(float startValue, float endValue, float duration)
    {
        float time = 0;
        text.fontSize = startValue;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (time < duration)
        {
            text.fontSize = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        text.fontSize = endValue;
        //textObject.SetActive(false);
    }
}
