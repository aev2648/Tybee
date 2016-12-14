using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;


public class Manager : MonoBehaviour {
    
    
    public enum GameState {start, playing, lost}; //3 game modes
    public GameState gameState;
    
    public static Manager instance; //to communicate with other c# files
    
    public List<GameObject> bees = new List<GameObject>(); //list of bees
    private List<string> words = new List<string>(); //list of words

	public GameObject BeePF; //BEE PREFAB
	public GameObject PlayerPF; //BEAR PREFAB

	public GameObject player; //YOU

    private int points = 0;
    private int nextlvlpoints = 4500;

    private int level = 0;
    private int lvlscale = 2;

    //bees' speed
    private float minSpeed = 0.25f;
    private float maxSpeed = 0.25f;
    
    private int previousSideSpawn;
    
    StreamReader levelOneWords; //to get all words from .txt file
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
        
        Debug.Log("Compiling word database..."); //to see if StreamReader works
        
        levelOneWords = new StreamReader("Assets/Words/level1words.txt"); //gets all words from this .txt file
        
        while ((lineOfText = levelOneWords.ReadLine()) != null) //will keep getting datas of next words until theres nothing left
            {
                string[] splitData = lineOfText.Split('|'); //words in the .txt file is spilt -- with | so this is for making StreamReader to stop when it sees |, parses the word, and move on to the next word
                
                for(int i = 0; i < splitData.Length; i++){
                
                    words.Add(splitData[i]); //adds the parsed word to the word list
                }
            }
        
