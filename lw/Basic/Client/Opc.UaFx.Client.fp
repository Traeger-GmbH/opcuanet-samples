s��        1�   D z  -H   �   ]����                                                               Opc.UaFx.Client.LabView                     CVIFUNC                                                      D  System_Nullable_T1 *[]     C  System_Nullable_T1 []     B ��System_Nullable_T1 **     A ��System_Nullable_T1 *     @ ��System_Nullable_T1     %? # $Opc_UaFx_Client_LabView_OpcStatus *[]     $> " #Opc_UaFx_Client_LabView_OpcStatus []     $= $��Opc_UaFx_Client_LabView_OpcStatus **     #< #��Opc_UaFx_Client_LabView_OpcStatus *     !; !��Opc_UaFx_Client_LabView_OpcStatus     $: " #Opc_UaFx_Client_LabView_OpcValue *[]     #9 ! "Opc_UaFx_Client_LabView_OpcValue []     #8 #��Opc_UaFx_Client_LabView_OpcValue **     "7 "��Opc_UaFx_Client_LabView_OpcValue *      6  ��Opc_UaFx_Client_LabView_OpcValue     5  System_EventHandler_T1 *[]     4  System_EventHandler_T1 []     3 ��System_EventHandler_T1 **     2 ��System_EventHandler_T1 *     1 ��System_EventHandler_T1     "0   !Opc_UaFx_Client_OpcClientState *[]     !/   Opc_UaFx_Client_OpcClientState []     !. !��Opc_UaFx_Client_OpcClientState **      -  ��Opc_UaFx_Client_OpcClientState *     , ��Opc_UaFx_Client_OpcClientState     %+ # $Opc_UaFx_Client_LabView_OpcClient *[]     $* " #Opc_UaFx_Client_LabView_OpcClient []     $) $��Opc_UaFx_Client_LabView_OpcClient **     #( #��Opc_UaFx_Client_LabView_OpcClient *     !' !��Opc_UaFx_Client_LabView_OpcClient     $& " #Opc_UaFx_Client_LabView_Licenser *[]     #% ! "Opc_UaFx_Client_LabView_Licenser []     #$ #��Opc_UaFx_Client_LabView_Licenser **     "# "��Opc_UaFx_Client_LabView_Licenser *      "  ��Opc_UaFx_Client_LabView_Licenser     ! 	 
ssize_t *[]     
   	ssize_t []     
 
��ssize_t **     	 	��ssize_t *      ��ssize_t     
  	size_t *[]     	  size_t []     	 	��size_t **      ��size_t *      ��size_t       CDotNetAssemblyHandle *[]       CDotNetAssemblyHandle []      ��CDotNetAssemblyHandle **      ��CDotNetAssemblyHandle *      ��CDotNetAssemblyHandle       CDotNetHandle *[]       CDotNetHandle []      ��CDotNetHandle **      ��CDotNetHandle *      ��CDotNetHandle     	  void **[]       void *[]      ��void ***     
 ��void **     		  char **[]      ��char ***     
  	double *[]     	 	��double **      ��double *     	  float *[]      ��float **      ��float *       unsigned __int64 *[]        unsigned __int64 []     � ��unsigned __int64 **     � ��unsigned __int64 *     � ��unsigned __int64     � 	 
__int64 *[]     
�  	__int64 []     
� 
��__int64 **     	� 	��__int64 *     � ��__int64     �  unsigned char *[]     � ��unsigned char **     � ��unsigned char *     � ��char **     �  unsigned short *[]     � ��unsigned short **     � ��unsigned short *     	�  short *[]     � ��short **     � ��short *     �  unsigned int *[]     � ��unsigned int **     � ��unsigned int *     �  int *[]     � ��int **     � ��int *  �    LabWindows/CVI .NET controller instrument wrapper for Opc.UaFx.Client.LabView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0220af0d33d50236.

The target assembly name is specified by the '__assemblyName' constant in the generated source file. If there are multiple versions of this assembly, and you want .NET to determine and load the appropriate one, then you can remove the version, culture, and public key information from the constant and just specify the name of the assembly.     t    Initializes this CVI .NET controller.

Call this function before calling any other functions in this controller.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.    �
���    �    Library Status                     	            j    Closes this CVI .NET controller.

