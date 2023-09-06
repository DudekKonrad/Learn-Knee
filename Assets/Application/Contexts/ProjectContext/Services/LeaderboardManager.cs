using System.Collections.Generic;
using Application.MainMenuContext.Views;
using Application.ProjectContext.Configs;
using Application.Utils;
using DG.Tweening;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.ProjectContext.Services
{
    public class LeaderboardManager : MonoBehaviour
    {
        [Inject] private readonly LeaderboardService _leaderboardService;
        [Inject] private readonly LearnGameConfig _gameConfig;
        
        [SerializeField] private RankingRecordView _playerRecordPrefab;
        [SerializeField] private Transform _playersRecordContainer;
        [SerializeField] private InputField _emailInputField;
        [SerializeField] private InputField _passwordInputField;
        [SerializeField] private InputField _nicknameInputField;
        [SerializeField] private GameObject _loginWindow;
        [SerializeField] private Toggle _rememberToggle;

        [SerializeField] private Text _errorMessage;

        private void Start()
        {
            if (_leaderboardService.HighScore > 0) SendLeaderboard(_leaderboardService.HighScore);
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                _loginWindow.SetActive(false);
                GetLeaderboard();
            }
            else
            {
                _loginWindow.SetActive(true);
            }
            if (PlayerPrefs.GetInt("RememberLogin") == 1)
            {
                _emailInputField.text = PlayerPrefs.GetString("Email");
                _passwordInputField.text = PlayerPrefs.GetString("Password");
                _rememberToggle.isOn = true;
            }
        }

        public void LoginButton()
        {
            var request = new LoginWithEmailAddressRequest()
            {
                Email = _emailInputField.text,
                Password = _passwordInputField.text,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            PlayerPrefs.SetString("Email", $"{_emailInputField.text}");
            PlayerPrefs.SetString("Password", $"{_passwordInputField.text}");
            PlayerPrefs.SetInt("RememberLogin", _rememberToggle.isOn ? 1 : 0);
            if (result.InfoResultPayload.PlayerProfile.DisplayName == null)
            {
                SubmitPlayerName();
            }
            else
            {
                GetLeaderboard();
            }
        }

        private void SubmitPlayerName()
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = _nicknameInputField.text
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSubmitPLayerName, OnError);
        }

        private void OnSubmitPLayerName(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log($"Display name updated");
            GetLeaderboard();
            _loginWindow.SetActive(false);
        }

        private void OnError(PlayFabError error)
        {
            Debug.Log($"{error.GenerateErrorReport()}");
            Debug.Log($"Error: {error.Error}");
            switch (error.Error)
            {
               case PlayFabErrorCode.ConnectionError:
                   Debug.Log($"Connection error");
                   break;
               case PlayFabErrorCode.InvalidEmailOrPassword:
                   _errorMessage.gameObject.GetComponent<LocalizedText>().SetTranslationKey("INVALIDEMAILORPASSWORD");
                   _errorMessage.DOColor(
                       new Color(_errorMessage.color.r, _errorMessage.color.g, _errorMessage.color.b,1),
                       _gameConfig.TextFadeDuration);
                   break;
               case PlayFabErrorCode.EmailAddressNotAvailable:
                   _errorMessage.gameObject.GetComponent<LocalizedText>().SetTranslationKey("EMAILADDRESSNOTAVAILABLE");
                   _errorMessage.DOColor(
                       new Color(_errorMessage.color.r, _errorMessage.color.g, _errorMessage.color.b,1),
                       _gameConfig.TextFadeDuration);
                   break;
            }
        }

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
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnSendLeaderBoard, OnError);
        }

        private void OnSendLeaderBoard(UpdatePlayerStatisticsResult result) => Debug.Log($"Successful leaderboard send");

        public void GetLeaderboard()
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
            _playersRecordContainer.RemoveAllChildren();
            foreach (var record in result.Leaderboard)
            {
                var playerRecordPrefab = Instantiate(_playerRecordPrefab, _playersRecordContainer);
                playerRecordPrefab.SetPosition(record.Position+1);
                playerRecordPrefab.SetName(record.DisplayName);
                playerRecordPrefab.SetScore(record.StatValue);
            }
            _loginWindow.transform.DOMoveY(1080, 2f).OnComplete(() => _loginWindow.SetActive(false));
        }
        
        public void Register()
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = _emailInputField.text,
                Password = _passwordInputField.text,
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
        private void OnRegisterSuccess(RegisterPlayFabUserResult result) => Debug.Log($"Registered and logged in");
    }
}