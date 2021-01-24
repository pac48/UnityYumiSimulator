@echo off

if DEFINED DESTDIR (
  echo "Destdir.............%DESTDIR%"
  set DESTDIR_ARG="--root=%DESTDIR%"
)

cd "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro"

if NOT EXIST "C:\Program Files (x86)\Project\lib/site-packages\" (
  mkdir "C:\Program Files (x86)\Project\lib/site-packages"
)

set "PYTHONPATH=C:\Program Files (x86)\Project\lib/site-packages;C:/Users/paulg/UnityProjects/project/Assets/ROS/src/build\lib/site-packages"
set "CATKIN_BINARY_DIR=C:/Users/paulg/UnityProjects/project/Assets/ROS/src/build"
for /f "usebackq tokens=*" %%a in ('C:\Program Files (x86)\Project') do (
  set _SETUPTOOLS_INSTALL_PATH=%%~pna
  set _SETUPTOOLS_INSTALL_ROOT=%%~da
)

"C:/opt/ros/noetic/x64/python.exe" ^
    "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro\setup.py" ^
    build --build-base "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/build/xacro" ^
    install %DESTDIR_ARG%  ^
    --prefix="%_SETUPTOOLS_INSTALL_PATH%" ^
    --install-scripts="%_SETUPTOOLS_INSTALL_PATH%\bin" ^
    --root=%_SETUPTOOLS_INSTALL_ROOT%\
