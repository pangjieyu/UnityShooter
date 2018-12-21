using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilei : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Dead", 4);
    }

    void Dead()
    {
        CancelInvoke("Dead");
        Collider[] c = Physics.OverlapSphere(this.transform.position, 1000);
        Destroy(this.gameObject);
		GameObject fx = Instantiate(Resources.Load("52SpecialEffectPack/Effect/Explode8")) as GameObject;
        fx.transform.position = this.transform.position;
        Debug.Log("长度："+c.Length);
        for (int i=0; i<c.Length; ++i)
        {
            Debug.Log(c[i].transform.name);
            // Destroy(c[i].gameObject);
            Rigidbody targetRigidBody = c[i].GetComponent<Rigidbody> ();
            if (!targetRigidBody) continue;
            //targetRigidBody.AddExplosionForce(100, this.transform.position, 1000);
            float damage = 10 * (1-Vector3.Distance(c[i].transform.position, this.transform.position) 
            / 1000);
            Debug.Log(c[i].transform.name+": "+damage);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
