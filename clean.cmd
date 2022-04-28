@echo off
setlocal

if exist .vs\nul rmdir /s /q .vs
for /D %%v in (*.*) do (
  pushd %%v
  echo Cleaning %%v ...
  if exist bin\nul rmdir /s /q bin
  if exist obj\nul rmdir /s /q obj
  if exist *.csproj.user del *.csproj.user
  popd
)

endlocal
echo.
pause
