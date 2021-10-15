using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject knife;
    void Start()
    {
        knife = (GameObject)Resources.Load("Knife");
        knife.GetComponent<Knife>().enabled = false;
        knife.SetActive(false);
        CreateKnife();
    }

    void Update()
    {
        if (Input.anyKeyDown && throwable)
        {
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

        yield return new WaitForSeconds(throwDelay);

        CreateKnife();
    }

}
