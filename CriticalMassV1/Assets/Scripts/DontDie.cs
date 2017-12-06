using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DontDestroyOnLoad (this.gameObject.transform);
	}
}
