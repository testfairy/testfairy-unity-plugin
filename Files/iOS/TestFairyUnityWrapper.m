#import "TestFairy.h"
#import "TestFairyUnityWrapper.h"

typedef void (*TFNativeScreenshotCallback)(
	const char* identifier,
	const char* imagePath,
	int width,
	int height
);

void TestFairy_begin(char *APIKey)
{
	NSString *value = APIKey == NULL ? @"" : [NSString stringWithUTF8String:APIKey];
	[TestFairy begin:value];
}

void TestFairy_pushFeedbackController()
{
	[TestFairy pushFeedbackController];
}

void TestFairy_showFeedbackForm(char *appToken, BOOL takeScreenshot)
{
	NSString *value = appToken == NULL ? @"" : [NSString stringWithUTF8String:appToken];
	[TestFairy showFeedbackForm:value takeScreenshot:takeScreenshot];
}

void TestFairy_checkpoint(char *name)
{
	NSString *value = name == NULL ? @"" : [NSString stringWithUTF8String:name];
	[TestFairy checkpoint:value];
}

void TestFairy_setServerEndpoint(char *serverOverride)
{
	NSString *value = serverOverride == NULL ? nil : [NSString stringWithUTF8String:serverOverride];
	[TestFairy setServerEndpoint:value];
}

void TestFairy_setCorrelationId(char *correlationId)
{
	NSString *value = correlationId == NULL ? @"" : [NSString stringWithUTF8String:correlationId];
	[TestFairy setCorrelationId:value];
}

void TestFairy_identify(char *correlationId, char *traits)
{
	NSString *coId = correlationId == NULL ? @"" : [NSString stringWithUTF8String:correlationId];
	if (traits == NULL) {
		[TestFairy identify:coId];
	} else {
		NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
		[dateFormatter setDateFormat:@"MM-dd-yyyy HH:mm:ss"];
		[dateFormatter setLocale:[NSLocale currentLocale]];

		NSString *attris = [NSString stringWithUTF8String:traits];
		NSArray *attributesArray = [attris componentsSeparatedByString:@"\n"];

		NSMutableDictionary *identityTraits = [[NSMutableDictionary alloc] init];
		for (int i = 0; i < [attributesArray count]; i++) {
			NSString *keyValuePair = [attributesArray objectAtIndex:i];
			NSRange range = [keyValuePair rangeOfString:@"="];
			if (range.location != NSNotFound) {
				NSString *unescapedKey = [keyValuePair substringToIndex:range.location];
				NSString *key =[[unescapedKey stringByReplacingOccurrencesOfString:@"+" withString:@" "] stringByReplacingPercentEscapesUsingEncoding:NSUTF8StringEncoding];

				NSString *valueProperty = [keyValuePair substringFromIndex:range.location+1];
				NSRange position = [valueProperty rangeOfString:@"/"];
				if (position.location != NSNotFound) {
					NSString *type = [valueProperty substringToIndex:position.location];
					NSString *unescapedValue = [valueProperty substringFromIndex:position.location + 1];
					NSString *escapedValue = [[unescapedValue stringByReplacingOccurrencesOfString:@"+" withString:@" "] stringByReplacingPercentEscapesUsingEncoding:NSUTF8StringEncoding];
					if ([@"System.Double" isEqualToString:type]) {
						double value = [escapedValue doubleValue];
						[identityTraits setObject:[NSNumber numberWithDouble:value] forKey:key];
					} else if ([@"System.Single" isEqualToString:type]) {
						float value = [escapedValue floatValue];
						[identityTraits setObject:[NSNumber numberWithDouble:value] forKey:key];
					} else if ([@"System.Int32" isEqualToString:type]) {
						int value = [escapedValue intValue];
						[identityTraits setObject:[NSNumber numberWithInt:value] forKey:key];
					} else if ([@"System.String" isEqualToString:type]) {
						[identityTraits setObject:escapedValue forKey:key];
					} else if ([@"System.DateTime" isEqualToString:type]) {
						NSDate *date = [dateFormatter dateFromString:escapedValue];
						[identityTraits setObject:date forKey:key];
					} else {
						NSLog(@"Unsupported trait type %@. Ignoring", type);
					}
				}
			}
		}

		[TestFairy identify:coId traits:identityTraits];
	}
}

void TestFairy_pause()
{
	[TestFairy pause];
}

void TestFairy_resume()
{
	[TestFairy resume];
}

void TestFairy_stop()
{
	[TestFairy stop];
}

const char * TestFairy_sessionUrl()
{
	const char *sessionUrl = [[TestFairy sessionUrl] UTF8String];
	return strdup(sessionUrl);
}

const char * TestFairy_version()
{
	const char *version = [[TestFairy version] UTF8String];
	return strdup(version);
}

void TestFairy_sendUserFeedback(char *feedback)
{
	NSString *value = feedback == NULL ? @"" : [NSString stringWithUTF8String:feedback];
	[TestFairy sendUserFeedback:value];
}

