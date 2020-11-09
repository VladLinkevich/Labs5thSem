#include <iostream>
#include <string>
#include <conio.h>
#include <Windows.h>
#include <powrprof.h>
#include <poclass.h>
#include <SetupAPI.h>
#include <devguid.h>

using namespace std;

string getPowerSupplyType(LPSYSTEM_POWER_STATUS lpSystemPowerStatus)
{
    if (lpSystemPowerStatus->ACLineStatus == 0) return "From the battery";
    else return "From the mains";
}

int getBatteryLevel(LPSYSTEM_POWER_STATUS lpSystemPowerStatus) {
    return lpSystemPowerStatus->BatteryLifePercent;
}

string getCurrentPowerSavingMode(LPSYSTEM_POWER_STATUS lpSystemPowerStatus) {
    if (lpSystemPowerStatus->SystemStatusFlag == 0) return "Off";
    else return "On";
}

string getBatteryType() {
    string batteryType;

    HDEVINFO hdev = SetupDiGetClassDevs(
        &GUID_DEVCLASS_BATTERY,
        0,
        0,
        DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

    if (hdev)
    {
        SP_DEVICE_INTERFACE_DATA did;
        did.cbSize = sizeof(did);
        SetupDiEnumDeviceInterfaces(hdev, 0, &GUID_DEVCLASS_BATTERY, 0, &did);

        DWORD cbRequired = 0;
        SetupDiGetDeviceInterfaceDetail(hdev, &did, 0, 0, &cbRequired, 0);

        PSP_DEVICE_INTERFACE_DETAIL_DATA pdidd = (PSP_DEVICE_INTERFACE_DETAIL_DATA)LocalAlloc(LPTR, cbRequired);

        if (pdidd)
        {
            pdidd->cbSize = sizeof(*pdidd);
            SetupDiGetDeviceInterfaceDetail(hdev, &did, pdidd, cbRequired, &cbRequired, 0);

            HANDLE batteryHandle = CreateFile(pdidd->DevicePath,
                GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_READ | FILE_SHARE_WRITE,
                NULL,
                OPEN_EXISTING,
                FILE_ATTRIBUTE_NORMAL,
                NULL);

            BATTERY_QUERY_INFORMATION bqi = { 0 };
            DeviceIoControl(batteryHandle,
                IOCTL_BATTERY_QUERY_TAG,
                NULL, NULL, &bqi.BatteryTag, sizeof(bqi.BatteryTag), NULL, NULL);

            BATTERY_INFORMATION bi;
            DeviceIoControl(batteryHandle, IOCTL_BATTERY_QUERY_INFORMATION, &bqi, sizeof(bqi), &bi, sizeof(bi), NULL, NULL);

            batteryType = reinterpret_cast<char*>(bi.Chemistry);

            CloseHandle(batteryHandle);
            LocalFree(pdidd);
        }
        SetupDiDestroyDeviceInfoList(hdev);
    }

    return batteryType.substr(0, 4);
}

void enterSleepMode() {
    SetSuspendState(false, true, false);
}

void enterHibernateMode() {
    SetSuspendState(true, true, false);
}

int main()
{

    LPSYSTEM_POWER_STATUS lpSystemPowerStatus;
    lpSystemPowerStatus = (LPSYSTEM_POWER_STATUS)calloc(2, sizeof(LPSYSTEM_POWER_STATUS));

    while (GetSystemPowerStatus(lpSystemPowerStatus))
    {
        if (_kbhit()) {
            switch (_getch())
            {
            case '0': enterSleepMode();
                break;
            case '1': enterHibernateMode();
                break;
            }
        }
        system("cls");

        cout << "Power supply type: " << getPowerSupplyType(lpSystemPowerStatus) << endl;
        cout << "The level of battery charge: " << getBatteryLevel(lpSystemPowerStatus) << " %" << endl;
        cout << "Current power saving mode: " << getCurrentPowerSavingMode(lpSystemPowerStatus) << endl;
        cout << "Battery type: " << getBatteryType() << endl;
        cout << endl << "Sleep mode - 0, Hibernation mode - 1" << endl;

        Sleep(1000);
    }
}