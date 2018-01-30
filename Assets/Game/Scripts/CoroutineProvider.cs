using UnityEngine;

public class CoroutineProvider: MonoBehaviour {

    private static CoroutineProvider _instance;

    public static CoroutineProvider Instance
    {
        get
        {
            if (_instance == null)
            {
                var provider = new GameObject();
                _instance = provider.AddComponent<CoroutineProvider>();
            }

            return _instance;
        }
    }
}
