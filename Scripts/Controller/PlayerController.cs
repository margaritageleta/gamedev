using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Player attributes")]
	[SerializeField]
	private float _health;
    [SerializeField]
    private float _maxHealth;
	[SerializeField]
	private float _healPerUnicorn;

	[SerializeField]
	private Move_leg[] _legs;

    [SerializeField]
    private float _currentMoveSpeed;
	[SerializeField]
	private float _maxMoveSpeed;
    [SerializeField]
    private float _moveAcceleration;
    [SerializeField]
    private float _currentRotationSpeed;
	[SerializeField]
	private float _maxRotationSpeed;
    [SerializeField]
    private float _rotationAcceleration;

	[Header("Abilities")]
	[SerializeField]
	private float _abilityTimer;
	[SerializeField]
	private float _abilityDelay;
	[SerializeField]
	private float _deathTimer;
	[SerializeField]
	private float _deathDelay;
    [SerializeField]
    private RockDestroyer _rd;
    
    
    public enum Ability { BREAK_ROCK, HEAL };

	[Header("Followers")]
	[SerializeField]
	private Object[] _followers;
	[SerializeField]
	private BitArray _occupiedFollowers;
	[SerializeField]
	private int _unicornCount;
	[SerializeField]
	private int _giraffeCount;
   
	// Use this for initialization
	void Start () {
		// Attributes
        _currentMoveSpeed = 0f;
        _currentRotationSpeed = 0f;

        // Timers
		_abilityTimer = 0f;
		_deathTimer = 0f;

		//Abilities
		_rd = this.GetComponentInChildren<RockDestroyer>();

        // Followers
		_followers = new Object[5];
		_occupiedFollowers = new BitArray(5);
		_occupiedFollowers.SetAll(false);

		_unicornCount = 3;
		_giraffeCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = Time.deltaTime;

		if (_health.Equals(0f)){
			_deathTimer = Mathf.Min(_deathTimer + deltaTime, _deathDelay);
			return;
		}

		_abilityTimer = Mathf.Max(_abilityTimer - deltaTime, 0f);
		UpdateMovement(deltaTime);
		UpdateRotation(deltaTime);

		if (!Input.GetAxis("Jump").Equals(0f)){
			UseAbility(Ability.BREAK_ROCK);
			//UseAbility(Ability.HEAL);
		}

	}

	protected void UpdateMovement(float deltaTime){
		Vector3 movement = this.transform.forward * _currentMoveSpeed * deltaTime;
		for (int i = 0; i < _legs.Length; i++){
			_legs[i].slerpCoefficient = movement.magnitude;
		}
		this.transform.position += movement;
	}

	protected void UpdateRotation(float deltaTime){
		Vector3 rotation = this.transform.eulerAngles;

		rotation = new Vector3(rotation.x, rotation.y + _currentRotationSpeed * Time.deltaTime, rotation.z);
		this.transform.rotation = Quaternion.Euler(rotation);
	}

	public void SetCursorPosition(float x, float y){
		
		_currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, _maxRotationSpeed * x, Time.deltaTime * _rotationAcceleration);
		if (Mathf.Abs(_currentRotationSpeed) < 0.01f)
        {
			_currentRotationSpeed = 0f;
        }

		_currentMoveSpeed = Mathf.Lerp(_currentMoveSpeed, _maxMoveSpeed * y, Time.deltaTime * _moveAcceleration);
		if (Mathf.Abs(_currentMoveSpeed) < 0.01f)
        {
			_currentMoveSpeed = 0f;
        }
	}

	public void UseAbility(Ability ability){
		
		if (_abilityTimer.Equals(0f)){
			_abilityTimer = _abilityDelay;
			switch(ability){
				case Ability.BREAK_ROCK:
					_rd.DestroyRocks();
					break;
				case Ability.HEAL:
					_health += Mathf.Min(_unicornCount * _healPerUnicorn, _maxHealth);
					break;
			}
		}
	}

	public void GetHit(float damage){
		// Deduct damage from health
		_health = Mathf.Max(_health - damage, 0f);
		//TODO: spawn particles
		if (_health.Equals(0f)){
			Die();
		}
	}

	private void Die(){

        //spawn particles
	}

	public void AddFollower(Animal newFollower){
		for (int i = 0; i < _occupiedFollowers.Length; i++){
			if (!_occupiedFollowers.Get(i)){
				_followers[i] = newFollower;
				_occupiedFollowers.Set(i, true);
                
				switch(newFollower.GetSpecies()){
					case Animal.Species.UNICORN:
						_unicornCount++;
						break;
					case Animal.Species.GIRAFFE:
						_giraffeCount++;
						break;
				}
				break;
			}
		}
	}
    
	public void LoseFollower(Animal follower){
		for (int i = 0; i < _occupiedFollowers.Length; i++){
			if (_occupiedFollowers.Get(i) && _followers[i].GetInstanceID().Equals(follower.GetInstanceID())){
				_followers[i] = null;
				_occupiedFollowers.Set(i, false);

				switch (follower.GetSpecies())
                {
                    case Animal.Species.UNICORN:
                        _unicornCount++;
                        break;
                    case Animal.Species.GIRAFFE:
                        _giraffeCount++;
                        break;
				}
                break;
			}
		}
	}

	public int GetGiraffeCount(){
		return _giraffeCount;
	}
}
