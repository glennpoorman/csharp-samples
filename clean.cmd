@echo off
setlocal

if exist .vs\nul rmdir /s /q .vs
for /D %%v in (*.*) do call :CLEAN_IT "%%v"
goto :DONE

:CLEAN_IT
if not exist %1 goto NODIR
cd %1
echo Cleaning %1 ...
if exist bin\nul rmdir /s /q bin
if exist obj\nul rmdir /s /q obj
if exist *.csproj.user del *.csproj.user
cd ..
goto :EOF
:NODIR
echo Folder %1 not found.
goto :EOF

:DONE
endlocal
echo.
pause
