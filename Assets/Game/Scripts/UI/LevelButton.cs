using de.deichkrieger.stateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelButton : MonoBehaviour
{
	[SerializeField] 
	Text _text;
	
	[SerializeField]
	Button _button;
	
	[SerializeField]
	private LevelChoseUI _levelChoseUi;

	private bool _isAllowed = true;

	public void StartLevel()
	{
		if (_isAllowed)
		{
			_levelChoseUi.StartLevel(int.Parse(_text.text));
		}
	}
	
	public void SetLevel(int level)
	{
		_text.text = level.ToString();
	}
	
	public void SetPlayed()
	{
		SetColor(Color.green);
		_isAllowed = true;
	}

	public void SetPlayable()
	{
		SetColor(Color.yellow);
		_isAllowed = true;
	}

	public void SetBlocked()
	{
		SetColor(Color.red);
		_isAllowed = false;
	}
	
	private void SetColor(Color color)
	{
		_button.GetComponent<Image>().color = color;
	}
	
	
}
