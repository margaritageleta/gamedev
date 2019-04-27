using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

	public enum Species { UNICORN, GIRAFFE };

	[Header("Species")]
	[SerializeField]
	private Species _species;

	protected enum Status { IDLE, FOLLOWING };

	[Header("Status")]
	[SerializeField]
	private Status _status;
	[SerializeField]
	private float _health;
	[SerializeField]
	private float _maxHealth;

    [Header("Spawn position")]
	[SerializeField]
	private Transform _initialTransform;

	[Header("Player follow")]
	private bool _followingPlayer;
	private Transform _playerTransform;

	UnityEngine.AI.NavMeshAgent nav;

	// Use this for initialization
	void Start () {
		_status = Status.IDLE;
		_initialTransform = this.transform;
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		Respawn();
	}
	
	// Update is called once per frame
	void Update () {
		// Follow player
		if (_followingPlayer && Vector3.Distance(this.transform.position, _playerTransform.position) > 5f)
		{
			nav.SetDestination(_playerTransform.position);
		}
	}

	public Species GetSpecies(){
		return _species;
	}

	public void FollowPlayer(PlayerController pc){
		_followingPlayer = true;
		_playerTransform = pc.transform;
	}

	public void GetHit(float damage)
    {
        // Deduct damage from health
        _health = Mathf.Max(_health - damage, 0f);
        //TODO: spawn particles
        if (_health.Equals(0f))
        {
            Die();
        }
    }

	private void Die(){
		_followingPlayer = false;
		// Spawn particles
		Respawn();
	}

	private void Respawn(){
		this.transform.position = _initialTransform.position;
		this.transform.rotation = _initialTransform.rotation;
        _health = _maxHealth;
		_status = Status.IDLE;
	}
}
