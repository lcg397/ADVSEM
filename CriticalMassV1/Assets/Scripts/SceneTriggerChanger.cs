using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerChanger : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{if (col.transform.gameObject.name == "Player")
        {

            SceneManager.LoadScene("Died");

        }

    else
        {

            Destroy(col.transform.gameObject);

        }
	}

	public void PlayGame1()
	{
		SceneManager.LoadScene ("1");

	}
    public void Instructions()
    {
        SceneManager.LoadScene("Ins");

    }
    public void Quit()
    {
        Application.Quit();

    }

    public void Death()
    {
        SceneManager.LoadScene("Death");

    }
}
