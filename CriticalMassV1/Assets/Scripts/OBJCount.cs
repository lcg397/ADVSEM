using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJCount : MonoBehaviour {
    public GameObject GameManager;
    public GameObject OBJPosition;


    void OBJCountUp()
    {
        GameManager.GetComponent<GameManager>().OBJNumber += 1;
    }


    private void OnTriggerEnter(Collider col)
    {if(col.CompareTag("OBJ"))
        {

            col.gameObject.GetComponent<OBJrbIND>().inOBJ = true;
            OBJCountUp();
            col.gameObject.transform.parent = null;
            col.transform.position = OBJPosition.transform.position;
            col.GetComponent<Rigidbody>().isKinematic = true;
            col.transform.gameObject.layer = 1;
        }
    }
}
