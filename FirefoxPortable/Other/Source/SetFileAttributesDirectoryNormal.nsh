; SetFileAttributesDirectoryNormal v 1.1
; Sets all the files in a given directory (and sub directories) to NORMAL attributes
;
; Usage: ${SetFileAttributesDirectoryNormal} DIRECTORY_TO_SET_NORMAL
;
; Macro and Define added by John T. Haller of PortableApps.com
;
; Uses function Attrib v1.1
; http://nsis.sourceforge.net/Attrib
; By: Hendri Adriaens (HendriAdriaens@hotmail.com)
; Additions by hobbyscripter to enable recursion of sub-directories
; BSD License

Function SetFileAttributesDirectoryNormal
	Exch $1 ; Dir
	Push $2
	Push $3
	FindFirst $2 $3 "$1\*.*"
	StrCmp $3 "" exitloop
	
	loop:
		StrCmp $3 "" exitloop
		StrCmp $3 "." next
		StrCmp $3 ".." next
		IfFileExists "$1\$3\*.*" 0 +4
			Push "$1\$3"
			Call SetFileAttributesDirectoryNormal
			Goto next
		; SetFileAttributes does not accept variables as attribute,
		; so manually set this to the necessary value.
		SetFileAttributes "$1\$3" NORMAL
		
	next:
		FindNext $2 $3
		Goto loop
		
	exitloop:
		FindClose $2
		Pop $3
		Pop $2
		Pop $1
FunctionEnd

!macro SetFileAttributesDirectoryNormal DIRECTORY_TO_SET_NORMAL
  Push `${DIRECTORY_TO_SET_NORMAL}`
  Call SetFileAttributesDirectoryNormal
!macroend

!define SetFileAttributesDirectoryNormal '!insertmacro "SetFileAttributesDirectoryNormal"'