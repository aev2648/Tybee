using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class BeeScript : MonoBehaviour {
    public string word { get; set; }
    public float speed = 0;

	// Use this for initialization
	void Awake () {
        word = "default";
    }
    void Start()
    {
        Text Bword = this.GetComponentInChildren<Text>() as Text;
        Bword.text = this.GetComponent<BeeScript>().word;
    }
	// Update is called once per frame
	void Update () {
        
	}
    public int KillBee()
    {
        int points = Awardpoints();
        destroy(gameObject);
        return points;
    }
    public int AwardPoints()
    {
        return word.Length * speed * 100;
    }
}
