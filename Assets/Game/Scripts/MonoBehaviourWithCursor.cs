using UnityEngine;

namespace Assets.Game.Scripts
{
    public abstract class MonoBehaviourWithCursor : MonoBehaviour
    {
        public Texture2D CursorTexture;

        private void OnMouseOver()
        {
            Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
        }

        private void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
