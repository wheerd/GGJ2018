using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class HighscoreState : DefaultState {

    private TrackingService _trackingService;

    public HighscoreState(TrackingService trackingService)
    {
        _trackingService = trackingService;
    }

	override public void Load ()
	{
		SceneManager.LoadScene ("HighscoreScene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("HighscoreUI", LoadSceneMode.Additive);

        _trackingService.ScreenEnter("highscore");
	}

	override public void Unload ()
	{
        _trackingService.ScreenLeave("highscore");

        SceneManager.UnloadSceneAsync ("HighscoreUI");
		SceneManager.UnloadSceneAsync ("HighscoreScene");
	}
}
