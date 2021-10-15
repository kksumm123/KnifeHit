using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager intantce;
    public int point;
    public int applePoint;
    public int stage = 1;
    CanvasGroup whiteCanvasGroup;
    void Awake()
    {
        whiteCanvasGroup = Resources.Load<CanvasGroup>("WhiteCanvas");
        if (intantce != null)
        { // 만약 이미 글로벌매니저가 잇으면 부수기
            Destroy(gameObject);
            return;
        }
        intantce = this;
        point = 0;
        applePoint = 0;
        stage = 1;
    }

    void Start()
    {
        whiteCanvasGroup = Instantiate(whiteCanvasGroup);
        whiteCanvasGroup.alpha = 0;
        DontDestroyOnLoad(whiteCanvasGroup);
        DontDestroyOnLoad(transform.root);
    }

    internal void StageClear()
    {
        StartCoroutine(StageClearCo());
    }

    private IEnumerator StageClearCo()
    {
        stage++;
        whiteCanvasGroup.alpha = 1;
        var progress = SceneManager.LoadSceneAsync(0);
        
        while (progress.isDone == false)
            yield return null;

        DOTween.To(() => whiteCanvasGroup.alpha, x => whiteCanvasGroup.alpha = x, 0, 1f)
               .SetLink(gameObject);
    }
}
