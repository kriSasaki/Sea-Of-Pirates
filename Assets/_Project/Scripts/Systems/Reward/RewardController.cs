using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Configs.GameResources;
using Project.Interfaces.Data;
using Project.Interfaces.SDK;
using Project.Interfaces.Stats;
using Project.Systems.Data;
using Project.UI.Reward;
using UnityEngine;
using YG;
using Zenject;

namespace Project.Systems.Reward
{
    public class RewardController : MonoBehaviour
    {
        private const int RewardAmountDivider = 2;

        [SerializeField] private GameResource _rewardType;
        [SerializeField] private int _minRewardAmount = 5;
        [SerializeField, Range(10f, 60f)] private float _offerDuration;
        [SerializeField, Range(5f, 600f)] private float _offerInterval;

        private StatsSheet _statSheet;
        private IUpgradableStats _stats;
        private IPlayerStorage _playerStorage;
        private IAdvertismentService _advertismentService;
        private RewardView _rewardView;

        private void Start()
        {
            OfferRewardAsync().Forget();
        }

        private void OnDestroy()
        {
            YandexGame.RewardVideoEvent -= AddReward;
        }

        [Inject]
        public void Construct(
            StatsSheet statSheet,
            IUpgradableStats stats,
            IPlayerStorage playerStorage,
            IAdvertismentService advertismentService,
            RewardView rewardView)
        {
            _statSheet = statSheet;
            _stats = stats;
            _playerStorage = playerStorage;
            _advertismentService = advertismentService;
            _rewardView = rewardView;

            YandexGame.RewardVideoEvent += AddReward;
        }

        private async UniTaskVoid OfferRewardAsync()
        {
            await UniTask.WaitForSeconds(_offerInterval, cancellationToken: destroyCancellationToken);

            ShowRewardOffer();
        }

        private void ShowRewardOffer()
        {
            int rewardAmount = ComputeRewardAmount();
            Sprite rewardSprite = _rewardType.Sprite;

            _rewardView.OfferExpired += OnOfferExpired;

            _rewardView.Show(rewardSprite, rewardAmount, _offerDuration, () => OnRewardRequested(rewardAmount));
        }

        private void AddReward(int rewardAmount)
        {
            _playerStorage.AddResource(_rewardType, rewardAmount);
        }

        private int ComputeRewardAmount()
        {
            int rewardAmount = 0;

            foreach (StatConfig stat in _statSheet.Stats)
            {
                int level = _stats.GetStatLevel(stat.StatType);
                List<GameResourceAmount> price = stat.GetUpgradePrice(level);

                rewardAmount += price.Where(p => p.Resource == _rewardType).Sum(res => res.Amount);
            }

            rewardAmount /= RewardAmountDivider;

            return Mathf.Max(_minRewardAmount, rewardAmount);
        }

        private void OnRewardRequested(int rewardAmount)
        {
            _advertismentService.ShowRewardAd(rewardAmount);
        }

        private void OnOfferExpired()
        {
            _rewardView.OfferExpired -= OnOfferExpired;
            OfferRewardAsync().Forget();
        }
    }
}