using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float horizontal;
    public float speed = (float) 5;
    public Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        print(horizontal);
    }

    void FixedUpdate()
    {
      
        print(speed * horizontal);
        rigidbody2D.velocity = new Vector2(speed * horizontal, rigidbody2D.velocity.y);
    }
}
