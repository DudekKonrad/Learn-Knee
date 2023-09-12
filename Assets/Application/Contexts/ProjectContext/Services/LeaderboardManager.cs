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
        [SerializeField] private GameObject _loginWindow;
        [SerializeField] private Toggle _rememberToggle;

        [SerializeField] private Text _errorMessage;
        [SerializeField] private Text _informationMessage;
        [SerializeField] private Text _waitText;

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
            _waitText.DOFade(1f, _gameConfig.TextFadeDuration);
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
            _waitText.DOFade(0f, _gameConfig.TextFadeDuration);
            PlayerPrefs.SetString("Email", $"{_emailInputField.text}");
            PlayerPrefs.SetString("Password", $"{_passwordInputField.text}");
            PlayerPrefs.SetInt("RememberLogin", _rememberToggle.isOn ? 1 : 0);
            SetInformationMessage("LOGINSUCESSFUL");
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
                DisplayName = StringExtensionMethods.GetStringTillChar(_emailInputField.text, "@")
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
            _waitText.DOFade(0f, _gameConfig.TextFadeDuration);
            if (_passwordInputField.text.Length < 6)
            {
                SetErrorMessage("PASSWORDTOOSHORT");
                return;
            }
            switch (error.Error)
            {
               case PlayFabErrorCode.ConnectionError:
                   SetErrorMessage("NOINTERNETCONNECTION");
                   break;
               case PlayFabErrorCode.InvalidEmailOrPassword:
                   SetErrorMessage("INVALIDEMAILORPASSWORD");
                   break;
               case PlayFabErrorCode.EmailAddressNotAvailable:
                   SetErrorMessage("EMAILADDRESSNOTAVAILABLE");
                   break;
               case PlayFabErrorCode.InvalidParams:
                   SetErrorMessage("INCORRECTMAIL");
                   break;
               case PlayFabErrorCode.AccountNotFound:
                   SetErrorMessage("ACCOUNTNOTFOUND");
                   break;
            }
        }

        private void SetErrorMessage(string translationKey)
        {
            _errorMessage.gameObject.GetComponent<LocalizedText>().SetTranslationKey(translationKey);
            _errorMessage.DOColor(
                new Color(_errorMessage.color.r, _errorMessage.color.g, _errorMessage.color.b,1),
                _gameConfig.TextFadeDuration);
            DOVirtual.DelayedCall(3f, () => _errorMessage.DOColor(
                new Color(_errorMessage.color.r, _errorMessage.color.g, _errorMessage.color.b, 0),
                _gameConfig.TextFadeDuration));
        }
        
        private void SetInformationMessage(string translationKey)
        {
            _informationMessage.gameObject.GetComponent<LocalizedText>().SetTranslationKey(translationKey);
            _informationMessage.DOColor(
                new Color(_informationMessage.color.r, _informationMessage.color.g, _informationMessage.color.b,1),
                _gameConfig.TextFadeDuration);
            DOVirtual.DelayedCall(3f, () => _informationMessage.DOColor(
                new Color(_informationMessage.color.r, _informationMessage.color.g, _informationMessage.color.b,0),
                _gameConfig.TextFadeDuration));
        }

        private void DisableErrorMessage()
        {
            _errorMessage.DOColor(
                new Color(_errorMessage.color.r, _errorMessage.color.g, _errorMessage.color.b,1),
                _gameConfig.TextFadeDuration); 
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

        public void PlayOffline()
        {
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
            _waitText.DOFade(1f, _gameConfig.TextFadeDuration);
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            SetInformationMessage("REGISTERSUCESSFUL");
            _waitText.DOFade(0f, _gameConfig.TextFadeDuration);
            Debug.Log($"Registered and logged in");
            LoginButton();
        }
    }
}