using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance { get; private set; }

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

    public void UpdateGameState()
    {
        if(monsterList.Count <= 0)
        {
            GameManager.Instance.ClearGame(true);
            ScenesManager.Instance.LoadScene("02. Battle Base");
        }
    }
}
