using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;


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
    private int nextlvlpoints = 4500;

    private int level = 0;
    private int lvlscale = 2;

    private float minSpeed = 0.3f;
    private float maxSpeed = 0.3f;
    
    StreamReader levelOneWords;
    string lineOfText = null;
    
    void Awake()
    {
        if(instance!= null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        
        DontDestroyOnLoad(gameObject);
        
        gameState = GameState.start;
        
        Debug.Log("Compiling word database...");
        
        levelOneWords = new StreamReader("Assets/Words/level1words.txt");
        
        while ((lineOfText = levelOneWords.ReadLine()) != null)
            {
                string[] splitData = lineOfText.Split('|');
                
                for(int i = 0; i < splitData.Length; i++){
                
                    words.Add(splitData[i]);
                }
            }
        
        levelOneWords.Close();
        
    }
	// Use this for initialization
	void Start () {
    
    initializeModes();
    CreatePlayer();

    }
	// Update is called once per frame
	void Update () {
      
    }

	void GenerateBee(){
		
		string temp = words[Random.Range(0, words.Count)];

        
		GameObject Bee = GameObject.Instantiate(BeePF);
		PositionBee (Bee);
		Bee.GetComponent<BeeScript>().word = temp;
        Bee.GetComponent<BeeScript>().speed = Random.Range(minSpeed, maxSpeed);

        //needs comments!!

        bees.Add(Bee);

        print("Active Bees: " + bees.Count );
        
        
        //stops generation when player dies.
        if (player == null) {
            foreach (GameObject bee in bees)
            {
                bee.GetComponent<BeeScript>().speed = 0;
            }
            gameState = GameState.lost;
            initializeModes();
			CancelInvoke ("GenerateBee");
		}
		

	}

	void PositionBee(GameObject bee)
	{
        int side = Random.Range(1, 4) ;
        switch (side) {
			case 1:
			bee.transform.position = new Vector3(Random.Range(-12,12),-7.5f, 0);
				break;
			case 2:
			bee.transform.position = new Vector3(Random.Range(-12,12),7.5f, 0);
				break;
			case 3:
			bee.transform.position = new Vector3(11,Random.Range(-5,5), 0);
				break;
			case 4:
			bee.transform.position = new Vector3(-11,Random.Range(-5,5), 0);
				break;
		}
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
            AttemptLevelUp();
            return true;
        }

        return false;
    }
	 
    void CreatePlayer()
    {
	    player = GameObject.Instantiate(PlayerPF);
        player.transform.Translate(0, 0, 0);
    }
    
    void AttemptLevelUp()
    {
        if(points > nextlvlpoints)
        {
            nextlvlpoints = 2000 * lvlscale;
            lvlscale *= lvlscale;
            maxSpeed += 0.15f;
            if(level%2 == 0)
            {
                minSpeed += 0.1f;
            }
        }
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
        
        GUIStyle playingStyle = new GUIStyle ();
        GUIStyle menuButton = new GUIStyle();
        
        if (gameState == GameState.playing){
        
		GUI.backgroundColor = Color.clear;
		GUI.color = Color.black;
        playingStyle.fontSize = 50;
        GUI.Box (new Rect (50, 25, 0, 0), "Points: " + Mathf.RoundToInt(points).ToString (), playingStyle);
        
        if (GUI.Button(new Rect(800, 25, 800, 25), ("Menu"), playingStyle)){
            Debug.Log("Switching to start...");
            gameState = GameState.start;
            initializeModes();
            }
        
        }
        
        if (gameState == GameState.start){
            
        GUI.backgroundColor = Color.white;
		GUI.color = Color.black;
        menuButton.fontSize = 87;
        
        if (GUI.Button(new Rect(400, 500, 960, 1280), ("Click"), menuButton)){
            Debug.Log("Switching to playing...");
            gameState = GameState.playing;
            initializeModes();
            }
        }
	}
    
    void initializeModes(){
        
        Debug.Log("INIT: " + gameState);
        
        if (gameState == GameState.playing){
            
        Time.timeScale = 1;
		InvokeRepeating ("GenerateBee", 2,2);
        }
        
        if (gameState == GameState.start){
            
            Time.timeScale = 0;
            CancelInvoke ("GenerateBee");
            }
            
        }
    }
