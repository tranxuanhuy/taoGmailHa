#include <FileConstants.au3>
#include <MsgBoxConstants.au3>
#include <WinAPIFiles.au3>
#include <GUIConstantsEx.au3>
#include <file.au3>
#include <array.au3>
#Include <GuiListView.au3>
#include <Clipboard.au3>
#Include <FF.au3>
#Include <FFEx.au3>
Opt("SendKeyDelay", 50) ;5 milliseconds
$time=5000;

taoGmail()

Func taoGmail()
	Sleep(2000)
  Local $oFF= _FFConnect()
Local $iCountLines = _FileCountLines("usaname.txt") ; Retrieve the number of lines in the current script.


   _FFOpenURL("https://accounts.google.com/signUp?service=mail")
   WinActivate("Create your Google Account - Mozilla Firefox")
_FFSetValueById("firstName", ReadFileAtLine("usaname.txt",Random(1, $iCountLines, 1)))
_FFSetValueById("lastName", ReadFileAtLine("usaname.txt",Random(1, $iCountLines, 1)))
$username=ReadFileAtLine("usaname.txt",Random(1, $iCountLines, 1))&ReadFileAtLine("usaname.txt",Random(1, $iCountLines, 1))&Randomstring()
_FFSetValueById("username", $username)
_FFSetValueByName("Passwd", "B1nbin!@#")
_FFSetValueByName("ConfirmPasswd", "B1nbin!@#")
Send("{tab}{tab}{enter}")
WriteEmptyFile("pvas0",$username)

;lay so phone
WriteEmptyFile("paramForcallingCSharp","1") ;neu la 1 thi goi lay phone number sms.ru, neu la 2 thi lay forward code gmail
FileDelete("phonenumber")
Run("taoGmailHa.exe", "")
;cho den khi co phone
While Not FileExists("phonenumber")
Sleep($time)
WEnd

;dien so phone

_FFSetValueById("phoneNumberId", "+"&ReadFileAtLine("phonenumber",1))
WinActivate("Create your Google Account - Mozilla Firefox")
Send("{enter}")

;lay code
FileDelete("phonenumber")
WriteEmptyFile("paramForcallingCSharp","0") ;neu la 1 thi goi lay phone number sms.ru, neu la 2 thi lay forward code gmail
While Not FileExists("phonenumber")
Sleep($time)
WEnd

;nhap code
Sleep($time)
_FFSetValueById("code", ReadFileAtLine("phonenumber",1))
Send("{enter}")

;dien info sau code, ngay thang nam sinh...
Sleep($time)
WinActivate("Create your Google Account - Mozilla Firefox")
Send("^a{del}{tab}getcryptotab.com{ASC 064}gmail.com{tab}j{tab}1{tab}1991{tab}f{tab}{tab}{enter}")

;doan agree policy
Sleep($time)
Send("{tab}{end}{tab 8}{enter}")

;~ ;send forward den mail chu
Sleep($time*4)
_FFOpenURL("https://mail.google.com/mail/?ui=html&zy=h")
Sleep($time)
Send("{tab 3}{enter}")
Sleep($time)
_FFOpenURL("https://mail.google.com/mail/?ui=html&zy=h")
_FFLinkClick("Settings","text")
Sleep($time)
_FFLinkClick("Forwarding and POP/IMAP","text")
_FFClick("addbutton","id")
Sleep($time)
Send("{tab}getcryptotab.com{ASC 064}gmail.com{enter}")
Sleep($time)
Send("{tab 2}{enter}")

;lay code forward
WriteEmptyFile("paramForcallingCSharp","2") ;neu la 1 thi goi lay phone number sms.ru, neu la 2 thi lay forward code gmail
Sleep($time)
$sFile="codeforward"
$hFile = FileOpen($sFile, 2)
FileClose($hFile)
Run("taoGmailHa.exe", "")
$codeforward=""
While $codeforward = ""
	Sleep(5000)
$codeforward=ReadFileAtLine($sFile,1)
WEnd

;dien code forward
_FFOpenURL("https://accounts.google.com/ServiceLogin?service=mail&continue=https://mail.google.com/mail/#identifier")
Sleep($time*2)
_FFOpenURL("https://mail.google.com/mail/?ui=html&zy=h")
Sleep($time)
_FFLinkClick("Settings","text")
Sleep($time)
_FFLinkClick("Forwarding and POP/IMAP","text")
_FFSetValue($codeforward,"fwvc","name")
Send("{tab 37}{enter}")
Sleep($time)
Send("{tab 35}{down}{tab 3}{enter}")

;clear ff
Sleep($time/2)
Send("{CTRLDOWN}{SHIFTDOWN}{del}{SHIFTup}{CTRLup}")
Sleep($time/2)
Send("{enter}")
EndFunc

Func WriteEmptyFile($sFilePath,$content)
	; Delete the temporary file.
    FileDelete($sFilePath)


    ; Create a temporary file to write data to.
    If Not FileWrite($sFilePath, $content) Then
        MsgBox($MB_SYSTEMMODAL, "", "An error occurred whilst writing the temporary file.")
        Return False
    EndIf

    ; Close the handle returned by FileOpen.
    FileClose($sFilePath)

EndFunc   ;==>Example

Func ReadFileAtLine($sFilePath,$line)
	 ; Open the file for reading and store the handle to a variable.
    Local $hFileOpen = FileOpen($sFilePath, $FO_READ)
    If $hFileOpen = -1 Then
        MsgBox($MB_SYSTEMMODAL, "", "An error occurred when reading the file.")
        Return False
    EndIf

    ; Read the fist line of the file using the handle returned by FileOpen.
    Local $sFileRead = FileReadLine($hFileOpen, $line)

    ; Close the handle returned by FileOpen.
    FileClose($hFileOpen)
Return $sFileRead


EndFunc   ;==>Example

; Create a random string of text.


Func Randomstring()
    Local $sText = ""
    For $i = 1 To Random(8, 10, 1) ; Return an integer between 5 and 20 to determine the length of the string.
        $sText &= Chr(Random(97, 122, 1)) ; Return an integer between 65 and 122 which represent the ASCII characters between a (lower-case) to Z (upper-case).
    Next
    Return $sText
EndFunc   ;==>Example
