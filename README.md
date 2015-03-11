# TestFairy plugin for Unity

1. Download this git repository: [master.zip](https://github.com/testfairy/testfairy-unity-plugin/archive/master.zip)

2. Unpack the zip on your disk.
 
3. Drag **TestFairy.cs** and **iOS** into your Project under `Plugins`. If you don't have Plugins, you can drag the entire folder onto your project.

  ![Step 1](/Images/step1.png)
  
4. Open `mainCamera` in Inspector by clicking on it, and then click on `Add Component`. Note: you can add TestFairy script to any game object. TestFairy is a singleton so no harm is done.

  ![Step 2](/Images/step2.png)
  
5. Type in the name of the script, for example `mainCameraScript`, choose `CSharp` and click on `Create and Add`.

  ![Step 3](/Images/step3.png)
  
6. Edit the newly created CSharp script, and add `using TestFairyUnity;` to the import section, and a call to `TestFairy.begin()` with your app token. You can find your app token in  [Account Settings](https://app.testfairy.com/settings/#apptoken) page.

  ![Step 4](/Images/step4.png)
  
Save, build and run.
