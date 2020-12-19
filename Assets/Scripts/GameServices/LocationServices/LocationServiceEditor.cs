using System;

namespace HeathCo.GameServices
{
    public class LocationServiceEditor : LocationServiceBase
	{
		private const float UPDATE_FREQUENCY_MIN = 0.5f;
		private const float UPDATE_FREQUENCY_MAX = 5.0f;
		private const float LATITUDE_MIN = -90f;
		private const float LATITUDE_MAX = 90f;
		private const float LONGITUDE_MIN = 0f;
		private const float LONGITUDE_MAX = 180f;

		/// <summary>
		/// Holds the time the latitude should be updated next.
		/// </summary>
		private DateTime _nextLatitudeUpdateTime;

		/// <summary>
		/// Holds the time the longitude should be updated next.
		/// </summary>
		private DateTime _nextLongitudeUpdateTime;

		/// <summary>
		/// Cached value of the latitude.
		/// </summary>
		private float _latitude;

		/// <summary>
		/// Cached value of the longitude.
		/// </summary>
		private float _longitude;

		/// <summary>
		/// Basic constructor to set the initial latitudinal and longitudinal values.
		/// </summary>
		public LocationServiceEditor()
		{
			UpdateLatitude();
			UpdateLongitude();
		}

		/// <summary>
		/// Determine whether or not the location service is started.
		/// </summary>
		/// <returns>True if the service has started, otherwise false.</returns>
		public override bool IsStarted()
		{
			return true;
		}

		/// <summary>
		/// Determines the last known latitude of the device.
		/// </summary>
		/// <returns>The last known latitude of the device.</returns>
		public override float GetLatitude()
		{
			if (ShouldUpdateLatitude())
			{
				UpdateLatitude();
			}
			return _latitude;
		}

		/// <summary>
		/// Determines the last known longitude of the device.
		/// </summary>
		/// <returns>The last known longitude of the device.</returns>
		public override float GetLongitude()
		{
			if (ShouldUpdateLongitude())
			{
				UpdateLongitude();
			}
			return _longitude;
		}

		/// <summary>
		/// Determine whether or not the user has location permissions enabled on this device.
		/// </summary>
		/// <returns>True if location permissions are enabled, otherwise false.</returns>
		protected override bool HasPermissions()
		{
			return true;
		}

		/// <summary>
		/// Updates the latitude and restarts the timer for updating it again.
		/// </summary>
		private void UpdateLatitude()
		{
			_latitude = UnityEngine.Random.Range(LATITUDE_MIN, LATITUDE_MAX);
			_nextLatitudeUpdateTime = DateTime.Now.AddSeconds(UnityEngine.Random.Range(UPDATE_FREQUENCY_MIN, UPDATE_FREQUENCY_MAX));
		}

		/// <summary>
		/// Updates the longitude and restarts the timer for updating it again.
		/// </summary>
		private void UpdateLongitude()
		{
			_longitude = UnityEngine.Random.Range(LONGITUDE_MIN, LONGITUDE_MAX);
			_nextLongitudeUpdateTime = DateTime.Now.AddSeconds(UnityEngine.Random.Range(UPDATE_FREQUENCY_MIN, UPDATE_FREQUENCY_MAX));
		}

		/// <summary>
		/// Determine whether or not the latitude should be updated.
		/// </summary>
		/// <returns>True if the latitude should be updated, otherwise false.</returns>
		private bool ShouldUpdateLatitude()
		{
			return DateTime.Now.CompareTo(_nextLatitudeUpdateTime) > 0;
		}

		/// <summary>
		/// Determine whether or not the longitude should be updated.
		/// </summary>
		/// <returns>True if the latitude should be updated, otherwise false.</returns>
		private bool ShouldUpdateLongitude()
		{
			return DateTime.Now.CompareTo(_nextLongitudeUpdateTime) > 0;
		}
	}
}