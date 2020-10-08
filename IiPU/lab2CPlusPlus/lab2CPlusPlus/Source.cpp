#include <Windows.h>
#include <iostream>
#include <iomanip>

using namespace std;

#define BYTE_THOUSAND 1024
#define Hundred 100
#define BYTE_SIZE 8

static const char* busTypeName[] = { "BusTypeUnknown", "BusTypeScsi", "BusTypeAtapi", "BusTypeAta",
                                     "BusType1394", "BusTypeSsa", "BusTypeFibre", "BusTypeUsb",
                                     "BusTypeRAID", "BusTypeiScsi", "BusTypeSas", "BusTypeSata",
                                     "BusTypeSd", "BusTypeMmc", "BusTypeVirtual", "BusTypeFileBackedVirtual",
                                     "BusTypeMax", "BusTypeMaxReserved"
};
static const char* driveType[] = { "UNKNOWN", "INVALID", "CARD_READER/FLASH", "HARD", "REMOTE", "CD_ROM", "RAM" };

void exitWithError(DWORD error);
void getMemotyInfo();
void getDeviceInfo(HANDLE diskHandle, STORAGE_PROPERTY_QUERY storageProtertyQuery);
void getMemoryTransferMode(HANDLE diskHandle, STORAGE_PROPERTY_QUERY storageProtertyQuery);
bool isSsd(HANDLE hDevice);

int main() {
    STORAGE_PROPERTY_QUERY storagePropertyQuery;                // properties query of a storage device or adapter
    storagePropertyQuery.QueryType = PropertyStandardQuery;     // flags indicating the type of query
    storagePropertyQuery.PropertyId = StorageDeviceProperty;    // Indicates whether the caller is requesting a device descriptor
    HANDLE diskHandle = CreateFileA("//./PhysicalDrive0",
        GENERIC_READ,                                           // desired Access
        FILE_SHARE_READ,                                        // share Mode
        nullptr,                                                // security Attributes
        OPEN_EXISTING,                                          // Creation Disposition
        NULL,                                                   // Flags And Attributes
        nullptr);                                               // Template File
    if (diskHandle == INVALID_HANDLE_VALUE) {
        exitWithError(GetLastError());
    }
    if (isSsd(diskHandle) == true) {
        cout << "SSD:" << endl << endl;
    }
    else {
        cout << "HDD:" << endl << endl;
    }

    getDeviceInfo(diskHandle, storagePropertyQuery);
    getMemoryTransferMode(diskHandle, storagePropertyQuery);
    getMemotyInfo();

    CloseHandle(diskHandle);
    getchar();
    return 0;
}

void getMemotyInfo() {
    string path;
    _ULARGE_INTEGER diskSpace;
    diskSpace.QuadPart = 0;
    _ULARGE_INTEGER freeSpace;
    freeSpace.QuadPart = 0;
    _ULARGE_INTEGER totalDiskSpace;
    totalDiskSpace.QuadPart = 0;
    _ULARGE_INTEGER totalFreeSpace;
    totalFreeSpace.QuadPart = 0;

    unsigned long int logicalDrivesCount = GetLogicalDrives();

    for (char i = 'A'; i < 'Z'; i++) {
        if ((logicalDrivesCount >> (i - 65)) && i != 'F') {
            path = i;
            path.append(":\\");
            GetDiskFreeSpaceExA(path.c_str(),
                nullptr,                    //Free Bytes Available To Caller
                &diskSpace, &freeSpace);
            diskSpace.QuadPart = diskSpace.QuadPart / (BYTE_THOUSAND * BYTE_THOUSAND * BYTE_THOUSAND);
            freeSpace.QuadPart = freeSpace.QuadPart / (BYTE_THOUSAND * BYTE_THOUSAND * BYTE_THOUSAND);

            if (diskSpace.QuadPart != 0) {

                cout << endl << "Disk \"" << i << "\":" << endl;
                cout << "Total space[Gb]: " << diskSpace.QuadPart << endl;
                cout << "Free space[Gb]: " << freeSpace.QuadPart << endl;
                cout << "Busy space[%]: " << setprecision(3) << 100.0 - (double)freeSpace.QuadPart /
                    (double)diskSpace.QuadPart * Hundred << endl;
                cout << "Driver type: " << driveType[GetDriveTypeA(path.c_str())] << endl;

                if (GetDriveTypeA(path.c_str()) == DRIVE_FIXED) {
                    totalDiskSpace.QuadPart += diskSpace.QuadPart;
                    totalFreeSpace.QuadPart += freeSpace.QuadPart;
                }
            }
        }
    }
    cout << endl << "Total space:" << endl;
    cout << "Total space[Gb]: " << totalDiskSpace.QuadPart << endl;
    cout << "Free space[Gb]: " << totalFreeSpace.QuadPart << endl;
    cout << "Busy space[%]: " << setprecision(3) << 100.0 - (double)totalFreeSpace.QuadPart /
        (double)totalDiskSpace.QuadPart * Hundred << endl;
}

