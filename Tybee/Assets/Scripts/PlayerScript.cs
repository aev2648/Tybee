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
	
	void Update()
	{
        Text word = GameObject.Find("Word").GetComponent<Text>() as Text;
        inputedWord = word.text.ToString() ;
        print("word: " + word);
        if (Input.GetKeyDown(sendword)) {
            print("inputed word: " + inputedWord);
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

    
    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Respawn")
		{
			Destroy(other.gameObject);
			LoseLife();
			Debug.Log("Lives: " + CurrentLives);
		}
	}
}
