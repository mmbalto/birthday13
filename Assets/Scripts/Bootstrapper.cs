using UnityEngine;
using UnityEngine.SceneManagement;
using HeathCo.GameServices;

namespace HeathCo.Core
{
    using HeathCo.ServiceLocator;

    public class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initiailze()
        {
            // Register all your services next.
            ServiceLocator.Instance.Register<ILocationService>(new LocationServiceAndroid());

            // Application is ready to start, load your main scene.
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
    }
}