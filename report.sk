options:
    STitem: &7Mainpage
    NSitem: &6nextpage
    VSitem: &7previous page
    pagetext: &6Page

function playerReports(p: player, user: offlineplayer, page: integer):
    set {_u} to uuid of {_user}
    set {_pageStart} to 45*{_page}
    set {_s} to 0
    set metadata tag "playerReports" of {_p} to chest inventory with 6 rows named "&7• &cReports of &4%{_user}%"
    loop {playerReports::%{_u}%::*}:
        (loop-index parsed as integer) > {_pageStart}

        set {_report::*} to "%loop-value%" split at ":"
        replace all " " in {_report::2} with ""
        set {_reportby::*} to {_report::1} split at " "

        set slot {_s} of metadata tag "playerReports" of {_p} to paper named "%{_reportby::1}% &3By: %{_reportby::2}%" with lore "&aReason: &c%{_report::2}%"
        add 1 to {_s}
        if ({_s}) >= 45:
            exit loop
    if (size of {playerReports::%{_u}%::*}) > {_pageStart} + 45:
        set slot 50 of metadata tag "playerReports" of {_p} to ("MHF_ArrowRight" parsed as offline player)'s skull named "{@NSitem}" with lore "&7%{_page}%"
    else:
        set slot 50 of metadata tag "playerReports" of {_p} to barrier named "&cNo page!"     
    if {_page} > 0:
        set slot 48 of metadata tag "playerReports" of {_p} to ("MHF_ArrowLeft" parsed as offline player)'s skull named "{@VSitem}" with lore "&7%{_page}%"
    else:
        set slot 48 of metadata tag "playerReports" of {_p} to barrier named "&cNo page!"    
    set slot 45 and 46,47,51,52,53 of metadata tag "playerReports" of {_p} to gray stained glass pane named "&7"
    set slot 49 of metadata tag "playerReports" of {_p} to {_user}'s skull named "&e%{_p}%"
    open (metadata tag "playerReports" of {_p}) to {_p}

inventory click:
    if event-inventory = (metadata "playerReports" of player):
        cancel event
        set {_user::*} to uncoloured name of event-inventory split at "of"
        set {_user} to ({_user::2} parsed as a offlineplayer)
        if name of event-slot is "{@NSitem}":
            set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)+1
            playerReports({_user}, player, {_invname})
        if name of event-slot is "{@VSitem}":
            set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)-1
            playerReports({_user}, player, {_invname})

command /checkreport [<offlineplayer>]:
    permission: checkreport
    trigger:
        playerReports(player, arg-1, 0)

command /report [<player>] [<text>]:
    cooldown: 5 minutes
    cooldown bypass: reportbypass
    cooldown message: &cReplace wait 5 minutes between each report!
    trigger:
        if arg-1 is set:
            if arg-2 is set:
                set {_id} to size of {playerReports::%player's uuid%::*}+1
                add "&c##%{_id}% &b%player%: &7%arg-2%" to {playerReports::%player's uuid%::*}
                send title "&3The player has been reported" with subtitle "&3Please wait untill a &bstaff&3 checks it out"
                send "&3Be aware if its a &bfalse &breport/spam report&3 you will be sanctioned!"
                send formatted "<command:/checkreport %arg-1%>&b%player%&3 has reported &b%arg-1% &7&o(Click here to check it out!)<reset>" to all players where [player input has permission "checkreport"]

command /clearreport [<offlineplayer>]:
    permission: clearreport
    trigger:
        send "&cCleared the reports of &4%arg-1%"
        delete {playerReports::%arg-1's uuid%::*}
