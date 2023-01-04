; Includes ---------------------------------
!include MUI2.nsh
!include 'LogicLib.nsh'

!define MUI_ABORTWARNING
!define VERSION "v1.0"


; Settings ---------------------------------
Name "Polokus"
OutFile "Polokus-${VERSION}-setup.exe"
RequestExecutionLevel admin
InstallDir "$PROGRAMFILES\Polokus"

!define INST_SOURCE_DIR ".\bin"

; Pages ------------------------------------

!define MUI_WELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Wizard\win.bmp"
!insertmacro MUI_PAGE_WELCOME 

!define MUI_COMPONENTSPAGE_SMALLDESC
!insertmacro MUI_PAGE_COMPONENTS

!insertmacro MUI_PAGE_DIRECTORY

!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

; Languages --------------------------------
!insertmacro MUI_LANGUAGE "English"

; Sections ---------------------------------
Section "!Polokus" SecCore
SectionIn RO

  SetOutPath "$INSTDIR"
  File /r "${INST_SOURCE_DIR}"

  WriteUninstaller "$INSTDIR\uninstall.exe"

SectionEnd

Section "Create desktop icon" section_shortcut

  CreateShortCut "$DESKTOP\Polokus.lnk" "$INSTDIR\bin\Polokus.App.exe" \
    "" "$INSTDIR\bin\Polokus.ico" 0 SW_SHOWNORMAL

SectionEnd

Section "Register Polokus Service" section_service

  Exec '"$INSTDIR\bin\Polokus.Service.exe" install start'

SectionEnd

Section Uninstall
  Exec '"$INSTDIR\bin\Polokus.Service.exe" uninstall'
  RMDir /r "$INSTDIR"
SectionEnd


; Descriptions -----------------------------
LangString DESC_SecCore ${LANG_English}              "Install whole components of Polokus system."
LangString DESC_section_shortcut ${LANG_English}     "Create desktop icon to run Polokus.App."
LangString DESC_section_service ${LANG_English}      "Register Polokus.Service as a Windows Service and runs it."

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
	!insertmacro MUI_DESCRIPTION_TEXT ${SecCore}              $(DESC_SecCore)
	!insertmacro MUI_DESCRIPTION_TEXT ${section_shortcut}     $(DESC_section_shortcut)
	!insertmacro MUI_DESCRIPTION_TEXT ${section_service}      $(DESC_section_service)
!insertmacro MUI_FUNCTION_DESCRIPTION_END
