using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Unity;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Geocoding;
using System.Globalization;


//[DefaultExecutionOrder(100)]
public class CoordSend : MonoBehaviour
{
  //public GameObject obj;
	[SerializeField]
  private Text latitude;//緯度表示Text
  [SerializeField]
  private Text longitude;//経度表示Text


  #region Feature variables
  private List<Feature> _features = new List<Feature>();
  ForwardGeocodeResource _resource;
  private string _searchInput;
	#endregion

	[SerializeField]
	private InputField inputField;

	private AbstractMap _map;

	private Vector2d coorded_spot = new Vector2d(0, 0);

	private bool seted = false;

  void OnEnable()
  {
    _resource = new ForwardGeocodeResource("");
  }

	public void InputWord()
	{
		string value = inputField.text;
		func(value);
    
	}

	public void SetMapCenter()
	{
		

		Debug.Log("coorded : " + coorded_spot);
		if (seted)
		{
			Debug.Log("now LL : " + _map.CenterLatitudeLongitude);
			//_map.CenterLatitudeLongitude = coorded_spot;
			_map.SetCenterLatitudeLongitude(coorded_spot);
			_map.UpdateMap(_map.CenterLatitudeLongitude, _map.Zoom);
		}
	}


  void Start()
  {
		_map = GameObject.Find("Map").GetComponent<AbstractMap>();

		CallGetData();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.A))
		{
			
      /*
      var map = obj.GetComponent < AbstractMap >();
      Debug.Log(map.CenterLatitudeLongitude);
      map.CenterLatitudeLongitude = new Vector2d(35.45881, 139.62547);
      */
      //------------------------------
    }

    if (Input.GetKeyDown(KeyCode.Q))
    {
      var str = "yokohama";
      func(str);
    }
    else if (Input.GetKeyDown(KeyCode.W))
    {
      var str = "umeda";
      func(str);
    }
  }

  void HandleUserInput(string searchString)
  {
    _features = new List<Feature>();
    if (!string.IsNullOrEmpty(searchString))
    {
      _resource.Query = searchString;
      MapboxAccess.Instance.Geocoder.Geocode(_resource, HandleGeocoderResponse);
    }
  }


  void HandleGeocoderResponse(ForwardGeocodeResponse res)
  {
    if (res != null)
    {
      if (res.Features != null)
      {
        _features = res.Features;//参照getter
        foreach (var f in _features)
        {
					//string truncatedCoordinates = f.Center.x.ToString("F2", CultureInfo.InvariantCulture) + ", " +
              //f.Center.y.ToString("F2", CultureInfo.InvariantCulture);


          Vector2d coord = new Vector2d(f.Center.y, f.Center.x);
					//string[] featureNameSplit = f.PlaceName.Split(‘,’);//address

					coorded_spot = coord;
					Debug.Log("spot" + coorded_spot);
     
          string cityAddress = f.PlaceName;
          Debug.Log(cityAddress);
					seted = true;
        }
      }
    }
  }

  void func(string searchWord)
  {
    var oldSearch = _searchInput;
    _searchInput = searchWord;

    var isWordChange = false;
    if (_searchInput.Length != 0)
    {
      isWordChange = (_searchInput != oldSearch) ? true : false;
      if (isWordChange) HandleUserInput(_searchInput);
    }
  }


  //----- 検索が実行できない場合に備えて，GPSデータを取得 -----
	void CallGetData()
  {
    StartCoroutine("GetData");
  }


  /* ---------- GetData ----------
   * GPSのデータを取得するコルーチン
   */
  IEnumerator GetData()
  {
    Debug.Log("GPS情報参照開始");

    //--- 端末のGPS機能が使えないとき ---
    if (!Input.location.isEnabledByUser)
    {
      Debug.Log("GPS is cannot use.");
      latitude.text = "GPSをONにしてね";
      longitude.text = "GPSをONにしてね";

      yield break;
    }


    Input.location.Start();
    int maxWait = 3;
    while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
    {
      yield return new WaitForSeconds(1);
      maxWait--;
    }
    if (maxWait < 1)
    {
      Debug.Log("Timed out");
      yield break;
    }
    if (Input.location.status == LocationServiceStatus.Failed)
    {
      Debug.Log("Unable to determine device location");
      latitude.text = "GPS情報を取得できません";
      longitude.text = "GPS情報を取得できません";

      yield break;
    }
    else
    {

      latitude.text = "緯度 : " + Input.location.lastData.latitude;
      longitude.text = "経度 : " + Input.location.lastData.longitude;

      //--- 地図の中心に緯度経度を指定 ---
      Vector2d pos = new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude);	

     //_map.CenterLatitudeLongitude = pos;
            _map.UpdateMap(pos, _map.Zoom);

      Debug.Log("Location: " +
      Input.location.lastData.latitude + " " +
      Input.location.lastData.longitude + " " +
      Input.location.lastData.altitude + " " +
      Input.location.lastData.horizontalAccuracy + " " +
      Input.location.lastData.timestamp);
    }
    Input.location.Stop();
  }
}