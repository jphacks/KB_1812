using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class LoginScript : MonoBehaviour {

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;

    // TODO
    // ログインの後に，名前を変える処理を行う．
    // そのときに，値が変わったのを取れるかどうか

    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hamukatsu2-9bebb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (user == null)
        {
            Debug.Log("ログインしてない");
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {  //Executed when "a" pressed down
            Debug.Log("createUser");
            createUser();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("login");
            login();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("logout");
            logout();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //SceneManager.LoadScene("Main");
        }
    }

    void createUser()
    {
        string email = "new@gmail.com";
        string password = "12345678";

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {

            if(task.IsCompleted){

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

            // TODO セットネイム
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = "Jane Q. User",
            };

            //PlayerPrefs.SetString


            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);

            if (newUser == null)
            {
                Debug.Log("DB1");
                //var userInfo = new Dictionary<string, string>()
                //{
                //    { "email" , email },
                //    { "password" , password },
                //    { "name" , "karuchan" }
                //};

                //var info = JsonUtility.ToJson(userInfo);

                Debug.Log("DB2");
                //reference.Child("users").Child(user.UserId).SetValueAsync(info);

                //Debug.Log("シーン移行");
                //SceneManager.LoadScene("Main");
            }
            if (newUser != null)
            {
                Debug.Log("ダメんご");

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

    void login()
    {
        string email = "sample@gmail.com";
        string password = "12345678";

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if(task.IsCompleted){

            }

            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
             
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            if (user == null)
            {
                Debug.Log("シーン移行");
                SceneManager.LoadScene("Main");
            }

        });
    }

    void logout()
    {
        auth.SignOut();
    }

    void checkUserLogin()
    {

    }


}
