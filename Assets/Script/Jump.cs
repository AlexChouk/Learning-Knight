using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    public float jumpSpeed = 1.0f;
    
    public bool isJumping = false;
    private Slide slide;

    private GameManager gm;
    private Move move;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        slide = gameObject.GetComponent<Slide>();   
        move = gameObject.GetComponent<Move>(); 
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.J) && !isGrounded() && !gm.isCurrentlyFighting()){
            if(!slide.isSliding && !move.isSprinting){
                rb.velocity = Vector2.up * jumpSpeed;
            }
        }
        isJumping = isGrounded();
    }

    private bool isGrounded(){
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayerMask);
        return raycast.collider == null;
    }
}