Call this function after your program is done using this controller.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.    D
���    �    Library Status                     	            .    Gets the LicenseKey property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     :    You must free the output string using CDotNetFreeMemory.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �
���    �    Library Status                    � -      �    returnValue                       � - �   �    Exception Handle                   	            	            
        0    .    Sets the LicenseKey property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �
���    �    Library Status                  ���� -      �    value                             � - �   �    Exception Handle                   	                
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     �    A handle to the requested .NET object of type: Opc.UaFx.Client.LabView.OpcClient

Use this handle to invoke members of this .NET object.

When it is no longer needed, you must discard this handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �
���    �    Library Status                    � -   '  �    Instance Handle                   � - �   �    Exception Handle                   	            	            
        0    1    Gets the ServerAddress property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     :    You must free the output string using CDotNetFreeMemory.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �
���    �    Library Status                    } -   '  �    Instance Handle                   � - �    �    returnValue                        -�   �    Exception Handle                   	                	            
        0    1    Sets the ServerAddress property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ]
���    �    Library Status                     P -   '  �    Instance Handle                 ���� - �    �    value                              � -�   �    Exception Handle                   	                    
        0    )    Gets the State property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    "�
���    �    Library Status                    #� -   '  �    Instance Handle                 ���� - � ,  �    returnValue                       $. -�   �    Exception Handle                   	                	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     W    Delegate Type:
System.EventHandler`1[Opc.UaFx.Client.OpcClientStateChangedEventArgs]        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    &6
���    �    Library Status                    ') -   '  �    Instance Handle                   '� - � 1  �    value                             '� -�   �    Exception Handle                   	                    
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     W    Delegate Type:
System.EventHandler`1[Opc.UaFx.Client.OpcClientStateChangedEventArgs]        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    )�
���    �    Library Status                    *� -   '  �    Instance Handle                   +5 - � 1  �    value                             +� -�   �    Exception Handle                   	                    
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    -�
���    �    Library Status                    .� -   '  �    Instance Handle                   .� - �   �    Exception Handle                   	                
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    0�
���    �    Library Status                    1� -   '  �    Instance Handle                   1� - �   �    Exception Handle                   	                
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    3�
���    �    Library Status                    4� -   '  �    Instance Handle                   5 - �   �    Exception Handle                   	                
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    6�
���    �    Library Status                    7� -   '  �    Instance Handle                 ���� - �    �    nodeId                            8) -� 6  �    returnValue                       8q �    �    Exception Handle                   	                    	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    :�
���    �    Library Status                    ;� -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�   �    value                             < �  ;  �    returnValue                       <J � �   �    Exception Handle                   	                        	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    >�
���    �    Library Status                    ?� -   '  �    Instance Handle                 ���� - �    �    nodeId                            @ -� ;  �    status                          ���� �     �    returnValue                       @\ � �   �    Exception Handle                   	                    	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     9    You must free the output array using CDotNetFreeMemory.     0    Length of dimension 0 of array: __returnValue.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    B�
���    �    Library Status                    C� -   '  �    Instance Handle                 ���� - �    �    nodeId                            D. -� ;  �    status                            Dv �  �  �    returnValue                       D� � �   �    returnValueLength                 D� ��   �    Exception Handle                   	                    	            	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    G�
���    �    Library Status                    H� -   '  �    Instance Handle                 ���� - �    �    nodeId                            I -� ;  �    status                          ���� �     �    returnValue                       IJ � �   �    Exception Handle                   	                    	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     9    You must free the output array using CDotNetFreeMemory.     0    Length of dimension 0 of array: __returnValue.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    K�
���    �    Library Status                    L� -   '  �    Instance Handle                 ���� - �    �    nodeId                            M -� ;  �    status                            Md �  �  �    returnValue                       M� � �   �    returnValueLength                 M� ��   �    Exception Handle                   	                    	            	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    P�
���    �    Library Status                    Q� -   '  �    Instance Handle                 ���� - �    �    nodeId                            Q� -� ;  �    status                          ���� �      �    returnValue                       R8 � �   �    Exception Handle                   	                    	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     9    You must free the output array using CDotNetFreeMemory.     0    Length of dimension 0 of array: __returnValue.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    T�
���    �    Library Status                    U� -   '  �    Instance Handle                 ���� - �    �    nodeId                            V
 -� ;  �    status                            VR �  �  �    returnValue                       V� � �   �    returnValueLength                 V� ��   �    Exception Handle                   	                    	            	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    Y�
