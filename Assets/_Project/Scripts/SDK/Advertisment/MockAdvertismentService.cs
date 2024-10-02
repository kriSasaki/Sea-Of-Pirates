using System;
using Project.Interfaces.SDK;
using UnityEngine;

namespace Project.SDK.Advertisment
{
    public class MockAdvertismentService : AdvertismentService, IAdvertismentService
    {
        public override void ShowInterstitialAd()
        {
            Debug.Log("Interstitial Shown");
        }

        public override void ShowRewardAd(int rewardAmount)
        {
            Debug.Log("Reward" + rewardAmount + "Get");
        }

        public override void ShowSticky()
        {
            Debug.Log("Sticky Shown");
        }

        public override void HideSticky()
        {
            Debug.Log("Sticky Hided");
        }
    }
}
