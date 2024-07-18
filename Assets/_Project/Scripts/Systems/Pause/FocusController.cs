using Agava.WebUtility;
using Project.SDK.Advertisment;
using Project.Systems.Pause;
using UnityEngine;

public class FocusController : MonoBehaviour
{
    private PauseService _pauseService;
    private AdvertismentService _advertisingService;

    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    public void Initialize(PauseService pauseService, AdvertismentService advertisingService)
    {
        _pauseService = pauseService;
        _advertisingService = advertisingService;
    }

    private void OnInBackgroundChangeApp(bool inApp)
    {
        if (_advertisingService.IsAdsPlaying == false)
            PauseGame(!inApp);
    }

    private void OnInBackgroundChangeWeb(bool isBackground)
    {
        if (_advertisingService.IsAdsPlaying == false)
            PauseGame(isBackground);
    }

    private void PauseGame(bool isUnfocused)
    {
        if (isUnfocused)
        {
            _pauseService.Pause();
        }
        else
        {
            _pauseService.Unpause();
        }
    }
}
