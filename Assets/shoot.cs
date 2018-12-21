using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {
	public Transform Bullet;
	public Transform firePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Fire:" );
		if (Input.GetKeyDown(KeyCode.Space))
		{
			
			Transform b =  Instantiate(Bullet,firePos.position,firePos.rotation);
			b.gameObject.GetComponent<Rigidbody>().AddForce(b.forward * 2000);
		}
	}
}
