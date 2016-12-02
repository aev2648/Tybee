using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    public static Manager instance;

    private Dictionary<string, List<Gameobject>> bees;
    private List<string> words = new List<string>();
<<<<<<< HEAD
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

=======

    public GameObject BeePF;
    public GameObject PlayerPF;


 
>>>>>>> 13c8eab72ebc54f878875de3c6182630908d587b
	// Use this for initialization
	void Start () {
        words.Add("pear");
        words.Add("apple");
        words.Add("grape");
        CreatePlayer();
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
<<<<<<< HEAD
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
=======
        
    }
    
    void CreatePlayer()
>>>>>>> 13c8eab72ebc54f878875de3c6182630908d587b
    {
        GameObject player = GameObject.Instantiate(PlayerPF);
        player.transform.Translate(0, 0, 0);
    }

	IEnumerable CheckEnemies()
    {

		PrintClosestEnemies();
        yield return null;

    }

	// The script starts by calling this:
	// Just print the name of all enemies,
	// sorted as closest to furthest.


	public void PrintClosestEnemies () 
	{

		bees.Sort(ByDistance);

		// Show the results;
		// list[0] will be the closest,
		// list[1] will be the second closest,
		// list[2] will be the third closest,
		// ... and so on.    
		foreach (GameObject Bees in bees){
			print(Bees);
		}
	}

	public int ByDistance(GameObject a, GameObject b)
	{   
		var dstToA = Vector3.Distance(transform.position, a.transform.position);
		var dstToB = Vector3.Distance(transform.position, b.transform.position);
		return dstToA.CompareTo(dstToB);
	}
}
//Instantiate(gameObject, Vector3(Random.Range(minY, maxY), Random.Range(minZ,maxZ), Random.Range(minX,maxX)), Quaternion.identity)