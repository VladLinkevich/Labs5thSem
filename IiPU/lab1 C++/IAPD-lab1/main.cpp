#include "targetver.h"

#include <stdio.h>
#include <tchar.h>

#include <Windows.h>
#include <SetupAPI.h>
#include <locale.h>

#include <iostream>
#include <iomanip>
#include <string>

int main() {
	setlocale(0, "");			
	const uint8_t SIZE = UINT8_MAX;
	
	HDEVINFO DeviceInfoSet;												 
	DeviceInfoSet = SetupDiGetClassDevs(NULL,							 
										TEXT("PCI"),					 
										NULL,							 							     
										DIGCF_PRESENT | DIGCF_ALLCLASSES 
										);
	
	SP_DEVINFO_DATA DeviceInfoData;										 
	ZeroMemory(&DeviceInfoData, sizeof(SP_DEVINFO_DATA));
	DeviceInfoData.cbSize = sizeof(SP_DEVINFO_DATA);					 
	
	int deviceNum = 0;

	while (SetupDiEnumDeviceInfo(										
		       DeviceInfoSet,
		       deviceNum,
		       &DeviceInfoData)) 
	{
		deviceNum++;
		
		TCHAR bufferID[SIZE];
		ZeroMemory(bufferID, sizeof(bufferID));

		TCHAR bufferName[SIZE];
		ZeroMemory(bufferName, sizeof(bufferName));
		
		SetupDiGetDeviceInstanceId(DeviceInfoSet,						
								&DeviceInfoData,						
								bufferID,								 
								sizeof(bufferID),						
								NULL									 
								);

		SetupDiGetDeviceRegistryProperty(DeviceInfoSet,					
										&DeviceInfoData,				
										SPDRP_DEVICEDESC,				
										NULL,							
										(PBYTE)bufferName,				
										sizeof(bufferName),				
										NULL							 
										);
		
		std::string deviceName(bufferName);
		std::string deviceAndVendorID(bufferID);

		std::string vendorID = deviceAndVendorID.substr(8, 4);
		std::string deviceID = deviceAndVendorID.substr(17, 4);


		std::cout << "¹ " << deviceNum << " Device ID: " << deviceID << "\tVendor ID: " << vendorID << std::endl;
		std::cout << "Description: " << deviceName << std::endl;
	}

	if (DeviceInfoSet) 
	{
		SetupDiDestroyDeviceInfoList(DeviceInfoSet);
	}
	return 0;
}