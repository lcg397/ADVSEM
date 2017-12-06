using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCube : MonoBehaviour {
	public GameObject TutCube;
	// Use this for initialization
	public void DetectButton()
	{

		if(this.gameObject.CompareTag("Up"))
		{

			growCube();

		}

			if(this.gameObject.CompareTag("Down"))
		{


			shrinkCube();

		}



	}
		public void growCube()
		{
		TutCube.gameObject.transform.localScale = Vector3.Lerp(TutCube.gameObject.transform.localScale, new Vector3 (2f, 2f, 2f), Time.deltaTime);
		Debug.Log ("GrowTuyt");

		}
		public void shrinkCube()
		{
		TutCube.gameObject.transform.localScale = Vector3.Lerp(TutCube.gameObject.transform.localScale, new Vector3 (.2f, .2f, .2f), Time.deltaTime);
			Debug.Log ("shrinkTuyt");

		}
}
