using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("1");




        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();



        }
    }
}
