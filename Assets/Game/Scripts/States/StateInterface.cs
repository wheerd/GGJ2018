namespace de.deichkrieger.stateMachine
{
	interface StateInterface
	{
		void Load ();

		void Unload ();
	}

	public class DefaultState : StateInterface
	{

		public virtual void Load ()
		{
			throw new System.NotImplementedException ();
		}

		public virtual void Unload ()
		{
			throw new System.NotImplementedException ();
		}

	}
}