���    �    Library Status                    Z� -   '  �    Instance Handle                 ���� - �    �    nodeId                            Z� -� ;  �    status                          ���� �  �  �    returnValue                       [& � �   �    Exception Handle                   	                    	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     9    You must free the output array using CDotNetFreeMemory.     0    Length of dimension 0 of array: __returnValue.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ]�
���    �    Library Status                    ^� -   '  �    Instance Handle                 ���� - �    �    nodeId                            ^� -� ;  �    status                            _@ �  �  �    returnValue                       _� � �   �    returnValueLength                 _� ��   �    Exception Handle                   	                    	            	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    b|
���    �    Library Status                    co -   '  �    Instance Handle                 ���� - �    �    nodeId                            c� -� ;  �    status                          ���� �     �    returnValue                       d � �   �    Exception Handle                   	                    	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     9    You must free the output array using CDotNetFreeMemory.     0    Length of dimension 0 of array: __returnValue.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    f�
���    �    Library Status                    g� -   '  �    Instance Handle                 ���� - �    �    nodeId                            g� -� ;  �    status                            h. �  �  �    returnValue                       ho � �   �    returnValueLength                 h� ��   �    Exception Handle                   	                    	            	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    kj
���    �    Library Status                    l] -   '  �    Instance Handle                 ���� - �    �    nodeId                            l� -� ;  �    status                          ���� �     �    returnValue                       m � �   �    Exception Handle                   	                    	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     9    You must free the output array using CDotNetFreeMemory.     0    Length of dimension 0 of array: __returnValue.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    o�
���    �    Library Status                    pw -   '  �    Instance Handle                 ���� - �    �    nodeId                            p� -� ;  �    status                            q �  �  �    returnValue                       q] � �   �    returnValueLength                 q� ��   �    Exception Handle                   	                    	            	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     9    You must free the output array using CDotNetFreeMemory.     0    Length of dimension 0 of array: __returnValue.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    tX
���    �    Library Status                    uK -   '  �    Instance Handle                 ���� - �    �    nodeId                            u� -� ;  �    status                            u� �  �  �    returnValue                       v1 � �   �    returnValueLength                 vi ��   �    Exception Handle                   	                    	            	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     :    You must free the output string using CDotNetFreeMemory.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    y,
���    �    Library Status                    z -   '  �    Instance Handle                 ���� - �    �    nodeId                            z| -� ;  �    status                            z� �     �    returnValue                       { � �   �    Exception Handle                   	                    	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.     �    You must free each string in the output array using CDotNetFreeMemory, and then free the output array using CDotNetFreeMemory.     0    Length of dimension 0 of array: __returnValue.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    }�
���    �    Library Status                    ~{ -   '  �    Instance Handle                 ���� - �    �    nodeId                            ~� -� ;  �    status                              �  �  �    returnValue                       � � �   �    returnValueLength                 � ��   �    Exception Handle                   	                    	            	            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �� �  ;  �    status                            �; � �   �    Exception Handle                   	                        	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     (    Length of dimension 0 of array: value.     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             � �    �    valueLength                       �5 � � ;  �    status                            �} ��   �    Exception Handle                   	                            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �0
���    �    Library Status                    �# -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �� �  ;  �    status                            �� � �   �    Exception Handle                   	                        	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     (    Length of dimension 0 of array: value.     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �B
���    �    Library Status                    �5 -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�  
  �    value                             �� �    �    valueLength                       �� � � ;  �    status                            �
 ��   �    Exception Handle                   	                            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�     �    value                             � �  ;  �    status                            �U � �   �    Exception Handle                   	                        	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     (    Length of dimension 0 of array: value.     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             � �    �    valueLength                       �O � � ;  �    status                            �� ��   �    Exception Handle                   	                            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �J
���    �    Library Status                    �= -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -� �  �    value                             �� �  ;  �    status                            �� � �   �    Exception Handle                   	                        	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     (    Length of dimension 0 of array: value.     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �\
���    �    Library Status                    �O -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -� �  �    value                             �� �    �    valueLength                       �� � � ;  �    status                            �$ ��   �    Exception Handle                   	                            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �' �  ;  �    status                            �o � �   �    Exception Handle                   	                        	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     (    Length of dimension 0 of array: value.     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �9 �    �    valueLength                       �i � � ;  �    status                            �� ��   �    Exception Handle                   	                            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �d
���    �    Library Status                    �W -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �� �  ;  �    status                            �� � �   �    Exception Handle                   	                        	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     (    Length of dimension 0 of array: value.     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �v
���    �    Library Status                    �i -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �� �    �    valueLength                       �� � � ;  �    status                            �> ��   �    Exception Handle                   	                            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     (    Length of dimension 0 of array: value.     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �A �    �    valueLength                       �q � � ;  �    status                            �� ��   �    Exception Handle                   	                            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �l
���    �    Library Status                    �_ -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �� �  ;  �    status                            � � �   �    Exception Handle                   	                        	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcClient     (    Length of dimension 0 of array: value.     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �~
���    �    Library Status                    �q -   '  �    Instance Handle                 ���� - �    �    nodeId                          ���� -�    �    value                             �� �    �    valueLength                       �� � � ;  �    status                            �F ��   �    Exception Handle                   	                            	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     �    A handle to the requested .NET object of type: Opc.UaFx.Client.LabView.OpcStatus

Use this handle to invoke members of this .NET object.

When it is no longer needed, you must discard this handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   ;  �    Instance Handle                   �� - �   �    Exception Handle                   	            	            
        0    (    Gets the Code property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcStatus        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �� -   ;  �    Instance Handle                 ���� - �    �    returnValue                       �( -�   �    Exception Handle                   	                	            
        0    /    Gets the Description property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcStatus     :    You must free the output string using CDotNetFreeMemory.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �g
���    �    Library Status                    �Z -   ;  �    Instance Handle                   ˷ - �    �    returnValue                       �� -�   �    Exception Handle                   	                	            
        0    )    Gets the IsBad property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcStatus        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �2
���    �    Library Status                    �% -   ;  �    Instance Handle                 ���� - �     �    returnValue                       ς -�   �    Exception Handle                   	                	            
        0    *    Gets the IsGood property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcStatus        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    Ѽ
���    �    Library Status                    ү -   ;  �    Instance Handle                 ���� - �     �    returnValue                       � -�   �    Exception Handle                   	                	            
        0    /    Gets the IsUncertain property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     U    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcStatus        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �K
���    �    Library Status                    �> -   ;  �    Instance Handle                 ���� - �     �    returnValue                       ֛ -�   �    Exception Handle                   	                	            
        0    �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     �    A handle to the requested .NET object of type: Opc.UaFx.Client.LabView.OpcValue

Use this handle to invoke members of this .NET object.

When it is no longer needed, you must discard this handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    أ
���    �    Library Status                    ٖ -   6  �    Instance Handle                   ڂ - �   �    Exception Handle                   	            	            
        0    5    Gets the ServerPicoseconds property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ܎
���    �    Library Status                    ݁ -   6  �    Instance Handle                 ���� - �    �    returnValue                       �� -�   �    Exception Handle                   	                	            
        0    5    Sets the ServerPicoseconds property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �"
���    �    Library Status                    � -   6  �    Instance Handle                 ���� - �    �    value                             �q -�   �    Exception Handle                   	                    
        0    3    Gets the ServerTimestamp property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �
���    �    Library Status                    � -   6  �    Instance Handle                   �� - � @  �    returnValue                       �C -�   �    Exception Handle                   	                	            
        0    3    Sets the ServerTimestamp property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �
���    �    Library Status                    �y -   6  �    Instance Handle                 ���� - � @  �    value                             �� -�   �    Exception Handle                   	                    
        0    5    Gets the SourcePicoseconds property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �
���    �    Library Status                    � -   6  �    Instance Handle                 ���� - �    �    returnValue                       �a -�   �    Exception Handle                   	                	            
        0    5    Sets the SourcePicoseconds property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �
���    �    Library Status                    � -   6  �    Instance Handle                 ���� - �    �    value                             �� -�   �    Exception Handle                   	                    
        0    3    Gets the SourceTimestamp property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �0
���    �    Library Status                    �# -   6  �    Instance Handle                   � - � @  �    returnValue                       �� -�   �    Exception Handle                   	                	            
        0    3    Sets the SourceTimestamp property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �

���    �    Library Status                    �� -   6  �    Instance Handle                 ���� - � @  �    value                             �Y -�   �    Exception Handle                   	                    
        0    *    Gets the Status property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    ��
���    �    Library Status                    �~ -   6  �    Instance Handle                   �� - � ;  �    returnValue                       �" -�   �    Exception Handle                   	                	            
        0    )    Gets the Value property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue     @    You must discard the output handle using CDotNetDiscardHandle.        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.    �[
���    �    Library Status                    �N -   6  �    Instance Handle                   �� - �   �    returnValue                       �� -�   �    Exception Handle                   	                	            
        0    )    Sets the Value property of this object.     �    A value indicating whether an error occurred. A negative error code indicates function failure.

Error codes are defined in cvi\include\cvidotnet.h.

You can use CDotNetGetErrorDescription to get the description of an error code.     T    Pass a handle representing a .NET object of type: Opc.UaFx.Client.LabView.OpcValue        A handle to the exception thrown by the target .NET assembly. If the output value is not NULL, then you must discard it using CDotNetDiscardHandle. Call CDotNetGetExceptionInfo to get information about the exception.

You may pass NULL for this parameter.   +
���    �    Library Status                    -   6  �    Instance Handle                 ���� - �   �    value                            z -�   �    Exception Handle                   	                    
        0 ����         "  �             K.        Initialize_Opc_UaFx_Client_LabView                                                                                                      ����         �  7             K.        Close_Opc_UaFx_Client_LabView                                                                                                           ����         x  �             K.        Opc_UaFx_Client_LabView_Licenser_Get_LicenseKey                                                                                         ����         �  �             K.        Opc_UaFx_Client_LabView_Licenser_Set_LicenseKey                                                                                         ����       ����  �             K.        Opc_UaFx_Client_LabView_OpcClient__Create                                                                                               ����         Q  '             K.        Opc_UaFx_Client_LabView_OpcClient_Get_ServerAddress                                                                                     ����         $  !�             K.        Opc_UaFx_Client_LabView_OpcClient_Set_ServerAddress                                                                                     ����         "�  %9             K.        Opc_UaFx_Client_LabView_OpcClient_Get_State                                                                                             ����       ����  (�             K.        Opc_UaFx_Client_LabView_OpcClient_add_StateChanged                                                                                      ����       ����  ,�             K.        Opc_UaFx_Client_LabView_OpcClient_remove_StateChanged                                                                                   ����       ����  /�             K.        Opc_UaFx_Client_LabView_OpcClient_Connect                                                                                               ����       ����  3             K.        Opc_UaFx_Client_LabView_OpcClient_Disconnect                                                                                            ����       ����  6             K.        Opc_UaFx_Client_LabView_OpcClient_Dispose                                                                                               ����       ����  9|             K.        Opc_UaFx_Client_LabView_OpcClient_ReadNode                                                                                              ����       ����  =U             K.        Opc_UaFx_Client_LabView_OpcClient_WriteNode                                                                                             ����       ����  Ag             K.        Opc_UaFx_Client_LabView_OpcClient_ReadByte                                                                                              ����       ����  E�             K.        Opc_UaFx_Client_LabView_OpcClient_ReadByteArray                                                                                         ����       ����  JU             K.        Opc_UaFx_Client_LabView_OpcClient_ReadInt16                                                                                             ����       ����  N�             K.        Opc_UaFx_Client_LabView_OpcClient_ReadInt16Array                                                                                        ����       ����  SC             K.        Opc_UaFx_Client_LabView_OpcClient_ReadInt32                                                                                             ����       ����  W�             K.        Opc_UaFx_Client_LabView_OpcClient_ReadInt32Array                                                                                        ����       ����  \1             K.        Opc_UaFx_Client_LabView_OpcClient_ReadInt64                                                                                             ����       ����  `�             K.        Opc_UaFx_Client_LabView_OpcClient_ReadInt64Array                                                                                        ����       ����  e             K.        Opc_UaFx_Client_LabView_OpcClient_ReadUInt16                                                                                            ����       ����  i�             K.        Opc_UaFx_Client_LabView_OpcClient_ReadUInt16Array                                                                                       ����       ����  n             K.        Opc_UaFx_Client_LabView_OpcClient_ReadUInt32                                                                                            ����       ����  r�             K.        Opc_UaFx_Client_LabView_OpcClient_ReadUInt32Array                                                                                       ����       ����  wt             K.        Opc_UaFx_Client_LabView_OpcClient_ReadUInt64Array                                                                                       ����       ����  |             K.        Opc_UaFx_Client_LabView_OpcClient_ReadString                                                                                            ����       ����  ��             K.        Opc_UaFx_Client_LabView_OpcClient_ReadStringArray                                                                                       ����       ����  �F             K.        Opc_UaFx_Client_LabView_OpcClient_WriteByte                                                                                             ����       ����  ��             K.        Opc_UaFx_Client_LabView_OpcClient_WriteByteArray                                                                                        ����       ����  ��             K.        Opc_UaFx_Client_LabView_OpcClient_WriteInt16                                                                                            ����       ����  �             K.        Opc_UaFx_Client_LabView_OpcClient_WriteInt16Array                                                                                       ����       ����  �`             K.        Opc_UaFx_Client_LabView_OpcClient_WriteInt32                                                                                            ����       ����  ��             K.        Opc_UaFx_Client_LabView_OpcClient_WriteInt32Array                                                                                       ����       ����  ��             K.        Opc_UaFx_Client_LabView_OpcClient_WriteInt64                                                                                            ����       ����  �/             K.        Opc_UaFx_Client_LabView_OpcClient_WriteInt64Array                                                                                       ����       ����  �z             K.        Opc_UaFx_Client_LabView_OpcClient_WriteUInt16                                                                                           ����       ����  ��             K.        Opc_UaFx_Client_LabView_OpcClient_WriteUInt16Array                                                                                      ����       ����  �             K.        Opc_UaFx_Client_LabView_OpcClient_WriteUInt32                                                                                           ����       ����  �I             K.        Opc_UaFx_Client_LabView_OpcClient_WriteUInt32Array                                                                                      ����       ����  ��             K.        Opc_UaFx_Client_LabView_OpcClient_WriteUInt64Array                                                                                      ����       ����  �             K.        Opc_UaFx_Client_LabView_OpcClient_WriteString                                                                                           ����       ����  �Q             K.        Opc_UaFx_Client_LabView_OpcClient_WriteStringArray                                                                                      ����       ����  ��             K.        Opc_UaFx_Client_LabView_OpcStatus__Create                                                                                               ����         ƨ  �3             K.        Opc_UaFx_Client_LabView_OpcStatus_Get_Code                                                                                              ����         �0  �             K.        Opc_UaFx_Client_LabView_OpcStatus_Get_Description                                                                                       ����         �  Ѝ             K.        Opc_UaFx_Client_LabView_OpcStatus_Get_IsBad                                                                                             ����         ъ  �             K.        Opc_UaFx_Client_LabView_OpcStatus_Get_IsGood                                                                                            ����         �  צ             K.        Opc_UaFx_Client_LabView_OpcStatus_Get_IsUncertain                                                                                       ����       ����  ۍ             K.        Opc_UaFx_Client_LabView_OpcValue__Create                                                                                                ����         �Q  ��             K.        Opc_UaFx_Client_LabView_OpcValue_Get_ServerPicoseconds                                                                                  ����         ��  �|             K.        Opc_UaFx_Client_LabView_OpcValue_Set_ServerPicoseconds                                                                                  ����         �q  �N             K.        Opc_UaFx_Client_LabView_OpcValue_Get_ServerTimestamp                                                                                    ����         �K  ��             K.        Opc_UaFx_Client_LabView_OpcValue_Set_ServerTimestamp                                                                                    ����         ��  �l             K.        Opc_UaFx_Client_LabView_OpcValue_Get_SourcePicoseconds                                                                                  ����         �i  �              K.        Opc_UaFx_Client_LabView_OpcValue_Set_SourcePicoseconds                                                                                  ����         ��  ��             K.        Opc_UaFx_Client_LabView_OpcValue_Get_SourceTimestamp                                                                                    ����         ��  �d             K.        Opc_UaFx_Client_LabView_OpcValue_Set_SourceTimestamp                                                                                    ����         �Y  �-             K.        Opc_UaFx_Client_LabView_OpcValue_Get_Status                                                                                             ����         �*  ��             K.        Opc_UaFx_Client_LabView_OpcValue_Get_Value                                                                                              ����         � �             K.        Opc_UaFx_Client_LabView_OpcValue_Set_Value                                                                                                    /                                                                                     �Initialize                                                                           �Close                                                                             ����Opc_UaFx_Client_LabView_Licenser                                                     �Opc UaFx Client LabView Licenser Get LicenseKey                                      �Opc UaFx Client LabView Licenser Set LicenseKey                                   ����Opc_UaFx_Client_LabView_OpcClient                                                    �Type Constructor                                                                     �Opc UaFx Client LabView OpcClient Get ServerAddress                                  �Opc UaFx Client LabView OpcClient Set ServerAddress                                  �Opc UaFx Client LabView OpcClient Get State                                          �Opc UaFx Client LabView OpcClient add StateChanged                                   �Opc UaFx Client LabView OpcClient remove StateChanged                                �Opc UaFx Client LabView OpcClient Connect                                            �Opc UaFx Client LabView OpcClient Disconnect                                         �Opc UaFx Client LabView OpcClient Dispose                                            �Opc UaFx Client LabView OpcClient ReadNode                                           �Opc UaFx Client LabView OpcClient WriteNode                                          �Opc UaFx Client LabView OpcClient ReadByte                                           �Opc UaFx Client LabView OpcClient ReadByteArray                                      �Opc UaFx Client LabView OpcClient ReadInt16                                          �Opc UaFx Client LabView OpcClient ReadInt16Array                                     �Opc UaFx Client LabView OpcClient ReadInt32                                          �Opc UaFx Client LabView OpcClient ReadInt32Array                                     �Opc UaFx Client LabView OpcClient ReadInt64                                          �Opc UaFx Client LabView OpcClient ReadInt64Array                                     �Opc UaFx Client LabView OpcClient ReadUInt16                                         �Opc UaFx Client LabView OpcClient ReadUInt16Array                                    �Opc UaFx Client LabView OpcClient ReadUInt32                                         �Opc UaFx Client LabView OpcClient ReadUInt32Array                                    �Opc UaFx Client LabView OpcClient ReadUInt64Array                                    �Opc UaFx Client LabView OpcClient ReadString                                         �Opc UaFx Client LabView OpcClient ReadStringArray                                    �Opc UaFx Client LabView OpcClient WriteByte                                          �Opc UaFx Client LabView OpcClient WriteByteArray                                     �Opc UaFx Client LabView OpcClient WriteInt16                                         �Opc UaFx Client LabView OpcClient WriteInt16Array                                    �Opc UaFx Client LabView OpcClient WriteInt32                                         �Opc UaFx Client LabView OpcClient WriteInt32Array                                    �Opc UaFx Client LabView OpcClient WriteInt64                                         �Opc UaFx Client LabView OpcClient WriteInt64Array                                    �Opc UaFx Client LabView OpcClient WriteUInt16                                        �Opc UaFx Client LabView OpcClient WriteUInt16Array                                   �Opc UaFx Client LabView OpcClient WriteUInt32                                        �Opc UaFx Client LabView OpcClient WriteUInt32Array                                   �Opc UaFx Client LabView OpcClient WriteUInt64Array                                   �Opc UaFx Client LabView OpcClient WriteString                                        �Opc UaFx Client LabView OpcClient WriteStringArray                                ����Opc_UaFx_Client_LabView_OpcStatus                                                    �Type Constructor                                                                     �Opc UaFx Client LabView OpcStatus Get Code                                           �Opc UaFx Client LabView OpcStatus Get Description                                    �Opc UaFx Client LabView OpcStatus Get IsBad                                          �Opc UaFx Client LabView OpcStatus Get IsGood                                         �Opc UaFx Client LabView OpcStatus Get IsUncertain                                 ����Opc_UaFx_Client_LabView_OpcValue                                                     �Type Constructor                                                                     �Opc UaFx Client LabView OpcValue Get ServerPicoseconds                               �Opc UaFx Client LabView OpcValue Set ServerPicoseconds                               �Opc UaFx Client LabView OpcValue Get ServerTimestamp                                 �Opc UaFx Client LabView OpcValue Set ServerTimestamp                                 �Opc UaFx Client LabView OpcValue Get SourcePicoseconds                               �Opc UaFx Client LabView OpcValue Set SourcePicoseconds                               �Opc UaFx Client LabView OpcValue Get SourceTimestamp                                 �Opc UaFx Client LabView OpcValue Set SourceTimestamp                                 �Opc UaFx Client LabView OpcValue Get Status                                          �Opc UaFx Client LabView OpcValue Get Value                                           �Opc UaFx Client LabView OpcValue Set Value                                      