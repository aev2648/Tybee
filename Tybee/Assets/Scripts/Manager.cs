using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public GameObject BeePF;

	// Use this for initialization
	void Start () {
        GameObject Bee = GameObject.Instantiate(BeePF);
        Text Bword = GameObject.Find("Bee").GetComponent<Text>() as Text;
        Bword.text = Bee.GetComponent<BeeScript>().word;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
