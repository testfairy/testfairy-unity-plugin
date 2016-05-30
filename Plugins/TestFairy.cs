using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace TestFairyUnity
{
	public class TestFairy : MonoBehaviour
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void TestFairy_begin(string APIKey);

		[DllImport("__Internal")]
		private static extern void TestFairy_pushFeedbackController();

		[DllImport("__Internal")]
		private static extern void TestFairy_checkpoint(string name);

		[DllImport("__Internal")]
		private static extern void TestFairy_setCorrelationId(string correlationId);

		[DllImport("__Internal")]
		private static extern void TestFairy_identify(string correlationId, string traits);

		[DllImport("__Internal")]
		private static extern void TestFairy_pause();

		[DllImport("__Internal")]
		private static extern void TestFairy_resume();

		[DllImport("__Internal")]
		private static extern string TestFairy_sessionUrl();

		[DllImport("__Internal")]
		private static extern string TestFairy_version();

		[DllImport("__Internal")]
		private static extern void TestFairy_sendUserFeedback(string feedback);

		[DllImport("__Internal")]
		private static extern void TestFairy_takeScreenshot();

#elif UNITY_ANDROID && !UNITY_EDITOR
		void Start () {
			AndroidJNI.AttachCurrentThread();
		}

		private static AndroidJavaClass getTestFairyClass() {
			return new AndroidJavaClass("com.testfairy.unity.TestFairyBridge");
		}
#endif

		/// <summary>
		/// Initialize a TestFairy session.
		/// </summary>
		/// <param name="APIKey"></param>	
		public static void begin(string APIKey)
		{
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_begin(APIKey);
#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaObject activityContext = null;
			using(AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
			}

			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					pluginClass.CallStatic("begin", activityContext, APIKey);
				}
			}
#endif
		}

		/// <summary>
		/// Push the feedback view controller. Hook a button to this method
		/// to allow users to provide feedback about the current session. All
		/// feedback will appear in your build report page, and in the
		/// recorded session page.
		/// </summary>
		public static void pushFeedbackController()
		{
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_pushFeedbackController();
#elif UNITY_ANDROID && !UNITY_EDITOR
			// TODO: No-op on Android
#endif
		}

		/// <summary>
		/// Mark a checkpoint in session. Use this text to tag a session
		/// with a checkpoint name. Later you can filter sessions where your
		/// user passed through this checkpoint, for bettering understanding
		/// user experience and behavior.
		/// </summary>
		/// <param name="name">Name of checkpoint, make it short.</param>
		public static void checkpoint(string name)
		{
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_checkpoint(name);
#elif UNITY_ANDROID && !UNITY_EDITOR
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					pluginClass.CallStatic("addCheckpoint", name);
				}
			}
#endif
		}

		/// <summary>
		/// Sets a correlation identifier for this session. This value can
		/// be looked up via web dashboard. For example, setting correlation
		/// to the value of the user-id after they logged in. Can be called
		/// only once per session (subsequent calls will be ignored.)
		/// </summary>
		/// <param name="correlationId">Correlation value</param>
		public static void setCorrelationId(string correlationId)
		{
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_setCorrelationId(correlationId);
#elif UNITY_ANDROID && !UNITY_EDITOR
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					pluginClass.CallStatic("setCorrelationId", correlationId);
				}
			}
#endif
		}

		/// <summary>
		/// Sets a correlation identifier for this session. This value can
		/// be looked up via web dashboard. For example, setting correlation
		/// to the value of the user-id after they logged in. Can be called
		/// only once per session (subsequent calls will be ignored.)
		/// </summary>
		/// <param name="correlationId">Correlation value</param>
		public static void identify(string correlationId, Dictionary<string, object> traits = null)
		{
			string traitsString = null;
			if (traits != null) {
				traitsString = "";
				foreach(KeyValuePair<string, object> kvp in traits)
				{
					string encodedKey = WWW.EscapeURL(kvp.Key);
					string encodedValue = WWW.EscapeURL(kvp.Value.ToString());
					string type = kvp.Value.GetType().ToString();
					traitsString += encodedKey + "=" + type + "/" + encodedValue + "\n";
				}
			}
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_identify(correlationId, traitsString);
#elif UNITY_ANDROID && !UNITY_EDITOR
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					pluginClass.CallStatic("identify", correlationId, traitsString);
				}
			}
#endif
		}
		
		/// <summary>
		/// Pauses the current session. This method stops recoding of 
		/// the current session until Resume() has been called.
		/// </summary>
		public static void pause()
		{
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_pause();
#elif UNITY_ANDROID && !UNITY_EDITOR
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					pluginClass.CallStatic("pause");
				}
			}
#endif
		}

		/// <summary>
		/// Resumes the recording of the current session. This method
		/// resumes a session after it was paused.
		/// </summary>
		public static void resume()
		{
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_resume();
#elif UNITY_ANDROID && !UNITY_EDITOR
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					pluginClass.CallStatic("resume");
				}
			}
#endif
		}

		/// <summary>
		/// Returns the url of the current session while its being recorded.
		/// Will return null if session hasn't started yet.
		/// </summary>
		/// <returns>The session URL.</returns>
		public static string sessionUrl()
		{
			string sessionUrl = null;
#if UNITY_IPHONE && !UNITY_EDITOR
			sessionUrl = TestFairy_sessionUrl();
#elif UNITY_ANDROID && !UNITY_EDITOR
			// TODO: no-op on android
#endif
			return sessionUrl;
		}

		/// <summary>
		/// Returns the current installed version of TestFairy SDK.
		/// </summary>
		/// <returns>TestFairy version string.</returns>
		public static string version()
		{
			string version = null;
#if UNITY_IPHONE && !UNITY_EDITOR
			version = TestFairy_version ();
#elif UNITY_ANDROID && !UNITY_EDITOR
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					version = pluginClass.CallStatic<string>("getVersion");
				}
			}
#endif
			return version;
		}
		
		/// <summary>
		/// Sends a feedback to TestFairy
		/// </summary>
		/// <returns>Feedback string.</returns>
		public static void sendUserFeedback(string feedback)
		{
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_sendUserFeedback(feedback);
#elif UNITY_ANDROID && !UNITY_EDITOR
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					pluginClass.CallStatic("sendUserFeedback", feedback);
				}
			}
#endif
		}

		/// <summary>
		/// Takes a screenshot
		/// </summary>
		public static void takeScreenshot()
		{
#if UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_takeScreenshot();
#elif UNITY_ANDROID && !UNITY_EDITOR
			// TODO: no-op on android
#endif
		}
	}
}
