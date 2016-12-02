using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    public static Manager instance;

    private Dictionary<string, List<GameObject>> bees;
    private List<string> words = new List<string>();

	public GameObject BeePF;
	public GameObject PlayerPF;

	public GameObject player;
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
		words.Add("berry");
		words.Add("kiwi");
		words.Add("banana");
		words.Add("plum");
		words.Add("peach");
        CreatePlayer();
		InvokeRepeating ("GenerateBee", 2,2);

    }
	
	// Update is called once per frame
	void Update () {
	}

	void GenerateBee(){
		bees = new Dictionary<string, List<GameObject>>();
		string temp = words[Random.Range(0, words.Count)];


		GameObject Bee = GameObject.Instantiate(BeePF);
		PositionBee (Bee);
		Bee.transform.Translate(Random.Range(-12,12),-8, 0);
		Bee.GetComponent<BeeScript>().word = temp;
		
		if(!bees.ContainsKey(temp))
		{
			bees.Add(temp, new List<GameObject>());
		}
		if (player == null) {
			CancelInvoke ("GenerateBee");
		}
		
		bees[temp].Add(Bee);
	}

	void PositionBee(GameObject bee)
	{
		int side = 2;
		switch (side) {
			case 1:
			bee.transform.position = new Vector3(Random.Range(-12,12),-8, 0);
				break;
			case 2:
			bee.transform.position = new Vector3(Random.Range(-12,12),0, 0);
				break;
			case 3:
			bee.transform.position = new Vector3(12,Random.Range(-5,5), 0);
				break;
			case 4:
			bee.transform.position = new Vector3(-12,Random.Range(-5,5), 0);
				break;
		}
	}

    bool checkword(string word, out List<GameObject> beeList)
    {
        if(bees.TryGetValue(word, out beeList))
        {
            if (beeList.Count != 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool TryDestroyBee(string word)
    {
		print (word);
        List<GameObject> temp;
        if (checkword(word, out temp))
        {
            GameObject tempBee = temp[0];
            temp.RemoveAt(0);
            points += tempBee.GetComponent<BeeScript>().KillBee();
            return true;
        }
        return false;
    }
	 
    void CreatePlayer()
    {
	    player = GameObject.Instantiate(PlayerPF);
        player.transform.Translate(0, 0, 0);
    }

	/*IEnumerable CheckEnemies()
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

*/
}