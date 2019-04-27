using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

	[Header("Timers")]
	[SerializeField]
	private float _currentTime;
	[SerializeField]
	private float _destroyDelay;
	[SerializeField]
	private bool _isDestroyed;

	private Collider _collider;

	// Use this for initialization
	void Start () {
		_currentTime = 0f;
		_isDestroyed = false;
		_collider = this.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_isDestroyed){
			_currentTime += Time.deltaTime;

			if (_currentTime >= _destroyDelay){
				this.enabled = false;
				Object.Destroy(this.gameObject);
			}
		}
	}

	public void Destroy(){
		_collider.enabled = false;
		_isDestroyed = true;
	}
}
