#
# Copyright(c) 2020 The nanoFramework project contributors
# See LICENSE file in the project root for full license information.
#

########################################################################################
# make sure that a valid path is set bellow                                            #
# this is an Interop module so this file should be placed in the CMakes module folder  #
# usually CMake\Modules                                                                #
########################################################################################

# native code directory
set(BASE_PATH_FOR_THIS_MODULE ${PROJECT_SOURCE_DIR}/InteropAssemblies/nanoFramework.Hardware.Esp32.Bluetooth)


# set include directories
list(APPEND nanoFramework.Hardware.Esp32.Bluetooth_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Core)
list(APPEND nanoFramework.Hardware.Esp32.Bluetooth_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Include)
list(APPEND nanoFramework.Hardware.Esp32.Bluetooth_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/HAL/Include)
list(APPEND nanoFramework.Hardware.Esp32.Bluetooth_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/PAL/Include)
list(APPEND nanoFramework.Hardware.Esp32.Bluetooth_INCLUDE_DIRS ${BASE_PATH_FOR_THIS_MODULE})


# source files
set(nanoFramework.Hardware.Esp32.Bluetooth_SRCS

    nanoFramework_Hardware_Esp32_Bluetooth.cpp


    nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_BTLEService_mshl.cpp
    nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_BTLEService.cpp
    nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_BTLEServiceSettings_mshl.cpp
    nanoFramework_Hardware_Esp32_Bluetooth_nanoFramework_Hardware_Esp32_Bluetooth_BTLEServiceSettings.cpp

)

foreach(SRC_FILE ${nanoFramework.Hardware.Esp32.Bluetooth_SRCS})

    set(nanoFramework.Hardware.Esp32.Bluetooth_SRC_FILE SRC_FILE-NOTFOUND)
    find_file(nanoFramework.Hardware.Esp32.Bluetooth_SRC_FILE ${SRC_FILE}
        PATHS
	        "${BASE_PATH_FOR_THIS_MODULE}"
	        "${TARGET_BASE_LOCATION}"

	    CMAKE_FIND_ROOT_PATH_BOTH
    )
# message("${SRC_FILE} >> ${nanoFramework.Hardware.Esp32.Bluetooth_SRC_FILE}") # debug helper
list(APPEND nanoFramework.Hardware.Esp32.Bluetooth_SOURCES ${nanoFramework.Hardware.Esp32.Bluetooth_SRC_FILE})
endforeach()

include(FindPackageHandleStandardArgs)

FIND_PACKAGE_HANDLE_STANDARD_ARGS(nanoFramework.Hardware.Esp32.Bluetooth DEFAULT_MSG nanoFramework.Hardware.Esp32.Bluetooth_INCLUDE_DIRS nanoFramework.Hardware.Esp32.Bluetooth_SOURCES)
