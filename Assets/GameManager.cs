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
        Point++;
        pointText.text = Point.ToString();
        if (usedKniftCount == totalKnifeCount)
        {
            Debug.LogWarning("스테이지 클리어");
            GameObject.Find("Board").GetComponent<RandomRotateZ>().StopAllCoroutines();
            GlobalManager.intantce.StageClear();
        }
    }

    internal void HitApple()
    {
        ApplePoint++;
        applePointText.text = ApplePoint.ToString();
    }

    GameObject knife;
    Text pointText;
    int Point
    {
        get => GlobalManager.intantce.point;
        set => GlobalManager.intantce.point = value;
    }
    Text applePointText;
    int ApplePoint
    {
        get => GlobalManager.intantce.applePoint;
        set => GlobalManager.intantce.applePoint = value;
    }
    Text stageText;
    int Stage
    {
        get => GlobalManager.intantce.stage;
        set => GlobalManager.intantce.stage = value;
    }
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
        stageText = canvas.transform.Find("StageUI/Text").GetComponent<Text>();
        baseKnifeIcon = canvas.transform.Find("KnifeCount/BaseIcon").GetComponent<Image>();
        usableKnifeIcon = Resources.Load<Sprite>("UsableKnife");
        usedKnifeIcon = Resources.Load<Sprite>("UsedKnife");

        stageText.text = $"Stage {Stage}";
        pointText.text = Point.ToString();
        applePointText.text = ApplePoint.ToString();
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
