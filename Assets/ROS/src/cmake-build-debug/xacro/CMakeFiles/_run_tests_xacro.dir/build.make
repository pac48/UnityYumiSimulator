# CMAKE generated file: DO NOT EDIT!
# Generated by "NMake Makefiles" Generator, CMake Version 3.17

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


.SUFFIXES: .hpux_make_needs_suffix_list


# Command-line flag to silence nested $(MAKE).
$(VERBOSE)MAKESILENT = -s

# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

!IF "$(OS)" == "Windows_NT"
NULL=
!ELSE
NULL=nul
!ENDIF
SHELL = cmd.exe

# The CMake executable.
CMAKE_COMMAND = "D:\Program Files\JetBrains\CLion 2020.2.3\bin\cmake\win\bin\cmake.exe"

# The command to remove a file.
RM = "D:\Program Files\JetBrains\CLion 2020.2.3\bin\cmake\win\bin\cmake.exe" -E rm -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = C:\Users\paulg\UnityProjects\project\Assets\ROS\src

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-debug

# Utility rule file for _run_tests_xacro.

# Include the progress variables for this target.
include xacro\CMakeFiles\_run_tests_xacro.dir\progress.make

_run_tests_xacro: xacro\CMakeFiles\_run_tests_xacro.dir\build.make

.PHONY : _run_tests_xacro

# Rule to build all files generated by this target.
xacro\CMakeFiles\_run_tests_xacro.dir\build: _run_tests_xacro

.PHONY : xacro\CMakeFiles\_run_tests_xacro.dir\build

xacro\CMakeFiles\_run_tests_xacro.dir\clean:
	cd C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-debug\xacro
	$(CMAKE_COMMAND) -P CMakeFiles\_run_tests_xacro.dir\cmake_clean.cmake
	cd C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-debug
.PHONY : xacro\CMakeFiles\_run_tests_xacro.dir\clean

xacro\CMakeFiles\_run_tests_xacro.dir\depend:
	$(CMAKE_COMMAND) -E cmake_depends "NMake Makefiles" C:\Users\paulg\UnityProjects\project\Assets\ROS\src C:\Users\paulg\UnityProjects\project\Assets\ROS\src\xacro C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-debug C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-debug\xacro C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-debug\xacro\CMakeFiles\_run_tests_xacro.dir\DependInfo.cmake --color=$(COLOR)
.PHONY : xacro\CMakeFiles\_run_tests_xacro.dir\depend

