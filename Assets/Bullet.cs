using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void Start () {
        Invoke("Dead", 1);
    }

    void Dead(){
        CancelInvoke("Dead");
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag !="Player")
        {
            //Debug.Log("hit:"+collision.transform.name);
            GameObject fx = Instantiate(Resources.Load("52SpecialEffectPack/Effect/Explode8")) as GameObject;
            fx.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            // Destroy(fx);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
