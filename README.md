# TestFairy plugin for Unity

1. Download the latest [TestFairy plugin for Unity](https://app.testfairy.com/sdk/ios/download/latest/unity/).

2. Unpack the zip on your disk.
 
3. Drag **TestFairy.cs**, **iOS** and **Android** into your Project under `Assets/Plugins`. If you don't have Plugins, you can drag the entire folder onto your project.

  ![Step 1](/Images/step1.png)
  
4. Open `mainCamera` in Inspector by clicking on it, and then click on `Add Component`. Note: you can add TestFairy script to any game object. TestFairy is a singleton so no harm is done.

  ![Step 2](/Images/step2.png)
  
5. Type in the name of the script, for example `mainCameraScript`, choose `CSharp` and click on `Create and Add`.

  ![Step 3](/Images/step3.png)
  
6. Edit the newly created CSharp script, and add `using TestFairyUnity;` to the import section, and a call to `TestFairy.begin()` with your app token. You can find your app token in  [Account Settings](https://app.testfairy.com/settings/#apptoken) page.

  ![Step 4](/Images/step4.png)

```
using UnityEngine;
using System.Collections;
using TestFairyUnity;

public class mainCameraScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
        TestFairy.begin("0ddd54741fc830787fb8e1a8232a49733ce9759b");
    }

    ...
}
```
  
7. At minimum, TestFairy requires the `INTERNET` and `ACCESS_NETWORK_STATE` permission for your Android build. You can copy a version of your AndroidManifest.xml from `<root>/Temp/StagingArea/AndroidManifest.xml` into `<root>/Assets/Plugin/Android` directory. From here, edit `AndroidManifest.xml` with the following line

```xml
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>
```

Additional features may require extra persmissions given below

```xml
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE"/>
<uses-permission android:name="android.permission.BATTERY_STATS"/>
<uses-permission android:name="android.permission.READ_PHONE_STATE"/>

<uses-permission android:name="android.permission.GET_TASKS"/>
<uses-permission android:name="android.permission.READ_LOGS"/>
```

8. Save, build and run.
