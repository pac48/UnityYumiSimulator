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
CMAKE_BINARY_DIR = C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release

# Utility rule file for tests.

# Include the progress variables for this target.
include CMakeFiles\tests.dir\progress.make

tests: CMakeFiles\tests.dir\build.make

.PHONY : tests

# Rule to build all files generated by this target.
CMakeFiles\tests.dir\build: tests

.PHONY : CMakeFiles\tests.dir\build

CMakeFiles\tests.dir\clean:
	$(CMAKE_COMMAND) -P CMakeFiles\tests.dir\cmake_clean.cmake
.PHONY : CMakeFiles\tests.dir\clean

CMakeFiles\tests.dir\depend:
	$(CMAKE_COMMAND) -E cmake_depends "NMake Makefiles" C:\Users\paulg\UnityProjects\project\Assets\ROS\src C:\Users\paulg\UnityProjects\project\Assets\ROS\src C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release\CMakeFiles\tests.dir\DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles\tests.dir\depend

