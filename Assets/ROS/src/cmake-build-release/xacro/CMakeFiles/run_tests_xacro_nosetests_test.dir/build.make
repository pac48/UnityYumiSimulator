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

# Utility rule file for run_tests_xacro_nosetests_test.

# Include the progress variables for this target.
include xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\progress.make

xacro\CMakeFiles\run_tests_xacro_nosetests_test:
	cd C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release\xacro
	call ..\catkin_generated\env_cached.bat C:/opt/ros/noetic/x64/python.exe C:/opt/ros/noetic/x64/share/catkin/cmake/test/run_tests.py C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/test_results/xacro/nosetests-test.xml "\"D:/Program Files/JetBrains/CLion 2020.2.3/bin/cmake/win/bin/cmake.exe\" -E make_directory C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/test_results/xacro" "C:/opt/ros/noetic/x64/Scripts/nosetests.exe -P --process-timeout=60 --where=C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test --with-xunit --xunit-file=C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-release/test_results/xacro/nosetests-test.xml"
	cd C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release

run_tests_xacro_nosetests_test: xacro\CMakeFiles\run_tests_xacro_nosetests_test
run_tests_xacro_nosetests_test: xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\build.make

.PHONY : run_tests_xacro_nosetests_test

# Rule to build all files generated by this target.
xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\build: run_tests_xacro_nosetests_test

.PHONY : xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\build

xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\clean:
	cd C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release\xacro
	$(CMAKE_COMMAND) -P CMakeFiles\run_tests_xacro_nosetests_test.dir\cmake_clean.cmake
	cd C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release
.PHONY : xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\clean

xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\depend:
	$(CMAKE_COMMAND) -E cmake_depends "NMake Makefiles" C:\Users\paulg\UnityProjects\project\Assets\ROS\src C:\Users\paulg\UnityProjects\project\Assets\ROS\src\xacro C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release\xacro C:\Users\paulg\UnityProjects\project\Assets\ROS\src\cmake-build-release\xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\DependInfo.cmake --color=$(COLOR)
.PHONY : xacro\CMakeFiles\run_tests_xacro_nosetests_test.dir\depend
