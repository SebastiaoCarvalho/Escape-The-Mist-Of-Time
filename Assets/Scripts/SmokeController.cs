using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SmokeController : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject Player;
    public float closedRadius = 10.0f;
    public float timeToLerp = 0.25f;

    public float smoothTime = 3.0f;
    private Vector3 velocity = Vector3.zero;
    private float openRadius;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        openRadius = GetComponent<ParticleSystem>().shape.radius;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.transform.position;
    }

    public void CloseSmoke()
    {
        StartCoroutine(LerpFunction(openRadius, closedRadius, true, timeToLerp));
    }

    public void OpenSmoke()
    {
        StartCoroutine(LerpFunction(closedRadius, openRadius, false, timeToLerp));
    }

    IEnumerator LerpFunction(float startValue, float endValue, bool smokeClosed, float duration)
    {
        float time = 0;
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var sh = ps.shape;
        while (time < duration)
        {
            sh.radius = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        sh.radius = endValue;
        gameManager.smokeClosed = smokeClosed;
    }
}
