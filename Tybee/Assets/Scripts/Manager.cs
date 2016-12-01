using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    private List<GameObject> bees;
    private List<string> words = new List<string>();

    public GameObject BeePF;
    public GameObject PlayerPF;


 
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
        bees = new List<GameObject>();
        for (int i = -4; i <= 4; i+= 4)
        {
            string temp = words[Random.Range(0, words.Count)];
            GameObject Bee = GameObject.Instantiate(BeePF);
            Bee.transform.Translate(i, -3, 0);
            Bee.GetComponent<BeeScript>().word = temp;
        }
        
    }
    
    void CreatePlayer()
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