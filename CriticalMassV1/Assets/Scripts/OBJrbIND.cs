using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJrbIND : MonoBehaviour {
    Rigidbody rb;
    public bool inOBJ;

	// Use this for initialization
	void Start ()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckRB();
	}

    void CheckRB()
    {if(inOBJ == true)
        {
            rb.isKinematic = true;
            this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        


    }
}
