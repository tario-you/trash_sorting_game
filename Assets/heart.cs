using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float xinit, yinit;
    private bool up = true;

    [SerializeField]
    private float ymovement, speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xinit = rb.position.x;
        yinit = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float y = rb.position.y;
        if (up && y > yinit + ymovement){
            up = false;
        }

        if (!up && y < yinit - ymovement){
            up = true;
        }

        UpdateMovement();
    }

    void UpdateMovement(){
        if (up) {
            rb.velocity = new Vector2 (0f, speed);
        }else{
            rb.velocity = new Vector2 (0f, -speed);
        }
    }
}
