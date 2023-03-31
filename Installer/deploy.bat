xcopy /s /y "..\Polokus.Service\bin\Release\net6.0" ".\bin"
xcopy /s /y "..\Polokus.App\bin\x64\Release\net6.0-windows\win-x64\" ".\bin"
makensis installer.nsi