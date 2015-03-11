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

