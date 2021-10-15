using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject knife;
    void Start()
    {
        knife = GameObject.Find("Knife");
        knife.GetComponent<KnifeMove>().enabled = false;
        knife.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            CreateKnife();
        }
    }

    GameObject newKnife;
    void CreateKnife()
    {
        newKnife = Instantiate(knife);
        newKnife.GetComponent<KnifeMove>().enabled = true;
        newKnife.SetActive(true);
    }
}
