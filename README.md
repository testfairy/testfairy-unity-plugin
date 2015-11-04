# TestFairy plugin for Unity

1. Download the latest [TestFairy plugin for Unity](https://app.testfairy.com/sdk/ios/download/latest/unity/).

2. Unpack the zip on your disk.
 
3. Drag **TestFairy.cs** and **iOS** into your Project under `Plugins`. If you don't have Plugins, you can drag the entire folder onto your project.

  ![Step 1](/Images/step1.png)
  
4. Open `mainCamera` in Inspector by clicking on it, and then click on `Add Component`. Note: you can add TestFairy script to any game object. TestFairy is a singleton so no harm is done.

  ![Step 2](/Images/step2.png)
  
5. Type in the name of the script, for example `mainCameraScript`, choose `CSharp` and click on `Create and Add`.

  ![Step 3](/Images/step3.png)
  
6. Edit the newly created CSharp script, and add `using TestFairyUnity;` to the import section, and a call to `TestFairy.begin()` with your app token. You can find your app token in  [Account Settings](https://app.testfairy.com/settings/#apptoken) page.

  ![Step 4](/Images/step4.png)
  
7. Extra permissions are required for Android instrumentation. You can copy a version of your AndroidManifest.xml from `<root>/Temp/StagingArea/AndroidManifest.xml` into `<root>/Assets/Plugin/Android` directory. From here, edit `AndroidManifest.xml` with the following lines
```
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE"/>
<uses-permission android:name="android.permission.BATTERY_STATS"/>
<uses-permission android:name="android.permission.GET_ACCOUNTS"/>
<uses-permission android:name="android.permission.RECORD_AUDIO"/>
<uses-permission android:name="android.permission.READ_PHONE_STATE"/>
<uses-permission android:name="com.google.android.providers.gsf.permission.READ_GSERVICES"/>

<uses-feature android:name="android.hardware.camera"/>
<uses-permission android:name="android.permission.CAMERA"/>
<uses-feature android:glEsVersion="0x00020000" android:required="true"/>

<!-- gps perms -->
<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION"/>
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION"/>
<uses-permission android:name="android.permission.GET_TASKS"/>
<uses-permission android:name="android.permission.READ_LOGS"/>
```

8. Save, build and run.
