using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace TestFairyUnity
{
	public class TestFairy : MonoBehaviour
	{
		public static void begin(string appToken) {
			TestFairy.installUnityCrashHandler();
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_begin(appToken);
#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaObject activityContext = null;
			using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			TestFairy.callMethod("begin", activityContext, appToken);
#endif
		}

		public static void installFeedbackHandler(string appToken) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_installFeedbackHandler(appToken, "shake|screenshot");
#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaObject activityContext = null;
			using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			TestFairy.callMethod("installFeedbackHandler", activityContext, appToken);
#endif
		}
		public static void setScreenName(string name) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_setScreenName(name);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("setScreenName", name);
#endif
		}

		public static void setUserId(string userId) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_setUserId(userId);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("setUserId", userId);
#endif
		}

		public static bool setAttribute(string aKey, string aValue) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
			return false;
#elif UNITY_IPHONE && !UNITY_EDITOR
			return TestFairy_setAttribute(aKey, aValue);
#elif UNITY_ANDROID && !UNITY_EDITOR
			return TestFairy.callBoolMethod("setAttribute", aKey, aValue);
#endif
		}

		/// <summary>
		/// Push the feedback view controller. Hook a button to this method
		/// to allow users to provide feedback about the current session. All
		/// feedback will appear in your build report page, and in the
		/// recorded session page.
		/// </summary>
		public static void pushFeedbackController() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_pushFeedbackController();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("showFeedbackForm");
#endif
		}

		public static void showFeedbackForm(string appToken, bool takeScreenshot) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_showFeedbackForm(appToken, takeScreenshot);
#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaObject activityContext = null;
			using(AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			TestFairy.callMethod("showFeedbackForm", activityContext, appToken, takeScreenshot);
#endif
		}

		/// <summary>
		/// Mark a checkpoint in session. Use this text to tag a session
		/// with a checkpoint name. Later you can filter sessions where your
		/// user passed through this checkpoint, for bettering understanding
		/// user experience and behavior.
		/// </summary>
		/// <param name="name">Name of checkpoint, make it short.</param>
		public static void checkpoint(string name) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_checkpoint(name);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("addCheckpoint", name);
#endif
		}

		/// <summary>
		/// Sets a correlation identifier for this session. This value can
		/// be looked up via web dashboard. For example, setting correlation
		/// to the value of the user-id after they logged in. Can be called
		/// only once per session (subsequent calls will be ignored.)
		/// </summary>
		/// <param name="correlationId">Correlation value</param>
		public static void setCorrelationId(string correlationId) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_setCorrelationId(correlationId);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("setCorrelationId", correlationId);
#endif
		}

		/// <summary>
		/// Sets a correlation identifier for this session. This value can
		/// be looked up via web dashboard. For example, setting correlation
		/// to the value of the user-id after they logged in. Can be called
		/// only once per session (subsequent calls will be ignored.)
		/// </summary>
		/// <param name="correlationId">Correlation value</param>
		public static void identify(string correlationId, Dictionary<string, object> traits = null) {
			string traitsString = null;
			if (traits != null) {
				traitsString = "";
				foreach(KeyValuePair<string, object> kvp in traits)
				{
					string encodedKey = UnityWebRequest.EscapeURL(kvp.Key);
					string encodedValue = UnityWebRequest.EscapeURL(kvp.Value.ToString());
					string type = kvp.Value.GetType().ToString();
					traitsString += encodedKey + "=" + type + "/" + encodedValue + "\n";
				}
			}
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_identify(correlationId, traitsString);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("identify", correlationId, traitsString);
#endif
		}

		/// <summary>
		/// Pauses the current session. This method stops recoding of
		/// the current session until Resume() has been called.
		/// </summary>
		public static void pause() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_pause();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("pause");
#endif
		}

		/// <summary>
		/// Resumes the recording of the current session. This method
		/// resumes a session after it was paused or stopped.
		/// </summary>
		public static void resume() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_resume();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("resume");
#endif
		}

		/// <summary>
		/// Stops the recording of the current session.
		/// </summary>
		public static void stop() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_stop();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("stop");
