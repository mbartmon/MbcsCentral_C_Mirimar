
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
Severity=Info
Facility=Runtime
SymbolicName=MSG_INFO
Language=English
Info Message%n
.

MessageId=0x2
Severity=Config
Facility=Runtime
Language=English
Application Configuration%n
.

MessageId=0x3
Severity=Warning
Facility=Runtime
Language=English
Application Warning%n
.

MessageId=0x4
Severity=Error
Facility=Runtime
Language=English
Application Error%n
.

MessageId=0x5
Severity=Info
Facility=Runtime
Language=English
Debug Message%n
.
