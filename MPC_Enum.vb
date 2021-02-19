Module MPC_Enum
    ' ======================================================================================
    ' ================= LOOPER CONSTANTS AND ENUMS =========================================
    ' ======================================================================================
    Public ReadOnly INIFile As String = Environment.GetEnvironmentVariable("LocalAppData") & "\Zach Glenwright's Looper 2\MPCLooper.ini"
    Public pausePlaybackOnLoadEvent As Boolean = False ' whether to force pause when loading new events instead of playing them

    Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
    End Enum

    ' ======================================================================================
    ' ================= MPC-HC ENUMS =======================================================
    ' ======================================================================================
    Public Enum CMD_RECEIVED As Integer
        CMD_CONNECT = &H50000000
        CMD_STATE = &H50000001
        CMD_PLAYMODE = &H50000002
        CMD_NOWPLAYING = &H50000003
        CMD_CURRENTPOSITION = &H50000007
        CMD_NOTIFYSEEK = &H50000008
        CMD_DISCONNECT = &H5000000B
    End Enum

    Public Enum CMD_SEND As Integer
        ' ---------------- FILE COMMANDS ----------------
        CMD_OPENFILE = &HA0000000
        CMD_CLOSEFILE = &HA0000002
        ' ---------------- MEDIA COMMANDS ----------------
        CMD_STOP = &HA0000001
        CMD_PLAY = &HA0000004
        CMD_PAUSE = &HA0000005
        CMD_PLAYPAUSE = &HA0000003
        CMD_SETSPEED = &HA0004008
        CMD_TOGGLEFULLSCREEN = &HA0004000
        CMD_CLOSEAPP = &HA0004006
        ' ---------------- INFO COMMANDS ----------------
        CMD_GETNOWPLAYING = &HA0003002
        CMD_GETCURRENTPOSITION = &HA0003004
        CMD_SETPOSITION = &HA0002000
    End Enum

    Public Enum LOADSTATE As Integer
        MLS_CLOSED = 0
        MLS_LOADING = 1
        MLS_LOADED = 2
        MLS_CLOSING = 3
    End Enum

    Public Enum PLAYSTATE As Integer
        PS_PLAY = 0
        PS_PAUSE = 1
        PS_STOP = 2
        PS_UNUSED = 3
    End Enum
End Module
