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
            // Register all of the services.
#if UNITY_ANDROID
            ServiceLocator.Instance.Register<ILocationService>(new LocationServiceAndroid());
#elif UNITY_EDITOR
            ServiceLocator.Instance.Register<ILocationService>(new LocationServiceEditor());
#endif

            // Application is ready to start, load your main scene.
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
    }
}