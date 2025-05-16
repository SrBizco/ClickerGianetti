using UnityEngine;
using UnityEngine.Advertisements;

public class BannersManager : MonoBehaviour
{
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iosAdUnitId = "Banner_iOS";
    string _adUnitId = null;

    void Start()
    {
#if UNITY_IOS
        _adUnitId = _iosAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
    }

    internal void Initialize()
    {
        BannerLoadOptions options = new BannerLoadOptions();
        options.loadCallback = ShowBanner;
        options.errorCallback = OnBannerErrorCb;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(_adUnitId, options);
    }

    public void ShowBanner()
    {
        Advertisement.Banner.Show(_adUnitId);
    }

    private void OnBannerErrorCb(string message)
    {
        Debug.LogError("OnBannerErrorCb: " + message);
    }
}
