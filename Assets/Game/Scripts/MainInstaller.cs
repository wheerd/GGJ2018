using UnityEngine;
using Zenject;
using de.deichkrieger.stateMachine;

public class ExampleSignal : Signal<ExampleSignal, string>
{
}

public class TestModel
{
	public void Test (string testString)
	{
		Debug.Log ("Test called and said: " + testString);
	}
}

public class GameModel : IInitializable
{
	readonly ExampleSignal _exampleSignal;

	public GameModel (ExampleSignal exampleSignal)
	{
		_exampleSignal = exampleSignal;
	}

	public void Initialize ()
	{
		_exampleSignal.Fire ("Test");
	}
}

public class MainInstaller : MonoInstaller
{
	public override void InstallBindings ()
	{
		DeclareSignals();
		BindSignals();

		Container.BindInterfacesTo<GameModel> ().AsSingle ().NonLazy ();

		InstallStates();
	}

	private void BindSignals()
	{
		Container.BindSignal<string, ExampleSignal> ()
			.To<TestModel> (x => x.Test).AsSingle ().NonLazy ();
	}
	
	private void DeclareSignals()
	{
		Container.DeclareSignal<ExampleSignal> ();
		Container.DeclareSignal<ChangeStateSignal> ();
		
		Container.DeclareSignal<GameCreditsSignal> ();
		Container.DeclareSignal<GameHighscoreSignal> ();
		Container.DeclareSignal<GameStartSignal> ();
		Container.DeclareSignal<LevelLostSignal> ();
		Container.DeclareSignal<LevelWinSignal> ();
		Container.DeclareSignal<LevelStartSignal> ();
	}

	private void InstallStates()
	{
		Container.Bind<PauseState> ().AsSingle ();
		
		Container.Bind<StartState> ().AsSingle ();
		Container.Bind<CreditsState> ().AsSingle ();
		Container.Bind<HighscoreState> ().AsSingle ();
		
		Container.Bind<LevelChoseState> ().AsSingle ();
		Container.Bind<LevelState> ().AsSingle ();		
		Container.Bind<LevelLostState> ().AsSingle ();
		Container.Bind<LevelWinState> ().AsSingle ();
		
		// OLD SHIT
		Container.Bind<FirstPersonState> ().AsSingle ();
		Container.Bind<ClickToMoveState> ().AsSingle ();
	}
}