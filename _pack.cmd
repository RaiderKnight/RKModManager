set target=RKLauncher.zip
set SEVENZIP=%programfiles%\7-Zip\7z.exe
IF EXIST %target% del %target%
"%SEVENZIP%" a %target% .\readme.txt .\bin\Release\RKLauncher.exe

