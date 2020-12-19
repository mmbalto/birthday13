using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeathCo.Game
{
    using HeathCo.ServiceLocator;
    using HeathCo.GameServices;

    /// <summary>
    /// Class used to initialize our game and ensure that all of the required subsystems
    /// are initialized and running.
    /// </summary>
    public class MainGame : MonoBehaviour
    {
        private const float LOCATION_ACCURACY_METERS = 1f;

        [SerializeField]
        private Text _latitude;

        [SerializeField]
        private Text _longitude;

        [SerializeField]
        private GameObject _tempCube;

        private void Start()
        {
#if UNITY_ANDROID
            ServiceLocator.Instance.Register<ILocationService>(new LocationServiceAndroid());
#elif UNITY_EDITOR
            ServiceLocator.Instance.Register<ILocationService>(new LocationServiceEditor());
#endif

            ServiceLocator.Instance.Get<ILocationService>().Start(LOCATION_ACCURACY_METERS, OnLocationServiceSucceeded, OnLocationServiceFailed);
            Input.gyro.enabled = true;
        }

        private void OnLocationServiceSucceeded()
        {
            Debug.LogFormat("SUCCESS!");
        }

        private void OnLocationServiceFailed()
        {
            Debug.LogFormat("FAILURE!");
        }

        private void Update()
        {
			if (ServiceLocator.Instance.Get<ILocationService>().IsStarted())
			{
				_latitude.text = ServiceLocator.Instance.Get<ILocationService>().GetLatitude().ToString("F5");
				_longitude.text = ServiceLocator.Instance.Get<ILocationService>().GetLongitude().ToString("F5");
			}
			else
			{
				_latitude.text = "Initializing...";
				_longitude.text = "Initializing...";
			}

            GyroModifyCamera();
		}

        void GyroModifyCamera()
        {
            _tempCube.transform.rotation = Input.gyro.attitude;
        }
    }
}