using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class CreditsState : DefaultState {

    private TrackingService _trackingService;

    public CreditsState(TrackingService trackingService)
    {
        _trackingService = trackingService;
    }

    override public void Load ()
    {
        SceneManager.LoadScene ("CreditsScene", LoadSceneMode.Additive);
        SceneManager.LoadScene ("CreditsUI", LoadSceneMode.Additive);

        _trackingService.ScreenEnter("credits");
    }

    override public void Unload ()
    {
        _trackingService.ScreenLeave("credits");

        SceneManager.UnloadSceneAsync ("CreditsUI");
        SceneManager.UnloadSceneAsync ("CreditsScene");
    }
}
