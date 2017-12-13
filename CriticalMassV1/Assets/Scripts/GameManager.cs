using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

   
    public float MassAvailable;
	public bool isTut; 
	public GameObject gun;
	public GameObject Explosions;
	public Text MatterText;
    public int NumberOfKeys;
    public List<GameObject> Keys;
    public int OBJNumber, OBJFinal;
    public UnityEvent Win;

    public float playerClicks = 10;


    private void Update()
    {
	
        MassTooLow();
		setMatterText ();
        CheckOBJs();
    }
	public void Start()
	{
        
   
	}
    void MassTooLow()
    {if(MassAvailable < 0)
        {
            MassAvailable = 0f;
        }
    }
	IEnumerator WinAfterTime(float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene ("2");
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
            Win.Invoke();
            StartCoroutine(WinAfterTime(2f));
        }


    }
}
