using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;

namespace HeathCo.GameServices
{
	/// <summary>
	/// Base class to use for location services for a variety of platforms. Can be
	/// overridden to handle any custom behaviors as desired.
	/// </summary>
	public abstract class LocationServiceBase : ILocationService
	{
		/// <summary>
		/// Determine whether or not this service is supported in the
		/// current game configuration.
		/// </summary>
		/// <returns>True if the service is supported, otherwise false.</returns>
		public bool IsSupported()
		{
			return true;
		}

		/// <summary>
		/// Call to start the location service. Should prompt for permissions if required.
		/// </summary>
		/// <param name="accuracyMeters">Desired accuracy of the user's location in meters.</param>
		/// <param name="onSuccess">Method to call if the service starts successfully.</param>
		/// <param name="onFailure">Method to call if the service fails to start.</param>
		public async void Start(float accuracyMeters, Action onSuccess, Action onFailure)
		{
			if (HasPermissions())
			{
				await StartupService(accuracyMeters, onSuccess, onFailure);
			}
			else
			{
				onFailure?.Invoke();
			}
		}

		/// <summary>
		/// Helper method to handle asynchronously starting up the location services.
		/// </summary>
		/// <param name="accuracyMeters">Desired accuracy of the user's location in meters.</param>
		/// <param name="onSuccess">Method to call if the service starts successfully.</param>
		/// <param name="onFailure">Method to call if the service fails to start.</param>
		private async Task StartupService(float accuracyMeters, Action onSuccess, Action onFailure)
		{

			// Uncomment if you want to test with Unity Remote
			/*
    #if UNITY_EDITOR
                    yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
                    yield return new WaitForSecondsRealtime(5f);
    #endif
            */

			Input.location.Start(accuracyMeters);
			while (IsInitializing() && !FailedInitialization())
			{
				await Task.Delay(100);
			}

			if (IsStarted())
			{
				onSuccess?.Invoke();
			}
			else
			{
				onFailure?.Invoke();
			}
		}

		/// <summary>
		/// Call to stop the location service.
		/// </summary>
		public void Stop()
		{
			Input.location.Stop();
		}

		/// <summary>
		/// Determine whether or not the location service is started.
		/// </summary>
		/// <returns>True if the service has started, otherwise false.</returns>
		public bool IsStarted()
		{
			return Input.location.status == LocationServiceStatus.Running;
		}

		/// <summary>
		/// Determines the last known latitude of the device.
		/// </summary>
		/// <returns>The last known latitude of the device.</returns>
		public float GetLatitude()
		{
			return Input.location.lastData.latitude;
		}

		/// <summary>
		/// Determines the last known longitude of the device.
		/// </summary>
		/// <returns>The last known longitude of the device.</returns>
		public float GetLongitude()
		{
			return Input.location.lastData.longitude;
		}

		/// <summary>
		/// Determine whether or not the user has location permissions enabled on this device.
		/// </summary>
		/// <returns>True if location permissions are enabled, otherwise false.</returns>
		protected virtual bool HasPermissions()
		{
			return Input.location.isEnabledByUser;
		}

		/// <summary>
		/// Determine whether or not the location services are still initializing or not.
		/// </summary>
		/// <returns>True while the location services are initializing, otherwise false.</returns>
		private bool IsInitializing()
		{
			return Input.location.status == LocationServiceStatus.Initializing;
		}

		/// <summary>
		/// Determine whether or not the location services failed to initialize.
		/// </summary>
		/// <returns>True if the initialization failed, otherwise false.</returns>
		private bool FailedInitialization()
		{
			return Input.location.status == LocationServiceStatus.Failed;
		}
	}
}