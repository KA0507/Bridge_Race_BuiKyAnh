using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFall : Brick
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider boxCollider;
    private void Awake()
    {
        SetBrickColor((int)ColorType.Gray);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra xem brick chạm vào vật thể có tag là "Ground" hay không
        if (collision.gameObject.CompareTag(Constant.TAG_GROUND))
        {
            Destroy(rb);
            boxCollider.isTrigger = true;
        }
    }
}
