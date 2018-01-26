﻿using UnityEngine;
using de.deichkrieger.stateMachine;
using Zenject;
using System.Collections.Generic;

namespace de.deichkrieger.stateMachine
{
	public class Manager : MonoBehaviour
	{
		[Inject] private StartState _startState;
		[Inject] private HighscoreState _highscoreState;
		[Inject] private CreditsState _creditsState;
		[Inject] private LevelState _levelState;
		[Inject] private LevelLostState _levelLostState;
		[Inject] private LevelWinState _levelWinState;
		[Inject] private LevelChoseState _levelChoseState;

		[Inject] private GameStartSignal _gameStartSignal;
		[Inject] private GameHighscoreSignal _gameHighscoreSignal;
		[Inject] private GameCreditsSignal _gameCreditsSignal;
		[Inject] private LevelStartSignal _levelStartSignal;
		[Inject] private LevelWinSignal _levelWinSignal;
		[Inject] private LevelLostSignal _levelLostSignal;
		[Inject] private LevelChoseSignal _levelChoseSignal;

		private Stack<StateInterface> _currentStates = new Stack<StateInterface> ();
		bool _isPaused = false;

		void Awake ()
		{
			_gameStartSignal += OnStartSignal;
			_gameHighscoreSignal += OnHighscoreSignal;
			_gameCreditsSignal += OnCreditsSignal;
			_levelStartSignal += OnLevelSignal;
			_levelLostSignal += OnLevelLostSignal;
			_levelWinSignal += OnLevelWinSignal;
			_levelChoseSignal += OnLevelChoseSignal;
		}

		void Start()
		{
			_gameStartSignal.Fire();
		}

		void OnDestroy ()
		{
			_gameStartSignal -= OnStartSignal;
			_gameHighscoreSignal -= OnHighscoreSignal;
			_gameCreditsSignal -= OnCreditsSignal;
			_levelStartSignal -= OnLevelSignal;
			_levelLostSignal -= OnLevelLostSignal;
			_levelWinSignal -= OnLevelWinSignal;
			_levelChoseSignal -= OnLevelChoseSignal;
		}

		public void OnLevelChoseSignal()
		{
			ChangeState(_levelChoseState, false);
		}
		
		public void OnLevelLostSignal()
		{
			ChangeState(_levelLostState, false);
		}
		
		public void OnLevelWinSignal()
		{
			ChangeState(_levelWinState, false);
		}
		
		public void OnStartSignal()
		{
			ChangeState(_startState, false);
		}
		
		public void OnLevelSignal()
		{
			ChangeState(_levelState, false);
		}

		public void OnCreditsSignal()
		{
			ChangeState(_creditsState, false);
		}

		public void OnHighscoreSignal()
		{
			ChangeState(_highscoreState, false);
		}

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
			} 
			else if (Input.GetKeyDown(KeyCode.Q))
			{
				ChangeState(_startState, false);
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