using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[Header("Player controller")]
	[SerializeField]
	private Transform _player;

	[Header("Variables")]
	[SerializeField]
	private float _distance;
	[SerializeField]
	private float _height;
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.LookAt(_player);
		this.transform.position = _player.position + _player.forward * -_distance + _player.up * _height;
	}
}
