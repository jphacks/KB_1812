using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class CreateAccountScript : MonoBehaviour {

    // firebaseの認証に必要
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;

    InputField inputMail, inputPass, inputName;
    string mMailText, mPassText, mNameText;

<<<<<<< HEAD
=======
    void OnEnable(){

        // 各テキストフィールドから取得
        inputName = GameObject.Find("DisplayNameInput").GetComponent<InputField>();
        inputMail = GameObject.Find("MailInput").GetComponent<InputField>();
        inputPass = GameObject.Find("PassInput").GetComponent<InputField>();

        // 認証しているuserの取得
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        // Firebase
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hamukatsu2-9bebb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (auth != null)
        {
            Debug.Log("start()");
        }
    }
    /*
>>>>>>> b290b82d048cb6f943584eb14656e75c66f61a39
	// Use this for initialization
	void Start () {
        inputName = GameObject.Find("DisplayNameInput").GetComponent<InputField>();
        inputMail = GameObject.Find("MailInput").GetComponent<InputField>();
        inputPass = GameObject.Find("PassInput").GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick(){
        mNameText = inputName.text; //入力したDisplayNameを取得
        mMailText = inputMail.text; //入力したMailAdressを取得
        mPassText = inputPass.text; //入力したPasswordを取得

        //debug用
        Debug.Log(mNameText);
        Debug.Log(mMailText);
        Debug.Log(mPassText);

        createUser(mMailText, mPassText, mNameText);
    }

    void createUser(string email, string password, string name)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {

            if (task.IsCompleted)
            {
                Debug.Log("Complete");
            }

            if (task.IsCanceled)
            {
                Debug.LogError("It was canceled");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Debug.Log("新規作成前");
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.Log("新規作成後");

            // SetName
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = name,
            };

            //PlayerPrefs.SetString

            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);

            if (newUser != null)
            {
                Debug.Log("名前を更新して遷移する");

                newUser.UpdateUserProfileAsync(profile).ContinueWith(innerTask => {
                    if (innerTask.IsCanceled)
                    {
                        Debug.LogError("UpdateUserProfileAsync was canceled.");
                        return;
                    }
                    if (innerTask.IsFaulted)
                    {
                        Debug.LogError("UpdateUserProfileAsync encountered an error: " + innerTask.Exception);
                        return;
                    }

                    Debug.Log("User profile updated successfully.");
                    Debug.Log(newUser.DisplayName);
                });

            }
        });
    }
}
