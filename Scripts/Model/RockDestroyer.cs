using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroyer : MonoBehaviour {
   
	[SerializeField]
	private Rock _lastRock;

	// Use this for initialization
	void Start () {
	}

	public void DestroyRocks(){
		if (_lastRock != null){
			_lastRock.Destroy();
		}
	}

	public void OnTriggerEnter(Collider other){
		if (other.tag.Equals("Rock"))
		{
			_lastRock = other.GetComponent<Rock>();
		}
	}

	public void OnTriggerExit(Collider other){
		if (other.tag.Equals("Rock"))
		{
            _lastRock = other.GetComponent<Rock>();
        }
	}
}
