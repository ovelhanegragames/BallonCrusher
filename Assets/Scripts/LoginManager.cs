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
    public Text welcomeText;
    public Button continueButton;
    public Button noLoginButton;
    public static LoginManager instance = null;
    private bool mouseCanClick = true;

    private int hasLoginOneTime = 0;
    private string playerLoginName = string.Empty;

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

        welcomeText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        noLoginButton.gameObject.SetActive(false);
        username.transform.parent.gameObject.SetActive(false);


        hasLoginOneTime = PlayerPrefs.GetInt("login", 0);
        playerLoginName = PlayerPrefs.GetString("username", string.Empty);
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO
        //remove de ongTest for release
        //if (hasLoginOneTime == 1 && !playerLoginName.Equals("ongTest"))
        if (hasLoginOneTime == 1)
        {
            //TODO
            //Make login with username load from playerpref
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = "Hi, " + playerLoginName + "\n\n" + "Wait for a moment";
            SimpleLogin(playerLoginName);
        }
        else
        {
            welcomeText.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
            username.transform.parent.gameObject.SetActive(true);
        }
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
        //if (!string.IsNullOrEmpty(username.text) && !string.IsNullOrEmpty(password.text) && mouseCanClick)
        if (!string.IsNullOrEmpty(username.text))
        {
            continueButton.enabled = false;
            continueButton.gameObject.SetActive(false);
            welcomeText.text = "Wait while we prepare everything.";
            mouseCanClick = false;
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
            {
                Username = username.text,
                Password = username.text,
                Email = username.text + "@gmail.com",
                DisplayName = username.text
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
    }

    public void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Register Success");
        mouseCanClick = true;

        PlayerPrefs.SetString("username", username.text);
        PlayerPrefs.SetInt("login", 1);

        SimpleLogin(username.text);
    }

    public void Login()
    {
        //if (!string.IsNullOrEmpty(username.text) && !string.IsNullOrEmpty(password.text) && mouseCanClick)
        if (!string.IsNullOrEmpty(username.text))
        {
            mouseCanClick = false;
            LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
            {
                Username = username.text,
                Password = username.text
            };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
        }
    }

    public void SimpleLogin(string user)
    {
        mouseCanClick = false;
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            Username = user,
            Password = user
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }

    public void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logado");
        mouseCanClick = true;

        LoginData.playfabId = result.PlayFabId;
        LoginData.playIsLogin = true;
        SceneManager.LoadScene("Menu");
    }

    public void OnError(PlayFabError error)
    {
        mouseCanClick = true;
        noLoginButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        username.transform.parent.gameObject.SetActive(true);
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
        public static string playfabId;
        public static bool playIsLogin = false;
    }
}
