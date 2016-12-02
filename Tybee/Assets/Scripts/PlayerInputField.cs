using UnityEngine;
using System.Collections;

public class PlayerInputField : MonoBehaviour {

	public GameObject player;

	void Start()
	{

	}

	public void charField(string input)
	{
		player.GetComponent<PlayerScript> ().inputedWord = input;
	}
}
