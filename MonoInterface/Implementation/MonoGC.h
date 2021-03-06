#pragma once

#include "IMonoInterface.h"
#include "Implementation/MonoGCHandle.h"

#include <mono/metadata/object.h>
#include <mono/metadata/mono-gc.h>

struct MonoGC : public IMonoGC
{
	MonoGC() {}
	virtual void Collect(int generation = -1)
	{
		mono_gc_collect(generation);
	}

	virtual IMonoGCHandle *Hold(mono::object obj)
	{
		if (obj)
		{
			return new MonoGCHandleWeak(obj, false);
		}
		ReportError("Attempted to create weak GC handle for null pointer.");
		return nullptr;
	}

	virtual IMonoGCHandle *HoldWithHope(mono::object obj)
	{
		if (obj)
		{
			return new MonoGCHandleWeak(obj, true);
		}
		ReportError("Attempted to create weak GC handle for null pointer.");
		return nullptr;
	}

	virtual IMonoGCHandle *Keep(mono::object obj)
	{
		if (obj)
		{
			return new MonoGCHandleStrong(obj, false);
		}
		ReportError("Attempted to create strong GC handle for null pointer.");
		return nullptr;
	}

	virtual IMonoGCHandle *Pin(mono::object obj)
	{
		if (obj)
		{
			return new MonoGCHandleStrong(obj, true);
		}
		ReportError("Attempted to create a pinning GC handle for null pointer.");
		return nullptr;
	}

	virtual int GetMaxGeneration()
	{
		return mono_gc_max_generation();
	}

	virtual __int64 GetHeapSize()
	{
		return mono_gc_get_heap_size();
	}

};