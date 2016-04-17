#import "TestFairy.h"
#import "TestFairyUnityWrapper.h"

void TestFairy_begin(char *APIKey)
{
	[TestFairy begin:[NSString stringWithUTF8String:APIKey]];
}

void TestFairy_pushFeedbackController()
{
	[TestFairy pushFeedbackController];
}

void TestFairy_checkpoint(char *name)
{
	[TestFairy checkpoint:[NSString stringWithUTF8String:name]];
}

void TestFairy_setCorrelationId(char *correlationId)
{
	[TestFairy setCorrelationId:[NSString stringWithUTF8String:correlationId]];
}

void TestFairy_identify(char *correlationId)
{
	[TestFairy identify:[NSString stringWithUTF8String:correlationId]];
}

void TestFairy_pause()
{
	[TestFairy pause];
}

void TestFairy_resume()
{
	[TestFairy resume];
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
	[TestFairy sendUserFeedback:[NSString stringWithUTF8String:feedback]];
}

void TestFairy_takeScreenshot()
{
	[TestFairy takeScreenshot];
}
