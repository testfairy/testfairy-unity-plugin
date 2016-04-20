package com.testfairy.unity;

import android.content.Context;
import android.content.pm.PackageManager;
import android.location.Location;
import android.util.Log;
import android.view.View;

import java.util.Map;
import java.net.URLDecoder;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Locale;

import com.testfairy.TestFairy;

public class TestFairyBridge {
	private static final DateFormat DATE_FORMAT = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss", Locale.getDefault());

	public static void begin(Context context, String appToken) {
		TestFairy.begin(context, appToken);
	}

	public static String getVersion() {
		return TestFairy.getVersion();
	}

	public static void sendUserFeedback(String feedback) {
		TestFairy.sendUserFeedback(feedback);
	}

	public static void addCheckpoint(String name) {
		TestFairy.addCheckpoint(name);
	}

	public static void setCorrelationId(String correlationId) {
		TestFairy.setCorrelationId(correlationId);
	}

	public static void identify(String correlationId, String traits) {
		if (traits == null) {
			TestFairy.identify(correlationId);
			return;
		}

		HashMap<String, Object> identityTraits = toHashMap(traits);
		TestFairy.identify(correlationId, identityTraits);
	}

	public static void resume() {
		TestFairy.resume();
	}

	public static void pause() {
		TestFairy.pause();
	}


	public static HashMap<String, Object> toHashMap(String traits) {
		HashMap<String, Object> identityTraits = new HashMap<String, Object>();
		for (String attribute : traits.split("\n")) {
			int equalDelimiter = attribute.indexOf("=");
			if (equalDelimiter == -1) {
				Log.d("TestFairy", "Unusable attribute " + attribute);
				continue;
			}

			try {
				String unescapedKey = attribute.substring(0, equalDelimiter);
				String key = URLDecoder.decode(unescapedKey, "UTF-8");
				String valueProperty = attribute.substring(equalDelimiter + 1, attribute.length());
				int typeDelimiter = valueProperty.indexOf("/");
				if (typeDelimiter == -1) {
					Log.d("TestFairy", "Unusable attribute " + attribute);
					continue;
				}

				String type = valueProperty.substring(0, typeDelimiter);
				String unescapedValue = valueProperty.substring(typeDelimiter + 1, valueProperty.length());
				String escapedValue = URLDecoder.decode(unescapedValue, "UTF-8");

				if ("System.Double".equals(type)) {
					Double value = Double.parseDouble(escapedValue);
					identityTraits.put(key, value);
				} else if ("System.Single".equals(type)) {
					Float value = Float.parseFloat(escapedValue);
					identityTraits.put(key, value);
				} else if ("System.Int32".equals(type)) {
					Integer value = Integer.parseInt(escapedValue);
					identityTraits.put(key, value);
				} else if ("System.String".equals(type)) {
					identityTraits.put(key, escapedValue);
				} else if ("System.DateTime".equals(type)) {
					Date value = DATE_FORMAT.parse(escapedValue);
					identityTraits.put(key, value);
				} else {
					Log.d("TestFairy", "Unsupported trait type " + type + ". Ignoring");
				}
			} catch (Exception exception) {
				Log.d("TestFairy", "Failed to add attribute " + attribute, exception);
			}
		}

		return identityTraits;
	}
}