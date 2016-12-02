using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    

    public int MaxLives = 5;
    private int CurrentLives;
    private string inputedWord;

    // Use this for initialization
    void Start () {
        CurrentLives = MaxLives;
        inputedWord = "";
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
        if
    }

}
