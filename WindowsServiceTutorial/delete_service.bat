@echo off
REM This command must be run as an administrator
sc delete Service1
if %errorlevel% == 0 (
    echo Service deleted successfully.
) else (
    echo Failed to delete the service. Check if the service name is correct.
)
pause