#include <FileConstants.au3>
#include <MsgBoxConstants.au3>
#include <WinAPIFiles.au3>
#include <GUIConstantsEx.au3>
#include <file.au3>
#include <array.au3>
#include <GuiListView.au3>
#include <Clipboard.au3>
#include <FF.au3>
#include <FFEx.au3>
Opt("SendKeyDelay", 100) ;5 milliseconds
$time = 5000 ;

taoGmail()

Func taoGmail()

;~ 	;lay so phone
	WriteEmptyFile("paramForcallingCSharp", "4") ;4 la xin c# tao 1 random password
	FileDelete("pvasPassword")
	Run("taoGmailHa.exe", "")
	;cho den khi co pass
	While Not FileExists("pvasPassword")
		Sleep($time)
	 WEnd

	Local $oFF = _FFConnect()
	Local $iCountLines = _FileCountLines("usaname.txt") ; Retrieve the number of lines in the current script.

	_FFPrefSet("general.useragent.override", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:56.0) Gecko/20100101 Firefox/56.0")

	_FFOpenURL("https://accounts.google.com/signUp?service=mail")
	WinActivate("Create your Google Account - Mozilla Firefox")
	_FFSetValueById("firstName", ReadFileAtLine("usaname.txt", Random(1, $iCountLines, 1)))
	_FFSetValueById("lastName", ReadFileAtLine("usaname.txt", Random(1, $iCountLines, 1)))
	$username = ReadFileAtLine("usaname.txt", Random(1, $iCountLines, 1)) & ReadFileAtLine("usaname.txt", Random(1, $iCountLines, 1)) & Randomstring()
	_FFSetValueById("username", $username)
	_FFSetValueByName("Passwd", ReadFileAtLine("pvasPassword",1))
	_FFSetValueByName("ConfirmPasswd", ReadFileAtLine("pvasPassword",1))
	Send("{tab}{tab}{enter}")
	WriteEmptyFile("pvas0", $username)

;~ 	;lay so phone
	WriteEmptyFile("paramForcallingCSharp", "3") ;neu la 1 thi goi lay phone number sms.ru, neu la 2 thi lay forward code gmail,neu la 3 thi goi lay phone number sms.ru API
	FileDelete("phonenumber")
	Run("taoGmailHa.exe", "")
	;cho den khi co phone
	While Not FileExists("phonenumber")
		Sleep($time)
	WEnd

	;dien so phone
  While True
        _FFXPath('/html/body/div[1]/div/div[2]/div[1]/div[2]/form/div[2]/div/div[1]/div/div[2]/div[1]/div/div[1]/input')
        $result = _FFCmd('FFau3.xpath')

        If $result <> '' Then
            ExitLoop
        EndIf
    WEnd
	_FFSetValueById("phoneNumberId", "+"&ReadFileAtLine("phonenumber",1))
	WinActivate("Create your Google Account - Mozilla Firefox")
	_FFCmd('FFau3.xpath.focus()')
	Send("{enter}")

	;lay code
	FileDelete("phonenumber")
	WriteEmptyFile("paramForcallingCSharp", "0") ;neu la 1 thi goi lay phone number sms.ru, neu la 2 thi lay forward code gmail
	While Not FileExists("phonenumber")
		Sleep($time)
	WEnd

;~ 	;nhap code
	  While True
        _FFXPath('/html/body/div[1]/div/div[2]/div[1]/div[2]/form/div[2]/div/div[3]/div/div/div[1]/div/div[1]/input')
        $result = _FFCmd('FFau3.xpath')

        If $result <> '' Then
            ExitLoop
        EndIf
    WEnd
	WinActivate("Create your Google Account - Mozilla Firefox")
	_FFSetValueById("code", ReadFileAtLine("phonenumber",1))
		_FFCmd('FFau3.xpath.focus()')
	Send("{enter}")

;~ 	;dien info sau code, ngay thang nam sinh...
 While True
        _FFXPath('/html/body/div[1]/div/div[2]/div[1]/div[2]/form/div[2]/div/div[1]/div[1]/div[2]/div[1]/div/div[1]/input')
        $result = _FFCmd('FFau3.xpath')

        If $result <> '' Then
            ExitLoop
        EndIf
    WEnd
	WinActivate("Create your Google Account - Mozilla Firefox")
			_FFCmd('FFau3.xpath.focus()')
	Send("^a{del}{tab}getcryptotab.com{ASC 064}gmail.com{tab}j{tab}1{tab}1991{tab}f{tab}{tab}{enter}")

