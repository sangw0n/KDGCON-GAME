using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchtoStart : MonoBehaviour
{
    public LoopType loopType;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject titleObj;
    [SerializeField] string sceneName;
    private CanvasGroup titleCanvasGroup; // 추가: titleObj의 CanvasGroup을 위한 변수
    Sequence mySequence;

    private void Start()
    {
        // titleObj의 CanvasGroup 컴포넌트 가져오기
        titleCanvasGroup = titleObj.GetComponent<CanvasGroup>();

        ShowTitle();
        StartCoroutine(ShowText());
    }
    private void Update()
    {
        StartCoroutine(TaptoStart());

    }

    private void ShowTitle()
    {
        // DOTween 시퀀스 설정
        mySequence = DOTween.Sequence()
        .SetAutoKill(false)
        .OnStart(() => {
            titleObj.transform.localScale = Vector3.zero;
            titleCanvasGroup.alpha = 0; // titleObj의 CanvasGroup alpha를 0으로 설정
        })
        .Append(titleObj.transform.DOScale(1, 1).SetEase(Ease.OutBounce))
        .Join(titleCanvasGroup.DOFade(1, 1)) // titleCanvasGroup의 alpha 값을 1로 애니메이션
        .SetDelay(0.5f);
    }

    void OnEnable()
    {
        mySequence.Restart();
    }

    IEnumerator ShowText()
    {
        text.alpha = 0;

        yield return new WaitForSeconds(2);

        text.gameObject.SetActive(true);
        text.DOFade(1, 1).OnComplete(() =>
        {
            // 텍스트가 완전히 나타난 후에 깜빡이는 효과 시작
            text.DOFade(0, 1).SetLoops(-1, loopType);
        });
    }

    IEnumerator TaptoStart()
    {
        yield return new WaitForSeconds(2);

        if(Input.GetMouseButtonDown(0))
        {
            FadeInOut.instance.gameObject.SetActive(true);
            FadeInOut.instance.StartCoroutine(FadeInOut.instance.FadeIn());
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneName);
        }
    }
}
