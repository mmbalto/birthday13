using UnityEngine.Android;

namespace HeathCo.GameServices
{
	/// <summary>
	/// Android-specific version of the location service to allow for requesting location
	/// service permissions at runtime prior to checking the result.
	/// </summary>
    public class LocationServiceAndroid : LocationServiceBase
	{

		/// <summary>
		/// Determine whether or not the user has location permissions enabled on this device.
		/// </summary>
		/// <returns>True if location permissions are enabled, otherwise false.</returns>
		protected override bool HasPermissions()
		{
			if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
			{
				Permission.RequestUserPermission(Permission.FineLocation);
			}

			return base.HasPermissions();
		}
	}
}