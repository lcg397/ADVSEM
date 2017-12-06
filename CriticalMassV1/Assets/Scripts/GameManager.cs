using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

   
    public float MassAvailable;
	public bool isTut; 
	public GameObject gun;
	public GameObject Explosions;
	public Text MatterText;
    public int NumberOfKeys;
    public List<GameObject> Keys;
    public int OBJNumber, OBJFinal;

    public float playerClicks = 10;

	/*void CheckClicks()
	{if (playerClicks >= 10f) {
			Instantiate (Explosions);
			StartCoroutine (ExplodeAfterTime (1));
		}
	}*/
    private void Update()
    {
	
        MassTooLow();
		setMatterText ();
        CheckOBJs();
    }
	public void Start()
	{
        
        Scene curScene = SceneManager.GetActiveScene();
		string sceneName = curScene.name;
		if (sceneName != "Tutorial1") {
			isTut = false;
		}
		if (sceneName == "Tutorial1") {
			isTut = true;
		}
	}
    void MassTooLow()
    {if(MassAvailable < 0)
        {
            MassAvailable = 0f;
        }
    }
	IEnumerator ExplodeAfterTime(float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene ("1");
	}
	void setMatterText()
	{if (isTut == false)
        {
			MatterText.text = "Matter Available: " + MassAvailable.ToString ();
		}
	}

    void CheckOBJs()
    {
        if(OBJNumber >= OBJFinal)
        {
            SceneManager.LoadScene("Menu");

        }


    }
}
