using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMove : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.up;
    [SerializeField] float speed = 50;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }
}