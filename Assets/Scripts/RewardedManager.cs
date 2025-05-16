using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iosAdUnitId = "Rewarded_iOS";
    string _adUnitId;
    bool isLoaded;
    bool rewardOtorgado = false;

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
            rewardOtorgado = false;
            Advertisement.Show(_adUnitId, this);
            isLoaded = false;
        }
        else
        {
            Debug.LogWarning("El rewarded aún no está cargado.");
        }
    }

    public bool RewardFueOtorgado()
    {
        return rewardOtorgado;
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
        if (placementId == _adUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            rewardOtorgado = true;
            Debug.Log("✅ Reward otorgado!");
        }

        Advertisement.Load(_adUnitId, this); // recargar
    }
}
