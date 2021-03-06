#include "stdafx.h"

#include "MonoObjects.h"
#include "MonoHandle.h"

IMonoHandle *MonoObjects::Wrap(mono::object obj)
{
	return new MonoHandle(obj);
}

void *MonoObjects::Unbox(mono::object value)
{
	return mono_object_unbox((MonoObject *)value);
}

IMonoArrays *MonoObjects::GetArrays()
{
	return this->arrays;
}

IMonoTexts *MonoObjects::GetTexts()
{
	return this->texts;
}

IMonoExceptions *MonoObjects::GetExceptions()
{
	return this->exceptions;
}

IMonoDelegates *MonoObjects::GetDelegates()
{
	return this->delegates;
}

IDefaultBoxinator *MonoObjects::GetBoxinator()
{
	return this->boxinator;
}

IMonoThreads *MonoObjects::GetThreads()
{
	return this->threads;
}
