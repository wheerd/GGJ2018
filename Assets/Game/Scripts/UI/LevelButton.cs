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
	
	[Inject]
	private LevelNumberStartSignal _levelNumberStartSignal;

	public void StartLevel()
	{
		_levelNumberStartSignal.Fire(int.Parse(_text.text));
	}
	
	public void SetLevel(int level)
	{
		_text.text = level.ToString();
	}
	
	public void SetPlayed()
	{
		SetColor(Color.green);
	}

	public void SetPlayable()
	{
		SetColor(Color.yellow);
	}

	public void SetBlocked()
	{
		SetColor(Color.red);
	}
	
	private void SetColor(Color color)
	{
		_button.GetComponent<Image>().color = color;
	}
	
	
}
