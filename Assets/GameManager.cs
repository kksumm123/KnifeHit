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
        knife.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
            CreateKnife();
    }

    void CreateKnife()
    {
        var newKnife = Instantiate(knife);
        newKnife.SetActive(true);    
    }
}
