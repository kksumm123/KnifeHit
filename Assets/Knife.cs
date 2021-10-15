using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.up;
    [SerializeField] float speed = 50;
    void Start()
    {
        Destroy(gameObject, 2);
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }
}
