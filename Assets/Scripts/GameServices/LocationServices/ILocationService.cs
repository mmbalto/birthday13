using System;

namespace HeathCo.GameServices
{
	/// <summary>
	/// Interface to use for all location-service implementations.
	/// </summary>
	public interface ILocationService : IGameService
	{
		/// <summary>
		/// Call to start the location service. Should prompt for permissions if required.
		/// </summary>
		/// <param name="accuracyMeters">Desired accuracy of the user's location in meters.</param>
		/// <param name="onSuccess">Method to call if the service starts successfully.</param>
		/// <param name="onFailure">Method to call if the service fails to start.</param>
		void Start(float accuracyMeters, Action onSuccess, Action onFailure);

		/// <summary>
		/// Call to stop the location service.
		/// </summary>
		void Stop();

		/// <summary>
		/// Determine whether or not the location service is started.
		/// </summary>
		/// <returns>True if the service has started, otherwise false.</returns>
		bool IsStarted();

		/// <summary>
		/// Determines the last known latitude of the device.
		/// </summary>
		/// <returns>The last known latitude of the device.</returns>
		float GetLatitude();

		/// <summary>
		/// Determines the last known longitude of the device.
		/// </summary>
		/// <returns>The last known longitude of the device.</returns>
		float GetLongitude();
	}
}