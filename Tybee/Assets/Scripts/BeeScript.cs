using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class BeeScript : MonoBehaviour {
    public string word { get; set; }
    public float speed = 1;
    public Transform target;

	// Use this for initialization
	void Awake () {
        word = "default";
    }
    void Start()
    {
        //bee's words
        Text Bword = this.GetComponentInChildren<Text>() as Text;
        Bword.text = this.GetComponent<BeeScript>().word;
    }
	// Update is called once per frame
	void Update () {
        
        //bees' speed
        float maxDistanceDelta = 1;
        float step = speed * (Time.deltaTime * maxDistanceDelta);
        
        //bees' move to target
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
	}
    
    //destroys bees
    public void KillBee()
    {

        Destroy(gameObject);

    }
    
    //when a bee is killed, points are given.
    public int AwardPoints()
    {
		return word.Length * ((int)Mathf.Round(speed)+1) * 100;
    }
}