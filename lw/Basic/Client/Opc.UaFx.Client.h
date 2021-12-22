// CVI wrapper header file for .NET assembly: Opc.UaFx.Client.LabView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0220af0d33d50236
//
// This is specified by the '__assemblyName' constant in the generated source file.
// If there are multiple versions of this assembly, and you want .NET to determine 
// the appropriate one to load, then you can remove the version, culture, and public 
// key token information from the constant and just specify the name of the assembly.
//
// Other assemblies referenced by the target assembly:
// Name:+ Opc.UaFx.Client, Location: c:\Dev\OPC-UA Samples (GitHub)\lw\Basic\Client\net46\Opc.UaFx.Client.dll
// Name:+ mscorlib, Location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll
//
// If any assembly, including the target assembly, is not installed in the
// Global Assembly Cache (GAC) or in the application directory, and the wrapper
// code needs to load the assembly to convert types like arrays and enums, then
// you must register the path of the assembly with the CVI .NET library by
// calling CDotNetRegisterAssemblyPath before calling the wrapper code.
//
// Types exposed by the target assembly but defined in other assemblies:
// CVI name: Opc_UaFx_Client_OpcClientState, .NET name: Opc.UaFx.Client.OpcClientState, Assembly: Opc.UaFx.Client, Module: c:\Dev\OPC-UA Samples (GitHub)\lw\Basic\Client\net46\Opc.UaFx.Client.dll
// CVI name: System_Nullable_T1, .NET name: System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], Assembly: mscorlib, Module: Global Assembly Cache
// CVI name: System_EventHandler_T1, .NET name: System.EventHandler`1[[Opc.UaFx.Client.OpcClientStateChangedEventArgs, Opc.UaFx.Client, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0220af0d33d50236]], Assembly: mscorlib, Module: Global Assembly Cache

#include <cvidotnet.h>

#ifdef __cplusplus
extern "C" {
#endif

// Type definitions
typedef struct __Opc_UaFx_Client_LabView_Licenser * Opc_UaFx_Client_LabView_Licenser;
typedef struct __Opc_UaFx_Client_LabView_OpcClient * Opc_UaFx_Client_LabView_OpcClient;
typedef struct __System_EventHandler_T1 * System_EventHandler_T1;
typedef struct __Opc_UaFx_Client_LabView_OpcStatus * Opc_UaFx_Client_LabView_OpcStatus;
typedef struct __Opc_UaFx_Client_LabView_OpcValue * Opc_UaFx_Client_LabView_OpcValue;
typedef struct __System_Nullable_T1 * System_Nullable_T1;

// C wrapper for enumeration type Opc.UaFx.Client.OpcClientState
#ifndef Opc_UaFx_Client_OpcClientState_DEFINED
#define Opc_UaFx_Client_OpcClientState_DEFINED
typedef enum Opc_UaFx_Client_OpcClientState
{
	Opc_UaFx_Client_OpcClientState_Created = 0x0,
	Opc_UaFx_Client_OpcClientState_Connecting = 0x1,
	Opc_UaFx_Client_OpcClientState_Connected = 0x2,
	Opc_UaFx_Client_OpcClientState_Disconnecting = 0x3,
	Opc_UaFx_Client_OpcClientState_Disconnected = 0x4,
	Opc_UaFx_Client_OpcClientState_Reconnecting = 0x5,
	Opc_UaFx_Client_OpcClientState_Reconnected = 0x6,
} Opc_UaFx_Client_OpcClientState;
#endif // Opc_UaFx_Client_OpcClientState_DEFINED




// Global Functions
int CVIFUNC Initialize_Opc_UaFx_Client_LabView(void);
int CVIFUNC Close_Opc_UaFx_Client_LabView(void);

// Type: Opc.UaFx.Client.LabView.Licenser
int CVIFUNC Opc_UaFx_Client_LabView_Licenser_Get_LicenseKey(
	char ** __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_Licenser_Set_LicenseKey(
	char * value,
	CDotNetHandle * __exception);

// Type: Opc.UaFx.Client.LabView.OpcClient
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient__Create(
	Opc_UaFx_Client_LabView_OpcClient * __instance,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Get_ServerAddress(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char ** __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Set_ServerAddress(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * value,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Get_State(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	Opc_UaFx_Client_OpcClientState * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_add_StateChanged(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	System_EventHandler_T1 value,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_remove_StateChanged(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	System_EventHandler_T1 value,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Connect(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Disconnect(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_Dispose(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadNode(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcValue * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteNode(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	CDotNetHandle value,
	Opc_UaFx_Client_LabView_OpcStatus * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadByte(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned char * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadByteArray(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned char ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt16(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	short * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt16Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	short ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt32(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	int * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt32Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	int ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt64(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	__int64 * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadInt64Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	__int64 ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt16(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned short * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt16Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned short ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt32(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned int * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt32Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned int ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadUInt64Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	unsigned __int64 ** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadString(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	char ** __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_ReadStringArray(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	char *** __returnValue,
	ssize_t * ____returnValueLength,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteByte(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned char value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteByteArray(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned char * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt16(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	short value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt16Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	short * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt32(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	int value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt32Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	int * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt64(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	__int64 value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteInt64Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	__int64 * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt16(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned short value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt16Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned short * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt32(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned int value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt32Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned int * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteUInt64Array(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	unsigned __int64 * value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteString(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	char * value,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcClient_WriteStringArray(
	Opc_UaFx_Client_LabView_OpcClient __instance,
	char * nodeId,
	char ** value,
	ssize_t __valueLength,
	Opc_UaFx_Client_LabView_OpcStatus * status,
	CDotNetHandle * __exception);

// Type: Opc.UaFx.Client.LabView.OpcStatus
int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus__Create(
	Opc_UaFx_Client_LabView_OpcStatus * __instance,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_Code(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	unsigned int * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_Description(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	char ** __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_IsBad(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	int * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_IsGood(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	int * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcStatus_Get_IsUncertain(
	Opc_UaFx_Client_LabView_OpcStatus __instance,
	int * __returnValue,
	CDotNetHandle * __exception);

// Type: Opc.UaFx.Client.LabView.OpcValue
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue__Create(
	Opc_UaFx_Client_LabView_OpcValue * __instance,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_ServerPicoseconds(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	unsigned short * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_ServerPicoseconds(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	unsigned short value,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_ServerTimestamp(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	System_Nullable_T1 * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_ServerTimestamp(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	System_Nullable_T1 value,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_SourcePicoseconds(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	unsigned short * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_SourcePicoseconds(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	unsigned short value,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_SourceTimestamp(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	System_Nullable_T1 * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_SourceTimestamp(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	System_Nullable_T1 value,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_Status(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	Opc_UaFx_Client_LabView_OpcStatus * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Get_Value(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	CDotNetHandle * __returnValue,
	CDotNetHandle * __exception);
int CVIFUNC Opc_UaFx_Client_LabView_OpcValue_Set_Value(
	Opc_UaFx_Client_LabView_OpcValue __instance,
	CDotNetHandle value,
	CDotNetHandle * __exception);


#ifdef __cplusplus
}
#endif
