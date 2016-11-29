using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    private List<GameObject> bees;
    private List<string> words = new List<string>();
    //public KeyCode checkword;


    public GameObject BeePF;
 
	// Use this for initialization
	void Start () {
        words.Add("pear");
        words.Add("apple");
        words.Add("grape");
        GenerateBees();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void GenerateBees()
    {
        bees = new List<GameObject>();
        for (int i = -4; i <= 4; i+= 4)
        {
            string temp = words[Random.Range(0, words.Count)];
            GameObject Bee = GameObject.Instantiate(BeePF);
            Bee.transform.Translate(i, -3, 0);
            Bee.GetComponent<BeeScript>().word = temp;
        }
        
        
        
    }
    /*IEnumerable CheckEnemies()
    {

    }*/
}
