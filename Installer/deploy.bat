xcopy /s /y "..\Polokus.App\bin\x64\Release\net6.0-windows\" ".\bin"
xcopy /s /y "..\Polokus.Service\bin\Release\net6.0" ".\bin"
makensis installer.nsi