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
    }
    public void GainLife()
    {
        CurrentLives++;
    }
    
    void SendTextInput()
    {
        if
    }

}