void TestFairy_sendUserFeedbackWithImage(char *appToken, char *text, char *imagePath) {
	UIImage *image = nil;
	if (imagePath != NULL) {
		image = [UIImage imageWithContentsOfFile:[NSString stringWithUTF8String: imagePath]];
	}

	[TestFairy sendUserFeedback:[NSString stringWithUTF8String:appToken]
						text:[NSString stringWithUTF8String:text]
						screenshot:image];
}

void TestFairy_takeScreenshot()
{
	[TestFairy takeScreenshot];
}

void TestFairy_setScreenName(char *name) {
	NSString *value = name == NULL ? nil : [NSString stringWithUTF8String:name];
	[TestFairy setScreenName:value];
}

void TestFairy_log(char *message) {
	NSString *value = message == NULL ? @"" : [NSString stringWithUTF8String:message];
	TFLog(@"%@", value);
}

void TestFairy_logException(char *message, char *trace) {
	NSString *messageString = message == NULL ? @"" : [NSString stringWithUTF8String:message];
	NSString *traceString = trace == NULL ? @"" : [NSString stringWithUTF8String:trace];

	NSError *error = [NSError errorWithDomain:@"com.testfairy.unity" code:-1 userInfo:@{NSLocalizedDescriptionKey: messageString}];
	[TestFairy logError:error stacktrace:[traceString componentsSeparatedByString:@"\n"]];
}

void TestFairy_hideWebViewElements(char *cssSelector) {
	if (cssSelector == NULL) {
		return;
	}

	NSString *value = [NSString stringWithUTF8String:cssSelector];
	[TestFairy hideWebViewElements:value];
}

void TestFairy_setUserId(char *userId) {
	if (userId == NULL) {
		return;
	}

	NSString *value = [NSString stringWithUTF8String:userId];
	[TestFairy setUserId:value];
}

bool TestFairy_setAttribute(char *aKey, char *aValue) {
	if (aKey == NULL || aValue == NULL) {
		return false;
	}

	NSString *key = [NSString stringWithUTF8String:aKey];
	NSString *value = [NSString stringWithUTF8String:aValue];
	return [TestFairy setAttribute:key withValue:value];
}

void TestFairy_enableCrashHandler() {
	[TestFairy enableCrashHandler];
}

void TestFairy_disableCrashHandler() {
	[TestFairy disableCrashHandler];
}

void TestFairy_enableMetric(char *metric) {
	NSString *value = metric == NULL ? @"" : [NSString stringWithUTF8String: metric];
	[TestFairy enableMetric: value];
}

void TestFairy_disableMetric(char *metric) {
	NSString *value = metric == NULL ? @"" : [NSString stringWithUTF8String: metric];
	[TestFairy disableMetric: value];
}

void TestFairy_enableVideo(char *policy, char *quality, float framesPerSecond) {
	NSString *policy_ = policy == NULL ? @"" : [NSString stringWithUTF8String: policy];
	NSString *quality_ = quality == NULL ? @"" : [NSString stringWithUTF8String: quality];
	[TestFairy enableVideo: policy_ quality: quality_ framesPerSecond: framesPerSecond];
}

void TestFairy_disableVideo() {
	[TestFairy disableVideo];
}

void TestFairy_enableFeedbackForm(char *method) {
	NSString *value = method  == NULL ? @"" : [NSString stringWithUTF8String:method];
	[TestFairy enableFeedbackForm: value];
}

void TestFairy_disableFeedbackForm() {
	[TestFairy disableFeedbackForm];
}

void TestFairy_disableAutoUpdate() {
	[TestFairy disableAutoUpdate];
}

void TestFairy_crash() {
	[TestFairy crash];
}

void TestFairy_installFeedbackHandler(char *appToken, char *method) {
	NSString *token = appToken == NULL ? @"" : [NSString stringWithUTF8String:appToken];
	NSString *value = method  == NULL ? @"" : [NSString stringWithUTF8String:method];
	[TestFairy installFeedbackHandler:token method:value];
}

void TestFairy_takeScreenshotWithCallback(TFNativeScreenshotCallback callback, char * identifier) {
	__block NSString *callbackId = [NSString stringWithUTF8String:identifier];
	[TestFairy takeScreenshot:^(UIImage *image) {
		if (image == nil) {
			callback([callbackId UTF8String], NULL, 0, 0);
			return;
		}

		NSData *data = UIImagePNGRepresentation(image);
		int width = [image size].width;
		int height = [image size].height;

		NSString *filename = [NSString stringWithFormat:@"%@.png", callbackId];
		NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
		NSString *documentsDirectory = [paths objectAtIndex:0];
		NSString *filePath = [documentsDirectory stringByAppendingPathComponent:filename];
		[data writeToFile:filePath atomically:YES];

		callback([callbackId UTF8String], [filePath UTF8String], width, height);
	}];
}
