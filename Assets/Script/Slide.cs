using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    //public Sprite originalSprite;
    public bool isSliding;
    private SpriteRenderer sp;
    //public Sprite spriteForSlide;
    private BoxCollider2D boxCollider2D;
    public bool isUnderBlockBool = false;
    [SerializeField] public LayerMask groundLayerMask;

    private Jump jump;

    private Move move;

    private GameManager gm;

    public bool slideCompleted;
    // Start is called before the first frame update
    void Start()
    {
        slideCompleted = false;
        isSliding = false;
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        jump = gameObject.GetComponent<Jump>();
        move = gameObject.GetComponent<Move>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.B) && !isSliding && !gm.isCurrentlyFighting()){
            if(! jump.isJumping && ! move.isSprinting){
                Debug.Log("lets start");
                slideCompleted = false;
                proceedSlide();
            }
        }

        if(isUnderBlockBool && isSliding){
            Debug.Log("lets continue");
            StartCoroutine(startSlide());
        }
        
        if(isSliding && !isUnderBlockBool && slideCompleted){
            Debug.Log("slideFinish");
            transform.eulerAngles = new Vector3(0,0,0);
            resetStats();
        }
    }

    void resetStats(){
        isSliding = false;
        slideCompleted = false;
        isUnderBlockBool = false;
    }

    void proceedSlide(){
        if(! isSliding){
            isSliding = true;
            transform.eulerAngles = new Vector3(0,0,90);
        }
            StartCoroutine(startSlide());
    }

    IEnumerator startSlide(){
        yield return new WaitForSeconds(1f);
        if(! slideCompleted){ slideCompleted = true; }
        isUnderBlock();
    }

    private void isUnderBlock(){
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.up, 2f, groundLayerMask);
        isUnderBlockBool = (raycast.collider != null);
    }
}
