// CVI wrapper source file for .NET assembly: Opc.UaFx.Client.LabView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0220af0d33d50236

#include "Opc.UaFx.Client.h"
#include <stdarg.h>

// Macros
#ifndef __errChk
#define __errChk(f) if (__error = (f), __error < 0) goto __Error; else
#endif
#ifndef __nullChk
#define __nullChk(p) if (!(p)) { __error = CDotNetOutOfMemoryError; goto __Error; } else
#endif

// Constants
static const char * __assemblyName = "Opc.UaFx.Client.LabView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0220af0d33d50236";

// Static Variables
static CDotNetAssemblyHandle __assemblyHandle = 0;

// Forward declarations
static void _CDotNetGenDisposeScalar(void * scalar, unsigned int typeId);
static void CVIFUNC_C _CDotNetGenDisposeArray(void * array, unsigned int typeId, size_t nDims, ...);

// Global Functions
int CVIFUNC Initialize_Opc_UaFx_Client_LabView(void)
{
	int __error = 0;


	if (__assemblyHandle == 0)
		__errChk(CDotNetLoadAssembly(
			__assemblyName, 
			&__assemblyHandle));



__Error:
	return __error;
}

int CVIFUNC Close_Opc_UaFx_Client_LabView(void)
{
	int __error = 0;


	if (__assemblyHandle) {
		__errChk(CDotNetDiscardAssemblyHandle(__assemblyHandle));
		__assemblyHandle = 0;
	}



__Error:
	return __error;
}


