using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LoginManager : MonoBehaviour
{
    public Text username;
    public Text password;
    public Text warningMessage;
    public static LoginManager instance = null;
    private bool mouseCanClick = true;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);

        PlayFabSettings.TitleId = "A1BEB";

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .AddOauthScope("profile")
            .RequestServerAuthCode(false)
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        username.text = string.Empty;
        password.text = string.Empty;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginWithoutPalyWithoutLogin()
    {
        if (mouseCanClick)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void Register()
    {
        LoginData.username = username.text;
        LoginData.password = password.text;
        //loginData = new LoginData()
        //{
        //    username = username.text,
        //    password = password.text
        //};

        if (!string.IsNullOrEmpty(username.text) && !string.IsNullOrEmpty(password.text) && mouseCanClick)
        {
            mouseCanClick = false;
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
            {
                Username = LoginData.username,
                Password = LoginData.password,
                Email = LoginData.username + "@gmail.com",
                DisplayName = LoginData.username
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
    }

    public void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Register Success");
        mouseCanClick = true;
        Login();
    }

    public void Login()
    {
        LoginData.username = username.text;
        LoginData.password = password.text;
        //loginData = new LoginData()
        //{
        //    username = username.text,
        //    password = password.text
        //};

        if (!string.IsNullOrEmpty(username.text) && !string.IsNullOrEmpty(password.text) && mouseCanClick)
        {
            mouseCanClick = false;
            LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
            {
                Username = LoginData.username,
                Password = LoginData.password
            };

            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
        }
    }

    public void OnLoginSuccess(LoginResult result)
    {
        mouseCanClick = true;
        Debug.Log("Logado");
        LoginData.playIsLogin = true;
        SceneManager.LoadScene("Menu");
    }

    public void OnError(PlayFabError error)
    {
        mouseCanClick = true;
        Debug.LogError(error.ErrorMessage);
        StartCoroutine(ShowWarningMessage(error.ErrorMessage));
    }

    public void LoginWithGoogleAccount()
    {
        Social.localUser.Authenticate((bool success) =>
        {

            if (success)
            {
                var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                Debug.Log("Server Auth Code: " + serverAuthCode);

                PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest()
                {
                    TitleId = PlayFabSettings.TitleId,
                    ServerAuthCode = serverAuthCode,
                    CreateAccount = true
                }, (result) =>
                {
                    Debug.Log("Signed In as " + result.PlayFabId);

                }, OnError);
            }
            else
            {
                StartCoroutine(ShowWarningMessage("Erro ao logar"));
            }

        });
    }

    IEnumerator ShowWarningMessage(string error)
    {
        warningMessage.text = error;
        yield return new WaitForSeconds(3);
        warningMessage.text = "";
    }

    public static class LoginData
    {
        public static string username;
        public static string password;
        public static bool playIsLogin = false;
    }
}
