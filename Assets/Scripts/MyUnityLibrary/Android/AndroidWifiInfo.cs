 using MyUnityLibrary.Patterns;
 using UnityEngine;

 namespace MyUnityLibrary.Android
 {
     /// <summary>
     /// Assets/Plugins/Android에 AndroidManifest.xml을 별도로 생성하고 아래와 같이 와이파이 상태 권한을 명시 해야함.
     /// <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
     /// </summary>
     public class AndroidWifiInfo : MonoSingleton<AndroidWifiInfo>
     {   
         [SerializeField] private float _frequencyTime = 1f;
         [SerializeField] private int _numLevels = 4;

         private float _elapsedTime;

         public string NetworkName { private set; get; }
         public int SignalLevel { private set; get; }

#if UNITY_ANDROID 
        private void Start()
        {
            _elapsedTime = _frequencyTime;
        }

        private void Update()
        {
            if (_elapsedTime >= _frequencyTime)
            {
                _elapsedTime = 0f;

                NetworkName = GetNetworkName().Trim('\"');
                SignalLevel = GetSignalLevel(_numLevels);
            }
            
            _elapsedTime += Time.deltaTime;
        }
#endif

         private static string GetNetworkName()
         {
             if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
             {
                 return "Cellular";
             }
        
             if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
             {

                 using (var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
                 {
                     var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
                     return wifiManager.Call<AndroidJavaObject>("getConnectionInfo").Call<string>("getSSID");
                 }
             }

             return "N/A";
         }

         private static int GetSignalLevel(int numLevels)
         {
             using (var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
             {
                 var wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
                 var wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
                 int rssi = wifiInfo.Call<int>("getRssi");
                 return wifiManager.CallStatic<int>("calculateSignalLevel", rssi, numLevels);
             }
         }
     }
 }
