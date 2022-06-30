using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 5;
    private Rigidbody2D mRig;

    void Start()
    {
        mRig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mRig.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, mRig.velocity.y);
    }
}
