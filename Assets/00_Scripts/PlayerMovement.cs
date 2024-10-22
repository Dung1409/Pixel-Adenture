using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigi;
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] Collider2D coll;
    State currentState;
    [SerializeField] private LayerMask Ground;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentState = State.IDLE;
        coll = GetComponent<Collider2D>();  
    }

    private void Move()
    {
        float HorizontalX = Input.GetAxisRaw("Horizontal");
        if (HorizontalX < -0.01f) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(HorizontalX > 0.01f)
        {
            transform.localScale = new Vector3(1, 1 , 1);
        }
        rigi.velocity = new Vector2(HorizontalX * speed , rigi.velocity.y);
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(Ground))
        {
            rigi.velocity = new Vector3(rigi.velocity.x, 10f);
            currentState = State.JUMP;
        }    
    }
    
    private void FixedUpdate()
    {
        if(currentState  !=  State.HURT)
        {
            Move();
            Jump();
            updateState();
        }
        anim.SetInteger("State", (int)currentState);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("Enermy") )
        {
            if(currentState == State.FALL)
                other.gameObject.SetActive(false);
            else
            {
                
                currentState = State.HURT;
                //force
                float force = transform.position.x > other.transform.position.x ? 5f : -5f;
                rigi.velocity = new Vector2(force , rigi.velocity.y);
                GameManager.instance.getDamage(10);
                StartCoroutine(WaitHurt());
                
            }
        }

        if (other.gameObject.CompareTag("Item"))
        {
            GameManager.instance.UpgradeScore();
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            GameManager.instance.UpgradeScore();
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            currentState = State.HURT;
           StartCoroutine(WaitHurt());  
        }
    }
    private enum State
    {
        IDLE,
        RUN,
        JUMP,
        FALL,
        HURT
    }

    private void updateState()
    {
        if(currentState == State.JUMP)
        {
            if(rigi.velocity.y <= 0f)
            {
               currentState = State.FALL;
            }
        }

        else if(currentState == State.FALL)
        {
            if (coll.IsTouchingLayers(Ground))
            {
                currentState = State.IDLE;
            } 
        }

        else if(Mathf.Abs(rigi.velocity.x) > 2f)
        {
            currentState = State.RUN;
        }
        else
        {
            currentState = State.IDLE;
        }

    }

    private IEnumerator WaitHurt()
    {
        yield return new WaitForSeconds(0.5f);
        currentState = State.IDLE;
    }
}
