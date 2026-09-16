////////////////////////////////////////
// Events
//
//
//  Values are 32 bit values laid out as follows:
//
//   3 3 2 2 2 2 2 2 2 2 2 2 1 1 1 1 1 1 1 1 1 1
//   1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0
//  +---+-+-+-----------------------+-------------------------------+
//  |Sev|C|R|     Facility          |               Code            |
//  +---+-+-+-----------------------+-------------------------------+
//
//  where
//
//      Sev - is the severity code
//
//          00 - Success
//          01 - Informational
//          10 - Warning
//          11 - Error
//
//      C - is the Customer code flag
//
//      R - is a reserved bit
//
//      Facility - is the facility code
//
//      Code - is the facility's status code
//
//
// Define the facility codes
//
#define FACILITY_RUNTIME                 0x0


//
// Define the severity codes
//
#define STATUS_WARNING                   0x2
#define STATUS_INFORMATION               0x0
#define STATUS_ERROR                     0x3
#define STATUS_CONFIGURATION             0x1


//
// MessageId: MSG_INFO
//
// MessageText:
//
// Info Message%n
//
#define MSG_INFO                         ((DWORD)0x00000001L)

//
// MessageId: 0x40000002L (No symbolic name defined)
//
// MessageText:
//
// Application Configuration%n
//


//
// MessageId: 0x80000003L (No symbolic name defined)
//
// MessageText:
//
// Application Warning%n
//


//
// MessageId: 0xC0000004L (No symbolic name defined)
//
// MessageText:
//
// Application Error%n
//


//
// MessageId: 0x00000005L (No symbolic name defined)
//
// MessageText:
//
// Debug Message%n
//


