@echo off
echo Building Parliamentary Question Tracker...
dotnet build PQTracker.vbproj -c Release
echo Build complete. Executable is in bin\Release\net9.0-windows\
pause