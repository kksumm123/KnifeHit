using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    GameObject appleGo;
    [SerializeField] float appleRatio = 0.3f;
    [SerializeField] int appleMaxCount = 2;
    GameObject fixedKnife;
    Vector3 fixedKnifePos;
    void Start()
    {
        appleGo = (GameObject)Resources.Load("AppleParent");
        for (int i = 0; i < appleMaxCount; i++)
        {
            if (Random.Range(0, 1f) < appleRatio)
            {
                var newAppleGo = Instantiate(appleGo, transform);
                newAppleGo.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
                newAppleGo.SetActive(true);
            }
        }

        fixedKnife = transform.Find("FixedKnife").gameObject;
        fixedKnifePos = fixedKnife.transform.position;
        fixedKnife.SetActive(false);


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Knife"))
        {
            collision.gameObject.SetActive(false);

            var newFixedKnife = Instantiate(fixedKnife, fixedKnifePos, Quaternion.identity, transform);
            newFixedKnife.SetActive(true);
        }
    }
}
