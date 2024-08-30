using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    public bool fadeInOnStart = true;
    public bool fadeOutOnExit = true;

    [HideInInspector] public CanvasGroup canvasGroup;

    public static FadeInOut instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (fadeInOnStart)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeIn());
        }
        else if (fadeOutOnExit)
        {
            canvasGroup.alpha = 1f;
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            if(canvasGroup.alpha == 0)  gameObject.SetActive(false);
            yield return null;
        }
    }
}