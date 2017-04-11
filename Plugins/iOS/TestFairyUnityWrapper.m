#import "TestFairy.h"
#import "TestFairyUnityWrapper.h"

void TestFairy_begin(char *APIKey)
{
	NSString *value = APIKey == NULL ? @"" : [NSString stringWithUTF8String:APIKey];
	[TestFairy begin:value];
}

void TestFairy_pushFeedbackController()
{
	[TestFairy pushFeedbackController];
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