void getDeviceInfo(HANDLE diskHandle, STORAGE_PROPERTY_QUERY storageProtertyQuery) {
    STORAGE_DEVICE_DESCRIPTOR* deviceDescriptor = (STORAGE_DEVICE_DESCRIPTOR*)calloc(BYTE_THOUSAND, 1);
    deviceDescriptor->Size = BYTE_THOUSAND;
    if (!DeviceIoControl(diskHandle,    // handle
        IOCTL_STORAGE_QUERY_PROPERTY,   // IO control code
        &storageProtertyQuery,          // in Buffer
        sizeof(storageProtertyQuery),   // buffer size
        deviceDescriptor,               // out buffer 
        BYTE_THOUSAND,                  // out buffer size
        nullptr,                        // Bytes Returned
        nullptr)) {                     // Overlapped
        exitWithError(GetLastError());
    }
    cout << "Model: " << (char*)(deviceDescriptor)+deviceDescriptor->ProductIdOffset << endl;
    cout << "Firmware: " << (char*)(deviceDescriptor)+deviceDescriptor->ProductRevisionOffset << endl;
    cout << "Serial number: " << (char*)(deviceDescriptor)+deviceDescriptor->SerialNumberOffset << endl;
    cout << "Bus type interface: " << busTypeName[deviceDescriptor->BusType] << endl;
    delete deviceDescriptor;
}


void getMemoryTransferMode(HANDLE diskHandle, STORAGE_PROPERTY_QUERY storageProtertyQuery) {
    STORAGE_ADAPTER_DESCRIPTOR adapterDescriptor;
    if (!DeviceIoControl(diskHandle, IOCTL_STORAGE_QUERY_PROPERTY, &storageProtertyQuery, sizeof(storageProtertyQuery),
        &adapterDescriptor, sizeof(STORAGE_DESCRIPTOR_HEADER), nullptr, nullptr)) {
        exitWithError(GetLastError());
    }
    cout << "Transfer mode: " << (adapterDescriptor.AdapterUsesPio ? "DMA" : "PIO") << endl;

}

bool isSsd(HANDLE hDevice) {
    DWORD bytesReturned = 0;
    //Check TRIM -- should be true for SSD
    STORAGE_PROPERTY_QUERY spqTrim;
    spqTrim.PropertyId = StorageDeviceTrimProperty;
    spqTrim.QueryType = PropertyStandardQuery;
    DEVICE_TRIM_DESCRIPTOR dtd;
    if (!DeviceIoControl(hDevice,
        IOCTL_STORAGE_QUERY_PROPERTY,
        &spqTrim,                           // contol code
        sizeof(spqTrim),                    
        &dtd,                               // device output 
        sizeof(dtd),
        &bytesReturned,                     // bytes returned
        nullptr) &&
        bytesReturned == sizeof(dtd)) {
        exitWithError(GetLastError());
    }
    return dtd.TrimEnabled;
}

void exitWithError(DWORD error) {
    cout << "Error = " << error << endl;
    exit(error);
}