#endif
		}

		/// <summary>
		/// Returns the url of the current session while its being recorded.
		/// Will return null if session hasn't started yet.
		/// </summary>
		/// <returns>The session URL.</returns>
		public static string sessionUrl() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
			return null;
#elif UNITY_IPHONE && !UNITY_EDITOR
			return TestFairy_sessionUrl();
#elif UNITY_ANDROID && !UNITY_EDITOR
			string sessionUrlStr = null;
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					sessionUrlStr = pluginClass.CallStatic<string>("getSessionUrl");
				}
			}
			return sessionUrlStr;
#endif
		}

		/// <summary>
		/// Returns the current installed version of TestFairy SDK.
		/// </summary>
		/// <returns>TestFairy version string.</returns>
		public static string version() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
			return null;
#elif UNITY_IPHONE && !UNITY_EDITOR
			return TestFairy_version();
#elif UNITY_ANDROID && !UNITY_EDITOR
			string versionStr = null;
			using(AndroidJavaClass pluginClass = getTestFairyClass()) {
				if(pluginClass != null) {
					versionStr = pluginClass.CallStatic<string>("getVersion");
				}
			}
			return versionStr;
#endif
		}

		/// <summary>
		/// Sends a feedback string to TestFairy
		/// </summary>
		public static void sendUserFeedback(string feedback) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_sendUserFeedback(feedback);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("sendUserFeedback", feedback);
#endif
		}

		/// <summary>
		/// Takes a screenshot
		/// </summary>
		public static void takeScreenshot() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_takeScreenshot();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("takeScreenshot");
#endif
		}

		public static void log(string message) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_log(message);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("log", "TestFairyUnity", message);
#endif
		}

		public static void logException(string message, string stacktrace) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_logException(message, stacktrace);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("logThrowable", stacktrace);
#endif
		}

		public static void hideWebViewElements(string cssSelector) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_hideWebViewElements(cssSelector);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("hideWebViewElements", cssSelector);
#endif
		}

		public static void disableVideo() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_disableVideo();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("disableVideo");
#endif
		}

		public static void enableVideo(string policy, string quality, float framesPerSecond) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_enableVideo(policy, quality, framesPerSecond);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("enableVideo", policy, quality, framesPerSecond);
#endif
		}

		public static void enableCrashHandler() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_enableCrashHandler();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("enableCrashHandler");
#endif
		}

		public static void disableCrashHandler() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_disableCrashHandler();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("disableCrashHandler");
#endif
		}

		public static void enableMetric(string metric) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_enableMetric(metric);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("disableCrashHandler");
#endif
		}

		public static void disableMetric(string metric) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_disableMetric(metric);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("disableMetric", metric);
#endif
		}

		public static void enableFeedbackForm(string method) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_enableFeedbackForm(method);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("enableFeedbackForm");
#endif
		}

		public static void disableFeedbackForm() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_disableFeedbackForm();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("disableFeedbackForm");
#endif
		}

		public static void disableAutoUpdate() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_disableAutoUpdate();
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("disableAutoUpdate");
#endif
		}

		public static void setServerEndpoint(string endpoint) {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_setServerEndpoint(endpoint);
#elif UNITY_ANDROID && !UNITY_EDITOR
			TestFairy.callMethod("setServerEndpoint", endpoint);
#endif
		}

		public static void crash() {
#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#elif UNITY_IPHONE && !UNITY_EDITOR
			TestFairy_crash();
#elif UNITY_ANDROID && !UNITY_EDITOR
			// TestFairy.callMethod("crash");
#endif
		}

		private static void installUnityCrashHandler() {
			Application.logMessageReceivedThreaded += TestFairy.logMsgRecvThreaded;
		}

		private static void logMsgRecvThreaded(string _condition, string _stackTrace, LogType _type) {
			if (_type==LogType.Exception) {
				TestFairy.logException(_condition, _stackTrace);
			}
		}
