@echo off
if "%1" == "" goto Usage
goto Build

:Usage
echo usage: build [TARGET]
echo where: target = one of "test", "release", "clean"
goto End

:Build
%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe ArgumentReflector.msbuild /t:%1

:End