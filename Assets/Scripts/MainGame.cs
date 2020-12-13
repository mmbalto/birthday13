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

        private void Start()
        {
            ServiceLocator.Instance.Get<ILocationService>().Start(LOCATION_ACCURACY_METERS, OnLocationServiceSucceeded, OnLocationServiceFailed);
        }

        private void OnLocationServiceSucceeded()
        {
        }

        private void OnLocationServiceFailed()
        {
        }

        private void Update()
        {
			if (ServiceLocator.Instance.Get<ILocationService>().IsStarted())
			{
				_latitude.text = Input.location.lastData.latitude.ToString("F5");
				_longitude.text = Input.location.lastData.longitude.ToString("F5");
			}
			else
			{
				_latitude.text = "Initializing...";
				_longitude.text = "Initializing...";
			}
		}
    }
}