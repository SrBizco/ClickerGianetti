using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public BannersManager banner;
    public InterstitialManager interstitial;
    public RewardedManager rewarded;
    [SerializeField] private bool isTesting;

    private string gameId;

    private void Awake()
    {
#if UNITY_ANDROID
        gameId = "5854989";
#elif UNITY_IOS
        gameId = "TU_GAME_ID_IOS";
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Debug.Log("HOLAAAAAA inicializando ADS" + isTesting);
            Advertisement.Initialize(gameId, isTesting, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Inicialización de ads exitosa!!!");

        banner?.Initialize();
        interstitial?.Initialize();
        rewarded?.Initialize();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError("Falló la inicialización de los ads: " + message);
    }
}
