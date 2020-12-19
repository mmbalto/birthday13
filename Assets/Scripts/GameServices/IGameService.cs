namespace HeathCo.GameServices
{
	/// <summary>
	/// Interface that all game service classes must implement in order to be
	/// registered with the service locator.
	/// </summary>
	public interface IGameService
	{
		/// <summary>
		/// Determine whether or not this service is supported in the
		/// current game configuration.
		/// </summary>
		/// <returns>True if the service is supported, otherwise false.</returns>
		bool IsSupported();
	}
}