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
		Container.DeclareSignal<ExampleSignal> ();
		Container.DeclareSignal<ChangeStateSignal> ();

		Container.BindSignal<string, ExampleSignal> ()
            .To<TestModel> (x => x.Test).AsSingle ().NonLazy ();

		Container.BindInterfacesTo<GameModel> ().AsSingle ().NonLazy ();

		Container.Bind<PauseState> ().AsSingle ();
		Container.Bind<StartState> ().AsSingle ();
		Container.Bind<FirstPersonState> ().AsSingle ();
		Container.Bind<ClickToMoveState> ().AsSingle ();
	}
}