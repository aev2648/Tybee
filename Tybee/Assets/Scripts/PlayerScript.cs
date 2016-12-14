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


       /* if (Input.GetKeyDown(sendword)) {
            print("inputed word: " + inputedWord);
            SendTextInput();
            
        }*/
	}

    public void LoseLife()
    {
        CurrentLives--;
        print("Lives: " + CurrentLives);
        if(CurrentLives <= 0)
        {
            kill();
        }
    }
    public void GainLife()
    {
        CurrentLives++;
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
            Manager.instance.GetComponent<Manager>().bees.Remove(other.gameObject);
			Destroy(other.gameObject);
			LoseLife();
			Debug.Log("Lives: " + CurrentLives);
		}
	}
}
