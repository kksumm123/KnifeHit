using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.up;
    [SerializeField] float speed = 25;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Knife"), LayerMask.NameToLayer("Knife"), true);
        isThrow = true;
        Destroy(gameObject, 2);
    }
    bool isThrow = false;
    void Update()
    {
        if (isThrow)
            transform.Translate(speed * Time.deltaTime * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Board"))
        {
            Debug.Log("Á¡¼ö È¹µæ");
            GameManager.instance.AddPoint();
        }
        else if (collision.collider.CompareTag("FixedKnife"))
        {
            isThrow = false;
            var rigid = transform.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.down * 300, ForceMode2D.Force);
            rigid.AddTorque(Random.Range(-180, 180), ForceMode2D.Force);
            Debug.Log("°ÔÀÓ ¿À¹ö");
        }
        else if (collision.collider.CompareTag("Apple"))
        {
            GameManager.instance.HitApple();
            Debug.Break();
        }
    }
}
