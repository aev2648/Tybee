using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    public static Manager instance;

    private Dictionary<string, List<Gameobject>> bees;
    private List<string> words = new List<string>();
    public GameObject BeePF;
    //public KeyCode checkword;

    private int points = 0;
     

    void Awake()
    {
        if(instance!= null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

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
        bees = new Dictionary<string, List<Gameobject>>();
        for (int i = -4; i <= 4; i+= 4)
        {
            string temp = words[Random.Range(0, words.Count)];
            GameObject Bee = GameObject.Instantiate(BeePF);
            Bee.transform.Translate(i, -3, 0);
            Bee.GetComponent<BeeScript>().word = temp;
            if(!bees.TryGetValue(temp))
            {
                bees.Add(temp, new List<Gameobject>());
            }
            bees[temp].Add(Bee);
        }   
    }
    //
    bool checkword(string word, out List<Gameobject> beeList)
    {
        List<GameObject> temp;
        if(bees.TryGetValue(word, temp))
        {
            beelist = temp;
            if (temp.Count != 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool TryDestroyBee(string word)
    {
        List<GameObject> temp;
        if (checkword(word, temp))
        {
            GameObject tempBee = temp[0];
            temp.RemoveAt(0);
            points += tempBee.GetComponent<BeeScript>().KillBee();
            return true;
        }
        return false;
    }

    /*IEnumerable CheckEnemies()
    {

    }*/
}
