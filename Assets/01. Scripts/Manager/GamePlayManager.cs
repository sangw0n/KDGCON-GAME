using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance { get; private set; }

    [SerializeField] GameObject clearPanel;

    public  List<Monster> monsterList = new List<Monster>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Monster[] monster = GetComponentsInChildren<Monster>();

        for(int i = 0; i < monster.Length; i++)
        {
            monsterList.Add(monster[i]);
        }
    }

    private void Update()
    {
        Camera.main.orthographicSize = 5; 
    }

    public void UpdateGameState()
    {
        if(monsterList.Count <= 0)
        {
            GameManager.Instance.ClearGame(true);
            clearPanel.SetActive(true);

            StartCoroutine(ClearCor());
        }
    }

    IEnumerator ClearCor()
    {
        yield return new WaitForSeconds(2);
        FadeInOut.instance.gameObject.SetActive(true);
        FadeInOut.instance.StartCoroutine(FadeInOut.instance.FadeIn());
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("02.Battle Base");
    }
}
