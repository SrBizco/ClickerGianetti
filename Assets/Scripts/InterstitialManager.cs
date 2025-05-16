using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iosAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    bool isLoaded;

    void Start()
    {
#if UNITY_IOS
        _adUnitId = _iosAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        isLoaded = false;
    }

    public void Initialize()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        if (isLoaded)
        {
            Advertisement.Show(_adUnitId, this);
            isLoaded = false; // volver a cargar luego
        }
        else
        {
            Debug.LogWarning("El interstitial aún no está cargado.");
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        isLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError("OnUnityAdsFailedToLoad: " + message);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError("OnUnityAdsShowFailure: " + message);
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Advertisement.Load(_adUnitId, this); // recargar para siguiente uso
    }
}
