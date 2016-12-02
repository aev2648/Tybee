using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour {
    

    public int MaxLives = 5;
    private int CurrentLives;
	public string inputedWord{ get; set; }

	public KeyCode sendword;

    // Use this for initialization
    void Start () {
        CurrentLives = MaxLives;
        inputedWord = "";
	}
	
	void update()
	{
		print (inputedWord);
		if (Input.GetKeyDown(sendword)) {
			SendTextInput();
		}
	}

    public void LoseLife()
    {
        CurrentLives--;
        print(CurrentLives);
        if(CurrentLives <= 0)
        {
            kill();
        }
    }
    public void GainLife()
    {
        CurrentLives++;
        print(CurrentLives);
    }
    public void kill()
    {
        Destroy(gameObject);
    }
    
    void SendTextInput()
    {
		Manager.instance.TryDestroyBee (inputedWord);
		
    }
	void OnMouseDown()
	{
		LoseLife ();
	}
}
