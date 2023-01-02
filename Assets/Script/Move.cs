using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float speed = 0.2f;
    public float currCountdownValue;
    public bool isSprinting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        
        if(! isSprinting){
            speed = 0.2f;
        }
        
        Debug.Log("out");
        if (Input.GetButtonDown("Fire1") )
        {
            isSprinting = true;
            StartCoroutine(StartSprint());
        }


    }

    public IEnumerator StartSprint() {
            speed = 0.5f;
            yield return new WaitForSeconds(1f);
            currCountdownValue--;
            isSprinting = false;
    }
}
