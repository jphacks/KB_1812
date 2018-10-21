using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DisplayDestination : MonoBehaviour
{
	

	[SerializeField]
	private GameObject destination;

  public List<GameObject> _spawnObjects;
	private Vector2d[] _locations;
	public string[] _locationStrings;
    public GameObject Canvas;
    public roomScript rs;
  
	private AbstractMap _map;

    //Firebaseの設定用変数
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser loginUser;
    DatabaseReference reference;
  
	// Use this for initialization
	void Start()
	{
        rs = Canvas.GetComponent<roomScript>();
		_map = GameObject.Find("Map").GetComponent<AbstractMap>();
		_locations = new Vector2d[_locationStrings.Length];
		_spawnObjects = new List<GameObject>();
		for (int i = 0; i < _locationStrings.Length; i++)
		{
			var location_string = _locationStrings[i];
			_locations[i] = Conversions.StringToLatLon(location_string);
			var instance = Instantiate(destination);
			instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
			float _spawnScale = 1.0f;
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			_spawnObjects.Add(instance);

		}

        //auth関係初期設定
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        loginUser = auth.CurrentUser;
        //database関係初期設定
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hamukatsu3-aba8b.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (loginUser != null)
        {
            Debug.Log("ログイン済");
        }

        // TODO string roomIdを引数にして(一番大事w)

        string roomId;
        roomId = rs.room_text;
        double[] d = getDestination(roomId);
        getDest(d);
	}

	// Update is called once per frame
	void Update()
	{
		int count = _spawnObjects.Count;
		for (int i = 0; i < count; i++)
		{
			var spawnedObject = _spawnObjects[i];
			var locate = _locations[i];
			spawnedObject.transform.localPosition = _map.GeoToWorldPosition(locate, true);
		}
	}

    // getDestinationとgetDest

    // _locationStrings[0]に入れる
    double[] getDestination(string roomId)
    {

        // ルーム内にて，目的地を取得する
        // TODO 前シーンで入力されたroomIdの保持をお願いしますb
        //string roomId = "982675"; // 仮パス
        double[] d = new double[] { 1, 2 };

        Debug.Log("1");

        reference.Child("rooms").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
                return d;
            }
            else if (task.IsCompleted)
            {
                Debug.Log("hoge2");
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot room in snapshot.Children)
                {
                    //これは次の画面で渡すやつ
                    IDictionary roomValue = (IDictionary)room.Value;
                    string roomKey = room.Key;
                    if (roomKey == roomId)
                    {
                        //グローバル変数のdoubleの配列を用意してそこにこの２つを代入
                        double latitude = (double)roomValue["latitude"];
                        double longitude = (double)roomValue["longitude"];
                        // d = new double[] { latitude, longitude };
                        Debug.Log("hoge3");
                        break;
                        //return destination;
                    }
                }
                Debug.Log("hoge4");
                return d;
            }
            Debug.Log("hoge5");
            return d;
        });


        Debug.Log("2");
        return d;
    }

    void getDest(double[] d)
    {
        string lat = d[0].ToString();
        string lan = d[1].ToString();

        _locationStrings[0] = lat + "," + lan;
    }
}