;~ 	;doan agree policy
While True
        _FFXPath('/html/body/div[1]/div/div[2]')
        $result = _FFCmd('FFau3.xpath')

        If $result <> '' Then
            ExitLoop
        EndIf
WEnd
 	WinActivate("Create your Google Account - Mozilla Firefox")
			_FFCmd('FFau3.xpath.focus()')
	Send("{tab}{end}")
	Sleep($time/2)
	Send("{tab 8}{enter}")

;~ ;send forward den mail chu
	Sleep($time * 4)
	_FFOpenURL("https://mail.google.com/mail/?ui=html&zy=h")
While True
        _FFXPath('/html/body/div[2]/form/p/input')
        $result = _FFCmd('FFau3.xpath')

        If $result <> '' Then
            ExitLoop
        EndIf
WEnd
_FFCmd('FFau3.xpath.click()')
Sleep($time)
	_FFLinkClick("Settings", "text")
Sleep($time)
	_FFLinkClick("Forwarding and POP/IMAP", "text")
	_FFClick("addbutton", "id")
	Sleep($time)
	Send("{tab}getcryptotab.com{ASC 064}gmail.com{enter}")
	Sleep($time * 2)
	Send("{tab 2}{enter}")

;~ 	;lay code forward
	WriteEmptyFile("paramForcallingCSharp", "2") ;neu la 1 thi goi lay phone number sms.ru, neu la 2 thi lay forward code gmail
	Sleep($time)
	$sFile = "codeforward"
	$hFile = FileOpen($sFile, 2)
	FileClose($hFile)
	Run("taoGmailHa.exe", "")
	$codeforward = ""
	While $codeforward = ""
		Sleep($time)
		$codeforward = ReadFileAtLine($sFile, 1)
	WEnd

;~ 	;dien code forward

	_FFOpenURL("https://mail.google.com/mail/?ui=html&zy=h")
Sleep($time)
	_FFLinkClick("Settings", "text")
Sleep($time)
	_FFLinkClick("Forwarding and POP/IMAP", "text")

	_FFSetValue($codeforward, "fwvc", "name")
	_FFXPath('/html/body/table[3]/tbody/tr/td[2]/table[1]/tbody/tr/td[2]/div/table/tbody/tr[2]/td[2]/p[3]/table/tbody/tr/td[2]/form[1]/input[6]')
_FFCmd('FFau3.xpath.click()')
Sleep($time*1.5)
_FFXPath('/html/body/table[3]/tbody/tr/td[2]/table[1]/tbody/tr/td[2]/div/table/tbody/tr[2]/td[2]/form[1]/span[2]/label')
_FFCmd('FFau3.xpath.click()')
_FFXPath('/html/body/table[3]/tbody/tr/td[2]/table[1]/tbody/tr/td[2]/div/table/tbody/tr[2]/td[2]/form[1]/input[2]')
_FFCmd('FFau3.xpath.click()')
	;Sleep($time)
	;Send("{tab 35}{down}{tab 3}{enter}")

	;clear ff
	Send("^w")
	Sleep($time / 2)
	Send("{CTRLDOWN}{SHIFTDOWN}{del}{SHIFTup}{CTRLup}")
	Sleep($time / 2)
	Send("{enter}")
EndFunc   ;==>taoGmail

Func WriteEmptyFile($sFilePath, $content)
	; Delete the temporary file.
	FileDelete($sFilePath)


	; Create a temporary file to write data to.
	If Not FileWrite($sFilePath, $content) Then
		MsgBox($MB_SYSTEMMODAL, "", "An error occurred whilst writing the temporary file.")
		Return False
	EndIf

	; Close the handle returned by FileOpen.
	FileClose($sFilePath)

EndFunc   ;==>WriteEmptyFile

Func ReadFileAtLine($sFilePath, $line)
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


EndFunc   ;==>ReadFileAtLine

; Create a random string of text.


Func Randomstring()
	Local $sText = ""
	For $i = 1 To Random(8, 10, 1) ; Return an integer between 5 and 20 to determine the length of the string.
		$sText &= Chr(Random(97, 122, 1)) ; Return an integer between 65 and 122 which represent the ASCII characters between a (lower-case) to Z (upper-case).
	Next
	Return $sText
EndFunc   ;==>Randomstring
