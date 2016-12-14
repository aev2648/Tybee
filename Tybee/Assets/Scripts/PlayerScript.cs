using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour {
    

    public int MaxLives = 5;
    public int CurrentLives;
	public string inputedWord{ get; set; }
    private Text word;

    public KeyCode sendword;

    // Use this for initialization
    void Start () {
        
        word = GameObject.Find("Inputword").GetComponent<Text>() as Text;
        CurrentLives = MaxLives;
        inputedWord = "";
	}
	
	void Update()
	{
        
        //constantly checks if the player enters any key and updates the input field. and when the player presses enter, it parses player's input
        if (Input.anyKeyDown)
        {
            char c = Input.inputString.ToCharArray()[0];
            print("inputed char: " + c);
            if (c == "\b"[0])
            { 
                if (word.text.Length != 0)
                    word.text = word.text.Substring(0, word.text.Length - 1);
            }
            else
            {
                if (c == "\n"[0] || c == "\r"[0])
                {
                    inputedWord = word.text;
                    print("inputed word: " + inputedWord);
                    SendTextInput();
                    word.text = "";
                }
                else
                    word.text += c;
            }
        }
	}

    //when a bee hits the player, the player loses a life. when the player loses all 5 lives, player is killed.
    public void LoseLife()
    {
        CurrentLives--;
        print("Lives: " + CurrentLives);
        if(CurrentLives <= 0)
        {
            kill();
        }
    }
    
    
    /*public void GainLife()
    {
        CurrentLives++;
    }*/
    
    //destroys a gameObject
    public void kill()
    {
        Destroy(gameObject);
    }
    
    //uses an instance to communicate with manager's TryDestroyBee -- to see if the player's input is matched -- if so -- a matched bee is destroyed
    void SendTextInput()
    {
		Manager.instance.TryDestroyBee (inputedWord);
    }

    //when the bees hit player (bear), the player loses a life.
    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Respawn")
		{
            Manager.instance.GetComponent<Manager>().bees.Remove(other.gameObject);
			Destroy(other.gameObject);
			LoseLife();
			Debug.Log("Lives: " + CurrentLives);
		}
	}
}
