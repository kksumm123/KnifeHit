using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject knife;
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
        baseKnifeIcon = GameObject.Find("Canvas").transform.Find("KnifeCount/BaseIcon").GetComponent<Image>();
        usableKnifeIcon = Resources.Load<Sprite>("UsableKnife");
        usedKnifeIcon = Resources.Load<Sprite>("UsedKnife");

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

    void Update()
    {
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