#if UNITY_ANDROID && !UNITY_EDITOR
		void Start () {
			AndroidJNI.AttachCurrentThread();
		}

		private static AndroidJavaClass getTestFairyClass() {
			return new AndroidJavaClass("com.testfairy.TestFairy");
		}

		private static void callMethod(string methodName, params object[] args) {
			using (AndroidJavaClass pluginClass = getTestFairyClass()) {
				if (pluginClass != null) {
					if (args.Length == 0) {
						pluginClass.CallStatic(methodName);
					} else if (args.Length == 1) {
						pluginClass.CallStatic(methodName, args[0]);
					} else if (args.Length == 2) {
						pluginClass.CallStatic(methodName, args[0], args[1]);
					} else if (args.Length == 3) {
						pluginClass.CallStatic(methodName, args[0], args[1], args[2]);
					}
				}
			}
		}

		private static bool callBoolMethod(string methodName, params object[] args) {
			using (AndroidJavaClass pluginClass = getTestFairyClass()) {
				if (pluginClass != null) {
					if (args.Length == 0) {
						return pluginClass.CallStatic<bool>(methodName);
					} else if (args.Length == 1) {
						return pluginClass.CallStatic<bool>(methodName, args[0]);
					} else if (args.Length == 2) {
						return pluginClass.CallStatic<bool>(methodName, args[0], args[1]);
					} else if (args.Length == 3) {
						return pluginClass.CallStatic<bool>(methodName, args[0], args[1], args[2]);
					}
				}
			}

			return false;
		}
#elif UNITY_IPHONE && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void TestFairy_begin(string APIKey);

		[DllImport("__Internal")]
		private static extern void TestFairy_installFeedbackHandler(string appToken, string method);

		[DllImport("__Internal")]
		private static extern void TestFairy_pushFeedbackController();

		[DllImport("__Internal")]
		private static extern void TestFairy_showFeedbackForm(string appToken, bool takeScreenshot);

		[DllImport("__Internal")]
		private static extern void TestFairy_checkpoint(string name);

		[DllImport("__Internal")]
		private static extern void TestFairy_setServerEndpoint(string endpoint);

		[DllImport("__Internal")]
		private static extern void TestFairy_setCorrelationId(string correlationId);

		[DllImport("__Internal")]
		private static extern void TestFairy_identify(string correlationId, string traits);

		[DllImport("__Internal")]
		private static extern void TestFairy_pause();

		[DllImport("__Internal")]
		private static extern void TestFairy_resume();

		[DllImport("__Internal")]
		private static extern void TestFairy_stop();

		[DllImport("__Internal")]
		private static extern string TestFairy_sessionUrl();

		[DllImport("__Internal")]
		private static extern string TestFairy_version();

		[DllImport("__Internal")]
		private static extern void TestFairy_sendUserFeedback(string feedback);

		[DllImport("__Internal")]
		private static extern void TestFairy_takeScreenshot();

		[DllImport("__Internal")]
		private static extern void TestFairy_setScreenName(string name);

		[DllImport("__Internal")]
		private static extern void TestFairy_log(string name);

		[DllImport("__Internal")]
		private static extern void TestFairy_logException(string name, string trace);

		[DllImport("__Internal")]
		private static extern void TestFairy_hideWebViewElements(string cssSelector);

		[DllImport("__Internal")]
		private static extern void TestFairy_setUserId(string userId);

		[DllImport("__Internal")]
		private static extern bool TestFairy_setAttribute(string aKey, string aValue);

		[DllImport("__Internal")]
		private static extern void TestFairy_enableCrashHandler();

		[DllImport("__Internal")]
		private static extern void TestFairy_disableCrashHandler();

		[DllImport("__Internal")]
		private static extern void TestFairy_enableFeedbackForm(string method);

		[DllImport("__Internal")]
		private static extern void TestFairy_disableFeedbackForm();

		[DllImport("__Internal")]
		private static extern void TestFairy_enableMetric(string metric);

		[DllImport("__Internal")]
		private static extern void TestFairy_disableMetric(string metric);

		[DllImport("__Internal")]
		private static extern void TestFairy_disableAutoUpdate();

		[DllImport("__Internal")]
		private static extern void TestFairy_disableVideo();

		[DllImport("__Internal")]
		private static extern void TestFairy_enableVideo(string policy, string quality, float framesPerSecond);

		[DllImport("__Internal")]
		private static extern void TestFairy_crash();
#endif
	}
}
