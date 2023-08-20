using System.Collections.Generic;
using Application.ProjectContext.Models;
using Application.ProjectContext.Signals;
using Application.QuizContext.Services;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using Zenject;

namespace Application.ProjectContext.Services
{
    public class LeaderboardService
    {
        [Inject] private readonly SignalBus _signalBus;

        private List<LeaderboardRecord> _records = new List<LeaderboardRecord>();
        
        public List<LeaderboardRecord> Records => _records;

        [Inject]
        private void Construct()
        {
            _signalBus.Subscribe<LearnProjectSignals.GameFinished>(OnGameFinished);
            _signalBus.Subscribe<LearnProjectSignals.ActualizeLeaderboardSignal>(OnActualizeLeaderboard);
            Login();
        }

        private void OnActualizeLeaderboard(LearnProjectSignals.ActualizeLeaderboardSignal signal)
        {
            GetLeaderboard();
        }

        private void OnGameFinished(LearnProjectSignals.GameFinished signal)
        {
            SendLeaderboard(signal.GameResult.Score);
            GetLeaderboard();
        }

        private void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log($"Login successfully");
            if (result.InfoResultPayload.PlayerProfile != null)
            {
                var name = result.InfoResultPayload.PlayerProfile.DisplayName;
            }
            SubmitPlayerName();
            GetLeaderboard();
        }

        private void SubmitPlayerName()
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = "Konrad"
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayPlayerNameUpdate, OnError);
        }

        private void OnDisplayPlayerNameUpdate(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log($"Display name updated");
        }

        private void OnError(PlayFabError error) => Debug.Log($"{error.GenerateErrorReport()}");

        private void SendLeaderboard(int score)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate
                    {
                        StatisticName = "LearnKneeLeaderboard",
                        Value = score
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
        }

        private void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result) => Debug.Log($"Successful leaderboard send");

        private void GetLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "LearnKneeLeaderboard",
                StartPosition = 0,
                MaxResultsCount = 10
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        }

        private void OnLeaderboardGet(GetLeaderboardResult result)
        {
            _records.Clear();
            foreach (var item in result.Leaderboard)
            {
                Debug.Log($"Display name: {item.DisplayName}");
                _records.Add(new LeaderboardRecord(item.Position, item.DisplayName, item.StatValue));
            }
        }

        private void SaveResults(GameResult gameResult)
        {
            var request = new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>{
                {
                    "Results",JsonUtility.ToJson(gameResult)
                }}
            };
            PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        }

        private void OnDataSend(UpdateUserDataResult result) => Debug.Log($"Data send successfully");

        private void GetScores() => PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnScoreDataRecieved, OnError);

        private void OnScoreDataRecieved(GetUserDataResult result)
        {
            if (result.Data != null)
            {
                var playerResult = JsonUtility.FromJson<GameResult>(result.Data["Results"].Value);
            }
        }
    }
}