// Type: Opc.UaFx.Client.LabView.Licenser
int CVIFUNC Opc_UaFx_Client_LabView_Licenser_Get_LicenseKey(
	char ** __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_STRING;

	// Call static member
	__errChk(CDotNetInvokeGenericStaticMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.Licenser", 
		0, 
		0, 
		CDOTNET_GET_PROPERTY, 
		"LicenseKey", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_STRING);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_Licenser_Set_LicenseKey(
	char * value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &value;

	// Call static member
	__errChk(CDotNetInvokeGenericStaticMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.Licenser", 
		0, 
		0, 
		CDOTNET_SET_PROPERTY, 
		"LicenseKey", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}


// Type: Opc.UaFx.Client.LabView.OpcClient
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient__Create(
	Opc_UaFx_Client_LabView_OpcClient * __instance,
	CDotNetHandle * __exception)
{
	int __error = 0;

	if (__exception)
		*__exception = 0;


	*__instance = 0;

	// Call constructor
	__errChk(CDotNetCreateGenericInstance(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		0, 
		0, 
		__instance, 
		0, 
		0, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Get_ServerAddress(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char ** __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_STRING;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"ServerAddress", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_STRING);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Set_ServerAddress(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &value;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_SET_PROPERTY, 
		"ServerAddress", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Get_State(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	Opc_UaFx_Client_OpcClientState * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_ENUM;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"State", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue)
		__errChk(CDotNetGetEnumValue(
			__returnValue__, 
			__returnValue));


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_add_StateChanged(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	System_EventHandler_T1 value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.EventHandler`1[[Opc.UaFx.Client.OpcClientStateChangedEventArgs, Opc.UaFx.Client, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0220af0d33d50236]]";
	__parameterTypes[0] = (CDOTNET_OBJECT);
	__parameters[0] = &value;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"add_StateChanged", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_remove_StateChanged(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	System_EventHandler_T1 value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.EventHandler`1[[Opc.UaFx.Client.OpcClientStateChangedEventArgs, Opc.UaFx.Client, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0220af0d33d50236]]";
	__parameterTypes[0] = (CDOTNET_OBJECT);
	__parameters[0] = &value;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"remove_StateChanged", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Connect(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	CDotNetHandle * __exception)
{
	int __error = 0;

	if (__exception)
		*__exception = 0;


	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"Connect", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Disconnect(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	CDotNetHandle * __exception)
{
	int __error = 0;

	if (__exception)
		*__exception = 0;


	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"Disconnect", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Dispose(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	CDotNetHandle * __exception)
{
	int __error = 0;

	if (__exception)
		*__exception = 0;


	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"Dispose", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadNode(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcValue * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_OBJECT;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadNode", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteNode(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	CDotNetHandle value,
	Opc_UaFx_Client_LabView_OpcStatus * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	__parameterTypeNames[1] = "System.Object";
	__parameterTypes[1] = (CDOTNET_OBJECT);
	__parameters[1] = &value;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_OBJECT;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteNode", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadByte(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned char * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_BYTE;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadByte", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadByteArray(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned char ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;
	if (__returnValue)
		*____returnValueLength = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_BYTE | CDOTNET_ARRAY;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadByteArray", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue__) {
		if (__returnValue)
			__errChk(CDotNetGetArrayElements(
				__returnValue__, 
				CDOTNET_BYTE, 
				0, 
				__returnValue));
		if (__returnValue)
			__errChk(CDotNetGetArrayLength(
				__returnValue__, 
				0, 
				____returnValueLength));
	}


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeArray(
			__returnValue, 
			CDOTNET_BYTE, 
			1, 
			____returnValueLength);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt16(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	short * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_INT16;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadInt16", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt16Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	short ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;
	if (__returnValue)
		*____returnValueLength = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_INT16 | CDOTNET_ARRAY;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadInt16Array", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue__) {
		if (__returnValue)
			__errChk(CDotNetGetArrayElements(
				__returnValue__, 
				CDOTNET_INT16, 
				0, 
				__returnValue));
		if (__returnValue)
			__errChk(CDotNetGetArrayLength(
				__returnValue__, 
				0, 
				____returnValueLength));
	}


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeArray(
			__returnValue, 
			CDOTNET_INT16, 
			1, 
			____returnValueLength);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt32(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	int * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_INT32;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadInt32", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt32Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	int ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;
	if (__returnValue)
		*____returnValueLength = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_INT32 | CDOTNET_ARRAY;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadInt32Array", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue__) {
		if (__returnValue)
			__errChk(CDotNetGetArrayElements(
				__returnValue__, 
				CDOTNET_INT32, 
				0, 
				__returnValue));
		if (__returnValue)
			__errChk(CDotNetGetArrayLength(
				__returnValue__, 
				0, 
				____returnValueLength));
	}


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeArray(
			__returnValue, 
			CDOTNET_INT32, 
			1, 
			____returnValueLength);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt64(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	__int64 * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_INT64;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadInt64", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt64Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	__int64 ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;
	if (__returnValue)
		*____returnValueLength = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_INT64 | CDOTNET_ARRAY;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadInt64Array", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue__) {
		if (__returnValue)
			__errChk(CDotNetGetArrayElements(
				__returnValue__, 
				CDOTNET_INT64, 
				0, 
				__returnValue));
		if (__returnValue)
			__errChk(CDotNetGetArrayLength(
				__returnValue__, 
				0, 
				____returnValueLength));
	}


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeArray(
			__returnValue, 
			CDOTNET_INT64, 
			1, 
			____returnValueLength);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt16(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned short * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_UINT16;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadUInt16", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt16Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned short ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;
	if (__returnValue)
		*____returnValueLength = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_UINT16 | CDOTNET_ARRAY;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadUInt16Array", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue__) {
		if (__returnValue)
			__errChk(CDotNetGetArrayElements(
				__returnValue__, 
				CDOTNET_UINT16, 
				0, 
				__returnValue));
		if (__returnValue)
			__errChk(CDotNetGetArrayLength(
				__returnValue__, 
				0, 
				____returnValueLength));
	}


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeArray(
			__returnValue, 
			CDOTNET_UINT16, 
			1, 
			____returnValueLength);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt32(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned int * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_UINT32;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadUInt32", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt32Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned int ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;
	if (__returnValue)
		*____returnValueLength = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_UINT32 | CDOTNET_ARRAY;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadUInt32Array", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue__) {
		if (__returnValue)
			__errChk(CDotNetGetArrayElements(
				__returnValue__, 
				CDOTNET_UINT32, 
				0, 
				__returnValue));
		if (__returnValue)
			__errChk(CDotNetGetArrayLength(
				__returnValue__, 
				0, 
				____returnValueLength));
	}


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeArray(
			__returnValue, 
			CDOTNET_UINT32, 
			1, 
			____returnValueLength);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt64Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned __int64 ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;
	if (__returnValue)
		*____returnValueLength = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_UINT64 | CDOTNET_ARRAY;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadUInt64Array", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue__) {
		if (__returnValue)
			__errChk(CDotNetGetArrayElements(
				__returnValue__, 
				CDOTNET_UINT64, 
				0, 
				__returnValue));
		if (__returnValue)
			__errChk(CDotNetGetArrayLength(
				__returnValue__, 
				0, 
				____returnValueLength));
	}


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeArray(
			__returnValue, 
			CDOTNET_UINT64, 
			1, 
			____returnValueLength);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadString(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	char ** __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_STRING;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadString", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_STRING);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadStringArray(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	char *** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[2] = {0};
	unsigned int __parameterTypes[2];
	void * __parameters[2];
	CDotNetHandle __returnValue__ = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	*status = 0;
	if (__returnValue)
		*__returnValue = 0;
	if (__returnValue)
		*____returnValueLength = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: status
	__parameterTypeNames[1] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[1] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[1] = status;

	// Pre-process return value
	__returnValueTypeId = CDOTNET_STRING | CDOTNET_ARRAY;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"ReadStringArray", 
		0, 
		0, 
		2, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		&__returnValueTypeId, 
		&__returnValue__, 
		__exception));

	// Post-process return value
	if (__returnValue__) {
		if (__returnValue)
			__errChk(CDotNetGetArrayElements(
				__returnValue__, 
				CDOTNET_STRING, 
				0, 
				__returnValue));
		if (__returnValue)
			__errChk(CDotNetGetArrayLength(
				__returnValue__, 
				0, 
				____returnValueLength));
	}


__Error:
	if (__returnValue__)
		CDotNetDiscardHandle(__returnValue__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
		_CDotNetGenDisposeArray(
			__returnValue, 
			CDOTNET_STRING, 
			1, 
			____returnValueLength);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteByte(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned char value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	__parameterTypeNames[1] = "System.Byte";
	__parameterTypes[1] = (CDOTNET_BYTE);
	__parameters[1] = &value;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteByte", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteByteArray(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned char * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];
	CDotNetHandle value__ = 0;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	if (value)
		__errChk(CDotNetCreateArray(
			CDOTNET_BYTE, 
			1, 
			0, 
			&__valueLength, 
			value, 
			&value__));
	__parameterTypeNames[1] = "System.Byte[]";
	__parameterTypes[1] = (CDOTNET_BYTE | CDOTNET_ARRAY);
	__parameters[1] = &value__;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteByteArray", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (value__)
		CDotNetDiscardHandle(value__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt16(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	short value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	__parameterTypeNames[1] = "System.Int16";
	__parameterTypes[1] = (CDOTNET_INT16);
	__parameters[1] = &value;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteInt16", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt16Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	short * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];
	CDotNetHandle value__ = 0;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	if (value)
		__errChk(CDotNetCreateArray(
			CDOTNET_INT16, 
			1, 
			0, 
			&__valueLength, 
			value, 
			&value__));
	__parameterTypeNames[1] = "System.Int16[]";
	__parameterTypes[1] = (CDOTNET_INT16 | CDOTNET_ARRAY);
	__parameters[1] = &value__;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteInt16Array", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (value__)
		CDotNetDiscardHandle(value__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt32(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	int value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	__parameterTypeNames[1] = "System.Int32";
	__parameterTypes[1] = (CDOTNET_INT32);
	__parameters[1] = &value;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteInt32", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt32Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	int * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];
	CDotNetHandle value__ = 0;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	if (value)
		__errChk(CDotNetCreateArray(
			CDOTNET_INT32, 
			1, 
			0, 
			&__valueLength, 
			value, 
			&value__));
	__parameterTypeNames[1] = "System.Int32[]";
	__parameterTypes[1] = (CDOTNET_INT32 | CDOTNET_ARRAY);
	__parameters[1] = &value__;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteInt32Array", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (value__)
		CDotNetDiscardHandle(value__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt64(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	__int64 value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	__parameterTypeNames[1] = "System.Int64";
	__parameterTypes[1] = (CDOTNET_INT64);
	__parameters[1] = &value;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteInt64", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt64Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	__int64 * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];
	CDotNetHandle value__ = 0;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	if (value)
		__errChk(CDotNetCreateArray(
			CDOTNET_INT64, 
			1, 
			0, 
			&__valueLength, 
			value, 
			&value__));
	__parameterTypeNames[1] = "System.Int64[]";
	__parameterTypes[1] = (CDOTNET_INT64 | CDOTNET_ARRAY);
	__parameters[1] = &value__;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteInt64Array", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (value__)
		CDotNetDiscardHandle(value__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt16(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned short value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	__parameterTypeNames[1] = "System.UInt16";
	__parameterTypes[1] = (CDOTNET_UINT16);
	__parameters[1] = &value;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteUInt16", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt16Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned short * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];
	CDotNetHandle value__ = 0;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	if (value)
		__errChk(CDotNetCreateArray(
			CDOTNET_UINT16, 
			1, 
			0, 
			&__valueLength, 
			value, 
			&value__));
	__parameterTypeNames[1] = "System.UInt16[]";
	__parameterTypes[1] = (CDOTNET_UINT16 | CDOTNET_ARRAY);
	__parameters[1] = &value__;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteUInt16Array", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (value__)
		CDotNetDiscardHandle(value__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt32(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned int value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	__parameterTypeNames[1] = "System.UInt32";
	__parameterTypes[1] = (CDOTNET_UINT32);
	__parameters[1] = &value;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteUInt32", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt32Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned int * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];
	CDotNetHandle value__ = 0;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	if (value)
		__errChk(CDotNetCreateArray(
			CDOTNET_UINT32, 
			1, 
			0, 
			&__valueLength, 
			value, 
			&value__));
	__parameterTypeNames[1] = "System.UInt32[]";
	__parameterTypes[1] = (CDOTNET_UINT32 | CDOTNET_ARRAY);
	__parameters[1] = &value__;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteUInt32Array", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (value__)
		CDotNetDiscardHandle(value__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt64Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned __int64 * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];
	CDotNetHandle value__ = 0;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	if (value)
		__errChk(CDotNetCreateArray(
			CDOTNET_UINT64, 
			1, 
			0, 
			&__valueLength, 
			value, 
			&value__));
	__parameterTypeNames[1] = "System.UInt64[]";
	__parameterTypes[1] = (CDOTNET_UINT64 | CDOTNET_ARRAY);
	__parameters[1] = &value__;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteUInt64Array", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (value__)
		CDotNetDiscardHandle(value__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteString(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	char * value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	__parameterTypeNames[1] = "System.String";
	__parameterTypes[1] = (CDOTNET_STRING);
	__parameters[1] = &value;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteString", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteStringArray(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	char ** value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[3] = {0};
	unsigned int __parameterTypes[3];
	void * __parameters[3];
	CDotNetHandle value__ = 0;

	if (__exception)
		*__exception = 0;
	*status = 0;


	// Pre-process parameter: nodeId
	__parameterTypeNames[0] = "System.String";
	__parameterTypes[0] = (CDOTNET_STRING);
	__parameters[0] = &nodeId;

	// Pre-process parameter: value
	if (value)
		__errChk(CDotNetCreateArray(
			CDOTNET_STRING, 
			1, 
			0, 
			&__valueLength, 
			value, 
			&value__));
	__parameterTypeNames[1] = "System.String[]";
	__parameterTypes[1] = (CDOTNET_STRING | CDOTNET_ARRAY);
	__parameters[1] = &value__;

	// Pre-process parameter: status
	__parameterTypeNames[2] = "Opc.UaFx.Client.LabView.OpcStatus&";
	__parameterTypes[2] = (CDOTNET_OBJECT | CDOTNET_OUT);
	__parameters[2] = status;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcClient", 
		__instance, 
		CDOTNET_CALL_METHOD, 
		"WriteStringArray", 
		0, 
		0, 
		3, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	if (value__)
		CDotNetDiscardHandle(value__);
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			status, 
			CDOTNET_OBJECT);
	}
	return __error;
}


// Type: Opc.UaFx.Client.LabView.OpcStatus
int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus__Create(
	Opc_UaFx_Client_LabView_OpcStatus * __instance,
	CDotNetHandle * __exception)
{
	int __error = 0;

	if (__exception)
		*__exception = 0;


	*__instance = 0;

	// Call constructor
	__errChk(CDotNetCreateGenericInstance(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcStatus", 
		0, 
		0, 
		__instance, 
		0, 
		0, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_Code(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	unsigned int * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_UINT32;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcStatus", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"Code", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_Description(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	char ** __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_STRING;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcStatus", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"Description", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_STRING);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_IsBad(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	int * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_BOOLEAN;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcStatus", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"IsBad", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_IsGood(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	int * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_BOOLEAN;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcStatus", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"IsGood", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_IsUncertain(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	int * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_BOOLEAN;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcStatus", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"IsUncertain", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	return __error;
}


// Type: Opc.UaFx.Client.LabView.OpcValue
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue__Create(
	Opc_UaFx_Client_LabView_OpcValue * __instance,
	CDotNetHandle * __exception)
{
	int __error = 0;

	if (__exception)
		*__exception = 0;


	*__instance = 0;

	// Call constructor
	__errChk(CDotNetCreateGenericInstance(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		0, 
		0, 
		__instance, 
		0, 
		0, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_ServerPicoseconds(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	unsigned short * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_UINT16;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"ServerPicoseconds", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_ServerPicoseconds(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	unsigned short value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.UInt16";
	__parameterTypes[0] = (CDOTNET_UINT16);
	__parameters[0] = &value;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_SET_PROPERTY, 
		"ServerPicoseconds", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_ServerTimestamp(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	System_Nullable_T1 * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_STRUCT;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"ServerTimestamp", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_STRUCT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_ServerTimestamp(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	System_Nullable_T1 value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
	__parameterTypes[0] = (CDOTNET_STRUCT);
	__parameters[0] = &value;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_SET_PROPERTY, 
		"ServerTimestamp", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_SourcePicoseconds(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	unsigned short * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_UINT16;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"SourcePicoseconds", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_SourcePicoseconds(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	unsigned short value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.UInt16";
	__parameterTypes[0] = (CDOTNET_UINT16);
	__parameters[0] = &value;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_SET_PROPERTY, 
		"SourcePicoseconds", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_SourceTimestamp(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	System_Nullable_T1 * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_STRUCT;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"SourceTimestamp", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_STRUCT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_SourceTimestamp(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	System_Nullable_T1 value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
	__parameterTypes[0] = (CDOTNET_STRUCT);
	__parameters[0] = &value;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_SET_PROPERTY, 
		"SourceTimestamp", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_Status(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	Opc_UaFx_Client_LabView_OpcStatus * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_OBJECT;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"Status", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_Value(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	CDotNetHandle * __returnValue,
	CDotNetHandle * __exception)
{
	int __error = 0;
	unsigned int __returnValueTypeId;

	if (__exception)
		*__exception = 0;
	if (__returnValue)
		*__returnValue = 0;


	// Pre-process return value
	__returnValueTypeId = CDOTNET_OBJECT;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_GET_PROPERTY, 
		"Value", 
		0, 
		0, 
		0, 
		0, 
		0, 
		0, 
		&__returnValueTypeId, 
		__returnValue, 
		__exception));


__Error:
	if (__error < 0) {
		_CDotNetGenDisposeScalar(
			__returnValue, 
			CDOTNET_OBJECT);
	}
	return __error;
}

int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_Value(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	CDotNetHandle value,
	CDotNetHandle * __exception)
{
	int __error = 0;
	char * __parameterTypeNames[1] = {0};
	unsigned int __parameterTypes[1];
	void * __parameters[1];

	if (__exception)
		*__exception = 0;


	// Pre-process parameter: value
	__parameterTypeNames[0] = "System.Object";
	__parameterTypes[0] = (CDOTNET_OBJECT);
	__parameters[0] = &value;

	// Call instance member
	__errChk(CDotNetInvokeGenericMember(
		__assemblyHandle, 
		"Opc.UaFx.Client.LabView.OpcValue", 
		__instance, 
		CDOTNET_SET_PROPERTY, 
		"Value", 
		0, 
		0, 
		1, 
		(const char **)__parameterTypeNames, 
		__parameterTypes, 
		__parameters, 
		0, 
		0, 
		__exception));


__Error:
	return __error;
}



// Internal functions
static void _CDotNetGenDisposeScalar(void * scalar, unsigned int typeId)
{
	if (!*(void **)scalar)
		return;

	typeId &= CDOTNET_BASIC_TYPE_MASK;
	if (typeId == CDOTNET_STRING) {
		CDotNetFreeMemory(*(char **)scalar);
		*(char **)scalar = 0;
	}
	else if (typeId == CDOTNET_OBJECT || typeId == CDOTNET_STRUCT) {
		CDotNetDiscardHandle(*(CDotNetHandle *)scalar);
		*(CDotNetHandle *)scalar = 0;
	}
}

static void CVIFUNC_C _CDotNetGenDisposeArray(void * array, unsigned int typeId, size_t nDims, ...)
{
	size_t i;
	ssize_t totalLength = 1;
	va_list list;

	if (!*(void **)array)
		return;

	va_start(list, nDims);
	for (i = 0; i < nDims; ++i) {
		ssize_t * lenPtr = va_arg(list, ssize_t*);

		totalLength *= *lenPtr;
		*lenPtr = 0;
	}
	va_end(list);

	typeId &= CDOTNET_BASIC_TYPE_MASK;
	if (typeId == CDOTNET_STRING)
		while (--totalLength >= 0)
			CDotNetFreeMemory((*(char ***)array)[totalLength]);
	else if (typeId > CDOTNET_ENUM)
		while (--totalLength >= 0)
			CDotNetDiscardHandle((*(CDotNetHandle **)array)[totalLength]);

	CDotNetFreeMemory(*(void**)array);
	*(void**)array = 0;
}

