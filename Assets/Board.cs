using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    GameObject fixedKnife;
    Vector3 fixedKnifePos;
    void Start()
    {
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
