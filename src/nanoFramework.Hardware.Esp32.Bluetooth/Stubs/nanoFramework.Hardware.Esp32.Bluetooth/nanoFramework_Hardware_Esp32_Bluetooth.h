//-----------------------------------------------------------------------------
//
//                   ** WARNING! ** 
//    This file was generated automatically by a tool.
//    Re-running the tool will overwrite this file.
//    You should copy this file to a custom location
//    before adding any customization in the copy to
//    prevent loss of your changes when the tool is
//    re-run.
//
//-----------------------------------------------------------------------------

#ifndef _NANOFRAMEWORK_HARDWARE_ESP32_BLUETOOTH_H_
#define _NANOFRAMEWORK_HARDWARE_ESP32_BLUETOOTH_H_

#include <nanoCLR_Interop.h>
#include <nanoCLR_Runtime.h>
#include <corlib_native.h>

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_BluetoothHostConfiguration
{
    static const int FIELD__<DeviceName>k__BackingField = 1;
    static const int FIELD__<Mode>k__BackingField = 2;
    static const int FIELD__<Appearance>k__BackingField = 3;
    static const int FIELD__<MaxTransferUnit>k__BackingField = 4;

    //--//

};

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_BluetoothHost
{
    static const int FIELD_STATIC__isDeviceInitialized = 0;

    static const int FIELD__isLocked = 1;
    static const int FIELD__services = 2;
    static const int FIELD__configuration = 3;

    NANOCLR_NATIVE_DECLARE(NativeCheckInterop___STATIC__VOID__STRING);
    NANOCLR_NATIVE_DECLARE(NativeInitializeDevice___STATIC__VOID__STRING__nanoFrameworkHardwareEsp32BluetoothBluetoothMode__I4);

    //--//

};

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_Gatt_AttributeRow
{
    static const int FIELD__<AutoRespond>k__BackingField = 1;
    static const int FIELD__<UUID>k__BackingField = 2;
    static const int FIELD__<Permissions>k__BackingField = 3;
    static const int FIELD__<MaxLength>k__BackingField = 4;
    static const int FIELD__<Value>k__BackingField = 5;

    //--//

};

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_Gatt_GattID
{
    static const int FIELD__<Bytes>k__BackingField = 1;

    //--//

};

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_Gatt_GattCharacteristic
{
    static const int FIELD__ValueChanged = 1;
    static const int FIELD__<Name>k__BackingField = 2;
    static const int FIELD__<UUID>k__BackingField = 3;
    static const int FIELD__<Properties>k__BackingField = 4;

    //--//

};

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_Gatt_GattService
{
    static const int FIELD__<UUID>k__BackingField = 1;
    static const int FIELD__<Characteristics>k__BackingField = 2;

    //--//

};

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_Gatt_GattTable__Row
{
    static const int FIELD__<Handle>k__BackingField = 1;
    static const int FIELD__<Type>k__BackingField = 2;
    static const int FIELD__<Value>k__BackingField = 3;
    static const int FIELD__<Permissions>k__BackingField = 4;

    //--//

};

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_Gatt_GattTable
{
    static const int FIELD__services = 1;
    static const int FIELD__rows = 2;
    static const int FIELD__nextHandle = 3;
    static const int FIELD__configuration = 4;
    static const int FIELD__<NumRows>k__BackingField = 5;

    //--//

};

struct Library_nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_Gatt_TextCharacteristic
{
    static const int FIELD__value = 5;

    //--//

};

extern const CLR_RT_NativeAssemblyData g_CLR_AssemblyNative_nanoFramework_Hardware_Esp32_Bluetooth;

#endif  //_NANOFRAMEWORK_HARDWARE_ESP32_BLUETOOTH_H_
