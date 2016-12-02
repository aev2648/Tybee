using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class BeeScript : MonoBehaviour {
    public string word { get; set; }
    public float speed = 0;
    public Transform target;

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
        
        float maxDistanceDelta = 1;
        float step = speed * (Time.deltaTime * maxDistanceDelta);
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
	}
<<<<<<< HEAD
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
=======
>>>>>>> 13c8eab72ebc54f878875de3c6182630908d587b
