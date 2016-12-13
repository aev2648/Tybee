using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    
    public enum GameState {start, playing, lost, test};
    public GameState gameState;
    
    public static Manager instance;
    public List<GameObject> bees = new List<GameObject>();
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
        
        gameState = GameState.playing;
    }
	// Use this for initialization
	void Start () {
        
    initializeModes();    

    }
	// Update is called once per frame
	void Update () {
        
    }

	void GenerateBee(){
		
		string temp = words[Random.Range(0, words.Count)];

//Alances (my partner) and I had some argument about this and I let him to change my codes. I intended to make the gameobject as object of bee class. But with this code - im not exactly sure if each bee is in their own list but I don't think so because they get assigned with words just fine... lets make a debug log of printing the dictionary?
        
		GameObject Bee = GameObject.Instantiate(BeePF);
		PositionBee (Bee);
		Bee.GetComponent<BeeScript>().word = temp;
		
        //needs comments!!

        bees.Add(Bee);

        print("Active Bees: " + bees.Count );
        
        
        //stops generation when player dies.
        if (player == null) {
            gameState = GameState.lost;
            initializeModes();
			CancelInvoke ("GenerateBee");
		}
		

	}

	void PositionBee(GameObject bee)
	{
        int side = Random.Range(1, 4) ;
        bee.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0);
        /*switch (side) {
			case 1:
			bee.transform.position = new Vector3(Random.Range(-12,12),-8, 0);
				break;
			case 2:
			bee.transform.position = new Vector3(Random.Range(-12,12),8, 0);
				break;
			case 3:
			bee.transform.position = new Vector3(12,Random.Range(-5,5), 0);
				break;
			case 4:
			bee.transform.position = new Vector3(-12,Random.Range(-5,5), 0);
				break;
		}*/
    }

    public bool TryDestroyBee(string word)
    {
        print("checking word: " + word);
        List<GameObject> temp = new List<GameObject>();
        foreach (GameObject bee in bees)
        {
            if (bee.GetComponent<BeeScript>().word == word)
            {
                temp.Add(bee);
            }
        }
        foreach (GameObject bee in temp)
        {
            print("Found Bee: " + bee);
        }
        if (temp.Count > 0)
        {
            bees.Remove(temp[0]);
            points += temp[0].GetComponent<BeeScript>().AwardPoints();
            temp[0].GetComponent<BeeScript>().KillBee();
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
	}*/

    public void OnGUI(){
        
        GUIStyle pointsStyle = new GUIStyle ();
        GUIStyle menuButton = new GUIStyle();
        GUIStyle menuRules = new GUIStyle();
        
        if (gameState == GameState.playing){
        
		GUI.backgroundColor = Color.clear;
		GUI.color = Color.black;
        pointsStyle.fontSize = 87;
        GUI.Box (new Rect (270, 25, 0, 0), "Points: " + Mathf.RoundToInt(points).ToString (), pointsStyle);
        }
        
        if (gameState == GameState.start){
            
        GUI.backgroundColor = Color.white;
		GUI.color = Color.black;
        menuButton.fontSize = 87;
        
        if (GUI.Button(new Rect(900, 1280, 960, 1280), ("Click"), menuButton)){
            Debug.Log("Switching to playing...");
            gameState = GameState.playing;
            initializeModes();
            }
        }
	}
    
    void initializeModes(){
        
        Debug.Log("INIT: " + gameState);
        
        if (gameState == GameState.playing){
        
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
        
        if (gameState == GameState.start){
            
            }
            
        }
    }