        levelOneWords.Close(); //closes StreamReader
        
    }
	// Use this for initialization
	void Start () {
    
    initializeModes(); //begins the enum control
    CreatePlayer(); //creates the player

    }
	// Update is called once per frame
	void Update () {
      
    }

    //makes Bees with words
	void GenerateBee(){
		
		string temp = words[Random.Range(0, words.Count)]; //randomly chooses a word from the word list

        
		GameObject Bee = GameObject.Instantiate(BeePF); //instantiate a bee
		PositionBee (Bee); //position the new bee
		Bee.GetComponent<BeeScript>().word = temp; //sets the bee's word
        Bee.GetComponent<BeeScript>().speed = Random.Range(minSpeed, maxSpeed); // randomly sets bee's speed between minSpeed and maxSpeed

        bees.Add(Bee); //adds the Bee in bees list

        print("Active Bees: " + bees.Count ); //to see if bees list is working
        
        
        //stops generation when player dies and switch game mode to lost
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

    //randomly positions the bees (top, right, left, and bottom)
	void PositionBee(GameObject bee)
	{
        int side = Random.Range(1, 4);
        while(side == previousSideSpawn)
        {
            side = Random.Range(1, 4);
        }
        switch (side) {
			case 1:
			bee.transform.position = new Vector3(Random.Range(-7,7),-6, 0);
				break;
			case 2:
			bee.transform.position = new Vector3(Random.Range(-7,7),6, 0);
				break;
			case 3:
			bee.transform.position = new Vector3(8,Random.Range(-5,5), 0);
				break;
			case 4:
			bee.transform.position = new Vector3(-8,Random.Range(-5,5), 0);
				break;
		}
        previousSideSpawn = side;
        
    }

    //to check if the input word matches a bee's word and if so, then remove bee from the list, destroy it, award points, and attempt to level up if achievable
    public bool TryDestroyBee(string word)
    {
        print("checking word: " + word); //when the player inputs their word
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
            print("Found Bee: " + bee); //if the bee is found
        }
        if (temp.Count > 0)//the true purpose of TryDestroyBee
        {
            bees.Remove(temp[0]);
            points += temp[0].GetComponent<BeeScript>().AwardPoints();
            temp[0].GetComponent<BeeScript>().KillBee();
            AttemptLevelUp();
            return true;
        }

        return false; //if the bee isnt found
    }
	 
    void CreatePlayer() //creates the player and position it
    {
	    player = GameObject.Instantiate(PlayerPF);
        player.transform.Translate(0, 0, 0);
    }
    
    void AttemptLevelUp() //when game levels up, bees get faster.
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
	
    //all GUIs
    public void OnGUI(){
        
        GUIStyle playingStyle = new GUIStyle ();
        GUIStyle menuButton = new GUIStyle();
        GUIStyle lostStyle = new GUIStyle();
        GUIStyle playStyle = new GUIStyle();
        GUIStyle ruleStyle = new GUIStyle();
        GUIStyle warningStyle = new GUIStyle();
        
        playingStyle.fontSize = 50;
        menuButton.fontSize = 87;
        playStyle.fontSize = 150;
        lostStyle.fontSize = 87;
        ruleStyle.fontSize = 36;
        warningStyle.fontSize = 24;
        
        GUI.color = Color.black;
        
        //GUIs in playing GameState
        if (gameState == GameState.playing){
            
        
            //Score GUI
        GUI.Box (new Rect (50, 25, 0,0), "Score: " + Mathf.RoundToInt(points).ToString (), playingStyle);
        
            //Menu Button GUI
        if (GUI.Button(new Rect(800, 25, 800,125), ("Menu"), playingStyle)){
            Debug.Log("Switching to start...");
            gameState = GameState.start;
            initializeModes();
            }
        
            //Lives GUI
        GUI.Box(new Rect(50, 80, 0, 0), "Lives: " + player.GetComponent<PlayerScript>().CurrentLives.ToString(), playingStyle);
        
        }
        
        //GUIs in start GameState
        if (gameState == GameState.start){
           
            //Instructions for the game GUI
        GUI.Box(new Rect(50, 150,0,0), "Instructions: Match a Bee's word by typing then press\nenter key to kill the bee with same word. If a Bee touches\nyou (bear), you lose a life. You have 5 lives when you\nstart playing. Good luck!!", ruleStyle);
            
        GUI.Box(new Rect(535, 350, 0,0), "WARNING: Buzzers (Game Creators)\nare aware that (1) bees can overlay each\nother and (2) when you pause the game\nby clicking 'Menu' while you are playing\ngame, bees are not hidden. These two\nproblems are part of few unsolved bugs.", warningStyle);
        
            //Game title GUI
        GUI.Box(new Rect((Screen.width/2) - 100, 25,0,0), "Tybee", menuButton);    
            
            //Play Button GUI
        if (GUI.Button(new Rect(350, 525, 350,625), ("Play"), playStyle)){
            Debug.Log("Switching to playing...");
            gameState = GameState.playing;
            initializeModes();
            } 
        }
        
        //GUIs in lost GameState
        if (gameState == GameState.lost){
            
            //Final Score GUI
            GUI.Box(new Rect(50, 20, 0, 0), "Score: " + points, lostStyle);
            
            //Restart Instructions
            GUI.Box(new Rect(125, (Screen.height/2) - 150, 0, 0), "YOU LOSE! To enact revenge on the Bees, click\n'PLAY AGAIN' again and again until you have\n                  killed all Bees to play again!", ruleStyle);
            
            //PLAY AGAIN Button GUI
            if (GUI.Button(new Rect((Screen.width/2) - 250, (Screen.height/2), (Screen.width/2) - 150, (Screen.height) + 100), ("PLAY AGAIN"), lostStyle)){


                foreach (GameObject bee in bees) //destroys a bee each time when PLAY AGAIN button is clicked until all bees are gone then it will go to start GameState
                {

                    Destroy(bee);
                    bees.Remove(bee);
                }
            
                if (player != null){ //kills the player if it is not killed yet (backup code)
                
                    player.GetComponent<PlayerScript>().kill(); 
                }
                
                //resets the game and switches to start mode
                points = 0;
                level = 0;
                lvlscale = 2;
                nextlvlpoints = 4500;
            
                Debug.Log("Switching to start...");
                gameState = GameState.start;
                CreatePlayer();
                initializeModes();
            }
            
            }
        }
    
    //controls game setting when game is in a specific enum
    void initializeModes(){
        
        Debug.Log("INIT: " + gameState);
        
        //if game is in playing mode
        if (gameState == GameState.playing){
            
        Time.timeScale = 1;
		InvokeRepeating ("GenerateBee", 2,2);
        }
        
        //if game is in start mode
        if (gameState == GameState.start){
            
            CancelInvoke ("GenerateBee");
            Time.timeScale = 0;
            
            }
        }
    }
