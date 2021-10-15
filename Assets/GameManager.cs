using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum GameState
{
    None,
    Play,
    GameOver,
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake() => instance = this;

    GameState gameState = GameState.None;
    public GameState GameState
    {
        get => gameState;
        set
        {
            if (gameState == value)
                return;
            gameState = value;
            switch (gameState)
            {
                case GameState.Play:
                    Time.timeScale = 1;
                    break;
                case GameState.GameOver:
                    reStartRealTime = Time.realtimeSinceStartup + 1;
                    Time.timeScale = 0;
                    break;
            }
        }
    }

    internal void AddPoint()
    {
        point++;
        pointText.text = point.ToString();
        if (point == totalKnifeCount)
        {
            Debug.LogWarning("스테이지 클리어");
            GameObject.Find("Board").GetComponent<RandomRotateZ>().StopAllCoroutines();
        }
    }

    internal void HitApple()
    {
        applePoint++;
        applePointText.text = applePoint.ToString();
    }

    GameObject knife;
    Text pointText;
    int point;
    Text applePointText;
    int applePoint;
    Image baseKnifeIcon;
    List<Image> knifeIcons = new List<Image>();
    Sprite usableKnifeIcon;
    Sprite usedKnifeIcon;
    [SerializeField] int totalKnifeCount = 8;
    [SerializeField] int usedKniftCount = 0;
    void Start()
    {
        knife = (GameObject)Resources.Load("Knife");
        knife.GetComponent<Knife>().enabled = false;
        knife.SetActive(false);
        var canvas = GameObject.Find("Canvas");
        pointText = canvas.transform.Find("PointText").GetComponent<Text>();
        applePointText = canvas.transform.Find("AppleScoreUI/Text").GetComponent<Text>();
        baseKnifeIcon = canvas.transform.Find("KnifeCount/BaseIcon").GetComponent<Image>();
        usableKnifeIcon = Resources.Load<Sprite>("UsableKnife");
        usedKnifeIcon = Resources.Load<Sprite>("UsedKnife");

        pointText.text = point.ToString();
        applePointText.text = applePoint.ToString();
        CreateKnife();
        InitKnifeIcons(totalKnifeCount);
    }

    void InitKnifeIcons(int totalKnifeCount)
    {
        baseKnifeIcon.gameObject.SetActive(true);

        for (int i = 0; i < totalKnifeCount; i++)
            knifeIcons.Add(Instantiate(baseKnifeIcon, baseKnifeIcon.transform.parent));

        baseKnifeIcon.gameObject.SetActive(false);

        knifeIcons.ForEach(x => x.sprite = usableKnifeIcon);
    }

    float reStartRealTime;
    void Update()
    {
        if (GameState == GameState.GameOver)
        {
            if (Input.anyKeyDown && Time.realtimeSinceStartup > reStartRealTime)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.anyKeyDown && throwable)
        {
            if (usedKniftCount < totalKnifeCount)
                StartCoroutine(ThrowNewKnife());
        }
    }

    GameObject newKnife;
    bool throwable = false;
    void CreateKnife()
    {
        newKnife = Instantiate(knife);
        newKnife.SetActive(true);
        throwable = true;
    }

    float throwDelay = 0.1f;
    IEnumerator ThrowNewKnife()
    {
        throwable = false;
        newKnife.GetComponent<Knife>().enabled = true;
        IncreaseUsedKnifeCount();

        yield return new WaitForSeconds(throwDelay);

        CreateKnife();
    }
    private void IncreaseUsedKnifeCount()
    {
        knifeIcons[usedKniftCount].sprite = usedKnifeIcon;
        usedKniftCount++;
    }
}
