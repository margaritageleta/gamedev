using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
	//TODO: Use PlayerController script
	[Header("Player controller")]
	[SerializeField]
	private PlayerController _player;
    
    [Header("Debug variables")]
	[SerializeField]
	//Cursor position in X direction (-1..1)
	private float _xAngle;
	[SerializeField]
	// Cursor position in Y direction (-1..1)
	private float _yAngle;

	[SerializeField]
    // True when the user is pressing on the screen
	private bool _isTouching;

	enum GameState { MENU, PLAYING };
	private GameState _state;

    /// <summary>
    /// Init the instance
    /// </summary>
	void Start()
	{
		_state = GameState.PLAYING;
		_isTouching = false;
	}

    /// <summary>
    /// Update instance
    /// </summary>
	void Update()
	{
		switch(_state){
			case GameState.PLAYING:
				UpdatePlaying();
				break;
			case GameState.MENU:
				UpdateMenu();
				break;
		}

	}

    /// <summary>
    /// Update while the user is playing
    /// </summary>
	private void UpdatePlaying(){
		
        // (0,0) bottom left (screenw, screenh) top right
		Vector2 cursorPos;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _isTouching = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isTouching = false;
            // Reset cursor position
			UpdateCursorPosition(new Vector2(Screen.width/2f,Screen.height/2f));
        }
		cursorPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#else
        Touch touch = Input.GetTouch(0);
        if (touch.phase.Equals(TouchPhase.Began))
        {
            _isTouching = true;
        }
        else if (touch.phase.Equals(TouchPhase.Canceled) || touch.phase.Equals(TouchPhase.Ended))
        {
            _isTouching = false;
            // Reset cursor position
		    UpdateCursorPosition(new Vector2(Screen.width/2f,Screen.height/2f));
        }
		cursorPos = touch.position;
#endif
        // User is pressing on the screen, update player movement
        if (_isTouching)
        {
			UpdateCursorPosition(cursorPos);
        }

		_player.SetCursorPosition(_xAngle, _yAngle);
	}

    /// <summary>
    /// Update while on a menu
    /// </summary>
	private void UpdateMenu(){
		
	}

    /// <summary>
    /// Translate the mouse position to -1..1 in both axes
    /// </summary>
    /// <param name="cursorPos">Cursor position</param>
	private void UpdateCursorPosition(Vector2 cursorPos){
		switch (_state)
        {
            case GameState.PLAYING:

                // Get at what percentage of the screen the cursor is, clamped (0..1)
				float screenXPercent = Mathf.Clamp(cursorPos.x / (float)Screen.width, 0f, 1f);
				float screenYPercent = Mathf.Clamp(cursorPos.y / (float)Screen.height, 0f, 1f);

                // Transform to (-1..1) in both axis
                _xAngle = screenXPercent * 2 - 1;
                _yAngle = screenYPercent * 2 - 1;
            
                break;
        }
	}
}
