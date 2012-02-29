/////////////////////////////////////////////////////////////////////////*
//Ink Studios Source File.
//Copyright (C), Ink Studios, 2011.
//////////////////////////////////////////////////////////////////////////
// IActionListener mono extension
//////////////////////////////////////////////////////////////////////////
// 02/02/2012 : Created by Filip 'i59' Lundgren
////////////////////////////////////////////////////////////////////////*/

#include <IMonoInput.h>
#include <IMonoScriptBind.h>

#include <IActionMapManager.h>
#include <IHardwareMouse.h>

#include "MonoCommon.h"

struct IMonoClass;

class CInput 
	: public IMonoInput
	, public IMonoScriptBind
	, public IActionListener
	, public IHardwareMouseEventListener
{
public:
	CInput();
	~CInput();

	// IMonoScriptBind
	virtual const char *GetClassName() { return "InputSystem"; }
	// ~IMonoScriptBind

	// IActionListener
	virtual void OnAction( const ActionId& action, int activationMode, float value );
	// ~IActionListener
	
	// IHardwareMouseEventListener
	virtual void OnHardwareMouseEvent(int iX,int iY,EHARDWAREMOUSEEVENT eHardwareMouseEvent, int wheelDelta = 0);
	// ~IHardwareMouseEventListener

	void Reset();

private:
	static void RegisterAction(mono::string);

	bool OnActionTriggered(EntityId entityId, const ActionId& actionId, int activationMode, float value);

	IMonoClass *m_pClass;
	
	static TActionHandler<CInput>	s_actionHandler;
};
