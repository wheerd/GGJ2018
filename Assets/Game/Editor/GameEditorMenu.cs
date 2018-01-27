using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEditorMenu : MonoBehaviour
{

	[MenuItem("Game/Start Game")]
	static void PlayGame()
	{
		if ( EditorApplication.isPlaying == true )
		{
			EditorApplication.isPlaying = false;
			return;
		}

		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Game/Scenes/Main.unity");
		EditorApplication.isPlaying = true;
	}
}
