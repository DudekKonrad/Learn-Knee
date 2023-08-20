using Application.MainMenuContext.Views;
using Application.ProjectContext.Services;
using Application.ProjectContext.Signals;
using Application.Utils;
using UnityEngine;
using Zenject;

namespace Application.MainMenuContext.Mediators
{
    public class RankingMediator : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LeaderboardService _leaderboardService;
        
        
        [SerializeField] private RankingRecordView _playerRecordPrefab;
        [SerializeField] private Transform _playersRecordContainer;

        private void Awake()
        {
            _signalBus.Fire(new LearnProjectSignals.ActualizeLeaderboardSignal());
            RefreshRecords();
        }

        private void OnEnable()
        {
            _signalBus.Fire(new LearnProjectSignals.ActualizeLeaderboardSignal());
            RefreshRecords();
        }

        private void RefreshRecords()
        {
            Debug.Log($"Refreshing records");
            _playersRecordContainer.RemoveAllChildren();
            foreach (var record in _leaderboardService.Records)
            {
                Debug.Log($"Record info: {record}");
                var playerRecordPrefab = Instantiate(_playerRecordPrefab, _playersRecordContainer);
                playerRecordPrefab.SetPosition(record.Position+1);
                playerRecordPrefab.SetName(record.Id);
                playerRecordPrefab.SetScore(record.Score);
            }
        }
    }
}