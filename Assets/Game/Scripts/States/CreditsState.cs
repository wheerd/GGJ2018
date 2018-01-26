using de.deichkrieger.stateMachine;
using UnityEngine.SceneManagement;

public class CreditsState : DefaultState {
    override public void Load ()
    {
        SceneManager.LoadScene ("CreditsScene", LoadSceneMode.Additive);
    }

    override public void Unload ()
    {
        SceneManager.UnloadSceneAsync ("CreditsScene");
    }
}
