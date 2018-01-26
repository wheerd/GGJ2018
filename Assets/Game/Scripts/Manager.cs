using UnityEngine;
using de.deichkrieger.stateMachine;
using Zenject;
using System.Collections.Generic;

namespace de.deichkrieger.stateMachine
{
	public class Manager : MonoBehaviour
	{

		[Inject]
		private ChangeStateSignal _changeStateSignal;

		[Inject]
		private StartState _startState;

		[Inject]
		private FirstPersonState _firstPersonState;

		[Inject]
		private ClickToMoveState _clickToMoveState;

		[Inject]
		private PauseState _pauseState;

		private Stack<StateInterface> _currentStates = new Stack<StateInterface> ();

		void Start ()
		{
			_changeStateSignal += ChangeState;
		}

		void OnDestroy ()
		{
			_changeStateSignal -= ChangeState;
		}

		bool _isPaused = false;

		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (!_isPaused) {
					Debug.Log ("Pause game");
					Time.timeScale = 0;
					_isPaused = true;
				} else {
					Debug.Log ("Resume game");
					Time.timeScale = 1;
					_isPaused = false;
				}
			} else if (Input.GetKeyDown (KeyCode.S)) {
				_changeStateSignal.Fire (_startState, false);
			} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
				_changeStateSignal.Fire (_firstPersonState, false);
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				_changeStateSignal.Fire (_clickToMoveState, false);
			}
		}

		private void ChangeState (StateInterface _newState, bool additive)
		{
			if (!additive && _currentStates.Count != 0) {
				_currentStates.Pop ().Unload ();
			}

			_currentStates.Push (_newState);
			_currentStates.Peek ().Load ();
		}
	}
}