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
        newKnife.GetComponent<Knife>().enabled = true;
        newKnife.SetActive(true);
    }
}
