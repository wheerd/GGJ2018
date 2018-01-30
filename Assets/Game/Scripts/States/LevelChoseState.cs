using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class LevelChooseState : DefaultState {

    TrackingService _trackingService;

    public LevelChooseState(TrackingService trackingService)
    {
        _trackingService = trackingService;
    }


	override public void Load ()
	{
		SceneManager.LoadScene ("LevelChoseScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("LevelChoseUI", LoadSceneMode.Additive);

        _trackingService.ScreenEnter("levelChoose");
	}

	override public void Unload ()
	{
        _trackingService.ScreenLeave("levelChoose");

        SceneManager.UnloadSceneAsync ("LevelChoseUI");
		SceneManager.UnloadSceneAsync ("LevelChoseScene");
	}
}
