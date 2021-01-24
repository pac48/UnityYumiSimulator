# Install script for directory: C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro

# Set the install prefix
if(NOT DEFINED CMAKE_INSTALL_PREFIX)
  set(CMAKE_INSTALL_PREFIX "C:/Program Files (x86)/Project")
endif()
string(REGEX REPLACE "/$" "" CMAKE_INSTALL_PREFIX "${CMAKE_INSTALL_PREFIX}")

# Set the install configuration name.
if(NOT DEFINED CMAKE_INSTALL_CONFIG_NAME)
  if(BUILD_TYPE)
    string(REGEX REPLACE "^[^A-Za-z0-9_]+" ""
           CMAKE_INSTALL_CONFIG_NAME "${BUILD_TYPE}")
  else()
    set(CMAKE_INSTALL_CONFIG_NAME "Release")
  endif()
  message(STATUS "Install configuration: \"${CMAKE_INSTALL_CONFIG_NAME}\"")
endif()

# Set the component getting installed.
if(NOT CMAKE_INSTALL_COMPONENT)
  if(COMPONENT)
    message(STATUS "Install component: \"${COMPONENT}\"")
    set(CMAKE_INSTALL_COMPONENT "${COMPONENT}")
  else()
    set(CMAKE_INSTALL_COMPONENT)
  endif()
endif()

# Is this installation the result of a crosscompile?
if(NOT DEFINED CMAKE_CROSSCOMPILING)
  set(CMAKE_CROSSCOMPILING "FALSE")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/lib/pkgconfig" TYPE FILE FILES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/installspace/xacro.pc")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/share/xacro/cmake" TYPE FILE FILES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/cmake/xacro-extras.cmake")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/share/xacro/cmake" TYPE FILE FILES
    "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/installspace/xacroConfig.cmake"
    "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/installspace/xacroConfig-version.cmake"
    )
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/share/xacro" TYPE FILE FILES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/package.xml")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  list(APPEND CMAKE_ABSOLUTE_DESTINATION_FILES
   "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/devel/bin/xacro.exe")
  if(CMAKE_WARN_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(WARNING "ABSOLUTE path INSTALL DESTINATION : ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
  if(CMAKE_ERROR_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(FATAL_ERROR "ABSOLUTE path INSTALL DESTINATION forbidden (by caller): ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
file(INSTALL DESTINATION "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/devel/bin" TYPE EXECUTABLE FILES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/windows_wrappers/xacro_xacro.py_exec_devel/xacro.exe")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE EXECUTABLE FILES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/windows_wrappers/xacro_xacro.py_exec_install/xacro.exe")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  include("C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/safe_execute_install.cmake")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/lib/xacro" TYPE PROGRAM FILES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/installspace/xacro.py")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/lib/xacro" TYPE EXECUTABLE FILES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/windows_wrappers/xacro_xacro.py_exec_install_python/xacro.exe")
endif()

if("x${CMAKE_INSTALL_COMPONENT}x" STREQUAL "xUnspecifiedx" OR NOT CMAKE_INSTALL_COMPONENT)
  list(APPEND CMAKE_ABSOLUTE_DESTINATION_FILES
   "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/devel/lib/xacro/xacro.exe")
  if(CMAKE_WARN_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(WARNING "ABSOLUTE path INSTALL DESTINATION : ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
  if(CMAKE_ERROR_ON_ABSOLUTE_INSTALL_DESTINATION)
    message(FATAL_ERROR "ABSOLUTE path INSTALL DESTINATION forbidden (by caller): ${CMAKE_ABSOLUTE_DESTINATION_FILES}")
  endif()
file(INSTALL DESTINATION "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/devel/lib/xacro" TYPE EXECUTABLE FILES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/catkin_generated/windows_wrappers/xacro_xacro.py_exec_cip_devel_python/xacro.exe")
endif()

if(NOT CMAKE_INSTALL_LOCAL_ONLY)
  # Include the install script for each subdirectory.
  include("C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/xacro/test/cmake_install.cmake")

endif()

