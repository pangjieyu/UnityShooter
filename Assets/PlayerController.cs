using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int speed = 2;
    public int rotateSpeed = 2;
  //  public GameObject weapon;
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = this.GetComponentInChildren<Animator>();
	}
	
 

	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(h, 0, v);
        moveDir = moveDir.normalized;//单位化向量   

        if(h !=0)
        {
            transform.Rotate(Vector3.up, h);
        }
        if (v !=0)
        {
            anim.SetFloat("speed", 2);
            transform.Translate(transform.forward * Time.deltaTime * speed * (v/Mathf.Abs(v)),Space.World);
        }
        
        if(h == 0 && v==0)
        {
            anim.SetFloat("speed", 0);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("shoot");
        }

	}
}
