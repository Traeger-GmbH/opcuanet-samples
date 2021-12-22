// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

#include <ansi_c.h>
#include "Opc.UaFx.Client.h"

void CDotNetValidate(int result, CDotNetHandle* exception);


void Initialize()
{
	CDotNetRegisterAssemblyPath(
			"Opc.UaFx.Client.LabView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0220af0d33d50236",
			".\\net46\\Opc.UaFx.Client.LabView.dll");
	
	CDotNetValidate(Initialize_Opc_UaFx_Client_LabView(), NULL);
}


int main (int argc, char *argv[])
{
	Initialize();
	
	CDotNetHandle exception = NULL;	
	Opc_UaFx_Client_LabView_OpcClient client;
	
    CDotNetValidate(Opc_UaFx_Client_LabView_OpcClient__Create(&client, &exception), &exception);
    CDotNetValidate(Opc_UaFx_Client_LabView_OpcClient_Set_ServerAddress(client, "opc.tcp://localhost:4840", &exception), &exception);
	
	CDotNetValidate(Opc_UaFx_Client_LabView_OpcClient_Connect(client, &exception), &exception);
	
	char* value;
	Opc_UaFx_Client_LabView_OpcStatus status;
	
	CDotNetValidate(Opc_UaFx_Client_LabView_OpcClient_ReadString(client, "ns=2;s=Hello", &status, &value, &exception), &exception);
	
	if (value) {
		printf("%s", value);
    	CDotNetFreeMemory(value);
	}
	
	CDotNetValidate(Opc_UaFx_Client_LabView_OpcClient_Disconnect(client, &exception), &exception);
	CDotNetValidate(Opc_UaFx_Client_LabView_OpcClient_Dispose(client, &exception), &exception);
    CDotNetValidate(Close_Opc_UaFx_Client_LabView(), &exception);
	
	return 0;
}


void CDotNetValidate(int result, CDotNetHandle* exception)
{
	if (result != 0) {
		char* description = CDotNetGetErrorDescription(result);
		fprintf(stderr, description);

		if (exception != NULL) {
			char* message = NULL;
			char* source = NULL;
			char* stackTrace = NULL;
			
			CDotNetGetExceptionInfo(exception, NULL, &message, &source, &stackTrace, NULL, NULL);
	    	fprintf(stderr, "EXCEPTION:\n\tMessage: %s\n\tSource: %s\n\tStack Trace: %s\n", message, source, stackTrace);
			
    		CDotNetFreeMemory(message);
    		CDotNetFreeMemory(source);
    		CDotNetFreeMemory(stackTrace);
    		CDotNetDiscardHandle(exception);
		}
		
		exit(-1);
	}
	
	exception = NULL;
}
