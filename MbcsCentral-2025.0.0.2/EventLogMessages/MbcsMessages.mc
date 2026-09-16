
MessageIdTypedef=DWORD

SeverityNames=(
Info=0x0:STATUS_INFORMATION
Config=0x1:STATUS_CONFIGURATION
Warning=0x2:STATUS_WARNING
Error=0x3:STATUS_ERROR
)

FacilityNames=(Runtime=0x0:FACILITY_RUNTIME
)

LanguageNames=(English=0x409:MSG00409)

;////////////////////////////////////////
;// Events
;//

MessageId=0x1
SymbolicName=MSG_INFO
Language=English
Info Message%n
.

MessageId=0x2
SymbolicName=MSG_CONFIG
Language=English
Application Configuration%n
.

MessageId=0x3
SymbolicName=MSG_WARNING
Language=English
Application Warning%n
.

MessageId=0x4
SymbolicName=MSG_ERROR
Language=English
Application Error%n
.

MessageId=0x5
SymbolicName=MSG_DEBUG
Language=English
Debug Message%n
.
