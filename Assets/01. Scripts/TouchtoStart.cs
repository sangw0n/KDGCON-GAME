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
    private CanvasGroup titleCanvasGroup; // �߰�: titleObj�� CanvasGroup�� ���� ����
    Sequence mySequence;

    private void Start()
    {
        // titleObj�� CanvasGroup ������Ʈ ��������
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
        // DOTween ������ ����
        mySequence = DOTween.Sequence()
        .SetAutoKill(false)
        .OnStart(() => {
            titleObj.transform.localScale = Vector3.zero;
            titleCanvasGroup.alpha = 0; // titleObj�� CanvasGroup alpha�� 0���� ����
        })
        .Append(titleObj.transform.DOScale(1, 1).SetEase(Ease.OutBounce))
        .Join(titleCanvasGroup.DOFade(1, 1)) // titleCanvasGroup�� alpha ���� 1�� �ִϸ��̼�
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
            // �ؽ�Ʈ�� ������ ��Ÿ�� �Ŀ� �����̴� ȿ�� ����
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
