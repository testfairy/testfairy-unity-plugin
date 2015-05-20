using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace TestFairyUnity
{
	public class TestFairy : MonoBehaviour
	{
		#if UNITY_IPHONE		
		
		[DllImport("__Internal")]
		private static extern void TestFairy_begin(string APIKey);

		/// <summary>
		/// Initialize a TestFairy session.
		/// </summary>
		/// <param name="APIKey"></param>
		public static void begin(string APIKey)
		{
			TestFairy_begin(APIKey);
		}

		[DllImport("__Internal")]
		private static extern void TestFairy_pushFeedbackController();

		/// <summary>
		/// Push the feedback view controller. Hook a button to this method
		/// to allow users to provide feedback about the current session. All
		/// feedback will appear in your build report page, and in the
		/// recorded session page.
		/// </summary>
		public static void pushFeedbackController()
		{
			TestFairy_pushFeedbackController();
		}

		[DllImport("__Internal")]
		private static extern void TestFairy_checkpoint(string name);

		/// <summary>
		/// Mark a checkpoint in session. Use this text to tag a session
		/// with a checkpoint name. Later you can filter sessions where your
		/// user passed through this checkpoint, for bettering understanding
		/// user experience and behavior.
		/// </summary>
		/// <param name="name">Name of checkpoint, make it short.</param>
		public static void checkpoint(string name)
		{
			TestFairy_checkpoint(name);
		}
		
		[DllImport("__Internal")]
		private static extern void TestFairy_setCorrelationId(string correlationId);

		/// <summary>
		/// Sets a correlation identifier for this session. This value can
		/// be looked up via web dashboard. For example, setting correlation
		/// to the value of the user-id after they logged in. Can be called
		/// only once per session (subsequent calls will be ignored.)
		/// </summary>
		/// <param name="correlationId">Correlation value</param>
		public static void setCorrelationId(string correlationId)
		{
			TestFairy_setCorrelationId(correlationId);
		}

		[DllImport("__Internal")]
		private static extern void TestFairy_pause();

		/// <summary>
		/// Pauses the current session. This method stops recoding of 
		/// the current session until Resume() has been called.
		/// </summary>
		public static void pause()
		{
			TestFairy_pause();
		}

		[DllImport("__Internal")]
		private static extern void TestFairy_resume();

		/// <summary>
		/// Resumes the recording of the current session. This method
		/// resumes a session after it was paused.
		/// </summary>
		public static void resume()
		{
			TestFairy_resume();
		}

		[DllImport("__Internal")]
		private static extern string TestFairy_sessionUrl();

		/// <summary>
		/// Returns the url of the current session while its being recorded.
		/// Will return null if session hasn't started yet.
		/// </summary>
		/// <returns>The session URL.</returns>
		public static string sessionUrl()
		{
			string sessionUrl = TestFairy_sessionUrl ();
			return sessionUrl;
		}

		[DllImport("__Internal")]
		private static extern string TestFairy_version();
		
		/// <summary>
		/// Returns the current installed version of TestFairy SDK.
		/// </summary>
		/// <returns>TestFairy version string.</returns>
		public static string version()
		{
			string version = TestFairy_version ();
			return version;
		}

		[DllImport("__Internal")]
		private static extern void TestFairy_sendUserFeedback(string feedback);
		
		/// <summary>
		/// Sends a feedback to TestFairy
		/// </summary>
		/// <returns>Feedback string.</returns>
		public static void sendUserFeedback(string feedback)
		{
			TestFairy_sendUserFeedback(feedback);
		}

		[DllImport("__Internal")]
		private static extern void TestFairy_takeScreenshot();
		
		/// <summary>
		/// Takes a screenshot
		/// </summary>
		public static void takeScreenshot()
		{
			TestFairy_takeScreenshot ();
		}

		#elif UNITY_ANDROID
		
		#endif
	}
}
