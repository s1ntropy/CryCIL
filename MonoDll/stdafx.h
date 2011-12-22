#if !defined(AFX_STDAFX_H__B36C365D_F0EA_4545_B3BC_1E0EAB3B5E43__INCLUDED_)
#define AFX_STDAFX_H__B36C365D_F0EA_4545_B3BC_1E0EAB3B5E43__INCLUDED_

#include <CryModuleDefs.h>

// Add eCryM_Moon to the ECryModule enumeration, before ECryM_Num.
#define eCryModule eCryM_Mono

#undef NULL
#define NULL nullptr

// Insert your headers here
#include <platform.h>
#include <algorithm>
#include <vector>
#include <memory>
#include <list>
#include <functional>
#include <limits>

#include <smartptr.h>

#include <CryThread.h>
#include <Cry_Math.h>
#include <ISystem.h>
#include <I3DEngine.h>
#include <IInput.h>
#include <IConsole.h>
#include <ITimer.h>
#include <ILog.h>
#include <IGameplayRecorder.h>
#include <ISerialize.h>
#include <CryMemoryManager.h>

#include <mono/mini/jit.h>

#ifndef MONODLL_EXPORTS
#define MONODLL_EXPORTS
#endif

#ifdef MONODLL_EXPORTS
#define MONO_API DLL_EXPORT
#else
#define MONO_API
#endif

#pragma warning(disable: 4018)	// conditional expression is constant

//////////////////////////////////////////////////////////////////////////
//! Reports a Game Warning to validator with WARNING severity.
inline void GameWarning( const char *format,... ) PRINTF_PARAMS(1, 2);
inline void GameWarning( const char *format,... )
{
	if (!format)
		return;
	va_list args;
	va_start(args, format);
	GetISystem()->WarningV( VALIDATOR_MODULE_GAME,VALIDATOR_WARNING,0,NULL,format,args );
	va_end(args);
}

typedef MonoString * monostring;

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif //AFX_STDAFX_H__B36C365D_F0EA_4545_B3BC_1E0EAB3B5E43__INCLUDED_