using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectRoom : MonoBehaviour {
	public GameObject Manager;
	public GameObject Explosions;

	public int playerClicks = 0;
	void OnTriggerEnter(Collider col)
	{if (playerClicks >= 10) {
		
			Instantiate (Explosions);
		
		
		}


	}
}
