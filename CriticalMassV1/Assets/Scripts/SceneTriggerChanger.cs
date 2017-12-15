using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerChanger : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{if (col.transform.gameObject.name == "Player")
        {

            SceneManager.LoadScene("Level2");

        }

    else
        {

            Destroy(col.transform.gameObject);

        }
	}

	public void PlayGame1()
	{
		SceneManager.LoadScene ("Level1");

	}
    public void Instructions()
    {
        SceneManager.LoadScene("Ins");

    }
    public void Instructions2()
    {
        SceneManager.LoadScene("Ins2");

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
