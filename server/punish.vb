options:
    STitem: &7Mainpage
    NSitem: &6nextpage
    VSitem: &7previous page
    GlobalPlayerGuiName: &cPlayers
    pagetext: &6Page

function punishConfirm(p: player, u: offlineplayer, type: text):
    if {punishConfirm::reasonMsg::%{_p}%} is not set:
        set {_reason} to "&cNo reason set!"
    else:
        set {_reason} to {punishConfirm::reasonMsg::%{_p}%}
    if {punishConfirm::time::%{_p}%} is not set:
        set {_time} to "&cNo time set!"
    else:
        set {_time} to {punishConfirm::time::%{_p}%} 
    if {punishConfirm::Broadcast::%{_p}%} is not set:
        set {punishConfirm::Broadcast::%{_p}%} to "Disabled"
    if {punishConfirm::Broadcast::%{_p}%} is "Enabled":
        set {_broadcast} to "&aEnabled"
    else: 
        set {_broadcast} to "&cDisabled"
    set metadata tag "punishConfirm" of {_p} to chest inventory with 3 rows named "&c&l%{_type}% &7• &fConfirmation"
    set slot (integers between 0 and 9) and 9,11,15,17 of metadata tag "punishConfirm" of {_p} to black stained glass pane named "&7" 
    set slot (integers between 18 and 21) of metadata tag "punishConfirm" of {_p} to red stained glass pane named "&cCancel"
    set slot (integers between 23 and 26) of metadata tag "punishConfirm" of {_p} to green stained glass pane named "&aConfirm"
    set slot 10 of metadata tag "punishConfirm" of {_p} to {_u}'s skull named "&f&l%{_u}%"
    set slot 12 of metadata tag "punishConfirm" of {_p} to anvil named "&f&lType: &7%{_type}%"
    set slot 13 of metadata tag "punishConfirm" of {_p} to book named "&f&lReason: &7%{_reason}%" with lore "&7Click to set a reason!"
    set slot 14 of metadata tag "punishConfirm" of {_p} to clock named "&f&lTime: &7%{_time}%"
    set slot 16 of metadata tag "punishConfirm" of {_p} to gray dye named "&f&lBroadcast" with lore "&7Click to %{_broadcast}% &7Broadcasting"
    set slot 22 of metadata tag "punishConfirm" of {_p} to barrier named "&c&lBack"
    open (metadata tag "punishConfirm" of {_p}) to {_p}

function punishTime(p: player, u: offlineplayer, type: text):
    set metadata tag "punishTime" of {_p} to chest inventory with 5 rows named "&c&l%{_type}% &7• &fTimespan"
    set slot 0 and 1,7,8,9,17,27,36,35,37,43,44 of metadata tag "punishTime" of {_p} to black stained glass pane named "&7"
    set slot 2 and 3,5,6,18,26,38,39,41,42 of metadata tag "punishTime" of {_p} to gray stained glass pane named "&7"
    set slot 10 of metadata tag "punishTime" of {_p} to lime wool named "&f&l5 Minutes"
    set slot 19 of metadata tag "punishTime" of {_p} to lime concrete named "&f&l6 Hours"
    set slot 28 of metadata tag "punishTime" of {_p} to lime terracotta named "&f&l4 Days"
    set slot 11 of metadata tag "punishTime" of {_p} to green wool named "&f&l15 Minutes"
    set slot 20 of metadata tag "punishTime" of {_p} to green concrete named "&f&l8 Hours"
    set slot 29 of metadata tag "punishTime" of {_p} to green terracotta named "&f&l6 Days"
    set slot 12 of metadata tag "punishTime" of {_p} to yellow wool named "&f&l30 Minutes"
    set slot 21 of metadata tag "punishTime" of {_p} to yellow concrete named "&f&l10 Hours"
    set slot 30 of metadata tag "punishTime" of {_p} to yellow terracotta named "&f&l8 Days"
    set slot 13 of metadata tag "punishTime" of {_p} to orange wool named "&f&l1 Hour"
    set slot 22 of metadata tag "punishTime" of {_p} to orange concrete named "&f&l12 Hours"
    set slot 31 of metadata tag "punishTime" of {_p} to orange terracotta named "&f&l10 Days"
    set slot 14 of metadata tag "punishTime" of {_p} to purple wool named "&f&l2 Hours"
    set slot 23 of metadata tag "punishTime" of {_p} to purple concrete named "&f&l1 Day"
    set slot 32 of metadata tag "punishTime" of {_p} to purple terracotta named "&f&l12 Days"
    set slot 15 of metadata tag "punishTime" of {_p} to magenta wool named "&f&l3 Hours"
    set slot 24 of metadata tag "punishTime" of {_p} to magenta concrete named "&f&l2 Days"
    set slot 33 of metadata tag "punishTime" of {_p} to magenta terracotta named "&f&l14 Days"
    set slot 16 of metadata tag "punishTime" of {_p} to red wool named "&f&l4 Hours"
    set slot 25 of metadata tag "punishTime" of {_p} to red concrete named "&f&l3 Days"
    set slot 34 of metadata tag "punishTime" of {_p} to red terracotta named "&f&lPermanent"
    set slot 40 of metadata tag "punishTime" of {_p} to barrier named "&c&lBack"
    set slot 4 of metadata tag "punishTime" of {_p} to clock named "&c%{_u}%"
    set {punishTime::type::%{_p}%} to {_type}
    open (metadata tag "punishTime" of {_p}) to {_p}

on inventory click:
    if event-inventory = (metadata "punishTime" of player):
        cancel event
        set {_name} to Uncolored name of slot 4 of metadata tag "punishTime" of player
        set {_p} to {_name} parsed as a offlineplayer
        if index of event-slot is 10 or 11 or 12 or 13 or 14 or 15 or 16 or 19 or 20 or 21 or 22 or 23 or 24 or 25 or 28 or 29 or 30 or 31 or 32 or 33 or 34:
            set {_time} to Uncolored name of slot index of event-slot of metadata tag "punishTime" of player 
            if {_time} is "Permanent":
                set {punishConfirm::time::%player%} to "100000 days" parsed as a timespan
            else:
                set {punishConfirm::time::%player%} to {_time} parsed as a timespan
            if {punishTime::type::%player%} is "ban":
                punishConfirm(player, {_p}, "Ban")
            else if {punishTime::type::%player%} is "mute":
                punishConfirm(player, {_p}, "Mute")
        if index of event-slot is 40:
            execute player command "/punish %{_p}%"
            delete {punishConfirm::time::%player%}
 
command /history [<offlineplayer>]:
    permission: punish.history
    trigger: 
        history(player, arg-1, 0)
        send "&7&o(Created by ThatOneDevil)" to player

function history(p: player, user: offlineplayer, page: integer):
    set {_u} to uuid of {_user}
    set {_pageStart} to 45*{_page}
    set {_s} to 0
    set metadata tag "history" of {_p} to chest inventory with 6 rows named "&c%{_user}% &7• &fHistory"
    loop {punishHistory::%{_u}%::*}:
        (loop-index parsed as integer) > {_pageStart}
        set {_type::*} to Uncolored loop-value split at " "
        if {_type::3} is "warn":
            set slot {_s} of metadata tag "history" of {_p} to paper named "%loop-value%"
        if {_type::3} is "kick":
            set slot {_s} of metadata tag "history" of {_p} to leather boots named "%loop-value%"
        if {_type::3} is "ban":
            set slot {_s} of metadata tag "history" of {_p} to beacon named "%loop-value%"
        if {_type::3} is "mute":
            set slot {_s} of metadata tag "history" of {_p} to barrier named "%loop-value%"
        add 1 to {_s}
        if ({_s}) >= 45:
            exit loop
    set slot 49 of metadata tag "history" of {_p} to {_user}'s skull named "{@STitem}" with lore "{@pagetext}: &7%{_page}%"
    if (size of {punishHistory::%{_u}%::*}) > {_pageStart} + 45:
        set slot 50 of metadata tag "history" of {_p} to ("MHF_ArrowRight" parsed as offline player)'s skull named "{@NSitem}" with lore "&7%{_page}%"
    if {_page} > 0:
        set slot 48 of metadata tag "history" of {_p} to ("MHF_ArrowLeft" parsed as offline player)'s skull named "{@VSitem}" with lore "&7%{_page}%"
    set {punishHistory::user::%{_p}%} to {_u} parsed as a offlineplayer
    delete {_total}
    open (metadata tag "history" of {_p}) to {_p}

inventory click:
    if event-inventory = (metadata "history" of player):
        cancel event
        if index of event-slot = 50:
            set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)+1
            history(player, {punishHistory::user::%player%}, {_invname})
        else if index of event-slot = 48:
            set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)-1
            history(player, {punishHistory::user::%player%}, {_invname})
        else if index of event-slot = 49:
            history(player, {punishHistory::user::%player%}, 0)

on inventory click: 
    if event-inventory = (metadata "punishConfirm" of player):
        cancel event
        set {_name} to Uncolored name of slot 10 of metadata tag "punishConfirm" of player
        set {_p} to {_name} parsed as a offlineplayer
        set {_u} to uuid of {_p}
        set {_type} to Uncolored name of slot 12 of metadata tag "punishConfirm" of player
        set {_type::*} to {_type} split at ": "
        set {_reason} to Uncolored name of slot 13 of metadata tag "punishConfirm" of player
        set {_reason::*} to {_reason} split at ": "
        set {_time} to uncolored name of slot 14 of metadata tag "punishConfirm" of player
        set {_time::*} to {_time} split at ": "

        if index of event-slot is 16:
            if {punishConfirm::Broadcast::%player%} is "Enabled":
                set {punishConfirm::Broadcast::%player%} to "Disabled"
                set slot 16 of metadata tag "punishConfirm" of player to gray dye named "&f&lBroadcast" with lore "&7Click to &cDisabled &7Broadcasting"
            else:
                set {punishConfirm::Broadcast::%player%} to "Enabled"
                set slot 16 of metadata tag "punishConfirm" of player to gray dye named "&f&lBroadcast" with lore "&7Click to &aEnabled &7Broadcasting"
        
        if index of event-slot is 18 or 19 or 20 or 21 or 22:
            if {_type::2} is "Ban" or "Mute":
                delete {punishConfirm::time::%player%}
                execute player command "/punish %{_p}%"
                play sound "UI.STONECUTTER.SELECT_RECIPE" with volume 0.5 and pitch 1 to player
            else:
                execute player command "/punish %{_p}%"
                play sound "UI.STONECUTTER.SELECT_RECIPE" with volume 0.5 and pitch 1 to player
        if index of event-slot is 23 or 24 or 25 or 26:

            if {_type::2} is "kick":
                if {punishConfirm::Broadcast::%player%} is "Enabled":
                    broadcast "&7------------------------"
                    broadcast ""
                    broadcast "&4%player% &chas kicked &4%{_p}%&c for the reason &4%{_reason::2}%"
                    broadcast ""
                    broadcast "&7------------------------"
                    kick {_p} due to "%{_reason::2}%"
                    add "&7(%player%) &7Type: &eKick &7Reason: &6%{_reason::2}%" to {punishHistory::%{_u}%::*}
                    play sound "BLOCK.NOTE_BLOCK.HARP" with volume 0.5 and pitch 1 to all players
                else:
                    kick {_p} due to "%{_reason::2}%"
                    add "&7(%player%) &7Type: &eKick &7Reason: &6%{_reason::2}%" to {punishHistory::%{_u}%::*}
                    send "&4%player% &chas kicked &4%{_p}%&c for the reason &4%{_reason::2}%" to all players where [input has permission "permission.punish"]
                    play sound "BLOCK.NOTE_BLOCK.HARP" with volume 0.5 and pitch 1 to player
                close player's inventory
                delete {punishConfirm::reasonMsg::%player%}

            else if {_type::2} is "warn":
                if {punishConfirm::Broadcast::%player%} is "Enabled":
                    broadcast "&7------------------------"
                    broadcast ""
                    broadcast "&4%player% &chas warned &4%{_p}%&c for the reason &4%{_reason::2}%"
                    broadcast ""
                    broadcast "&7------------------------"
                    add "&7(%player%) &7Type: &eWarn &7Reason: &6%{_reason::2}%" to {punishHistory::%{_u}%::*}
                    send "&4%player% &chas warned &4You&c for the reason &4%{_reason::2}%" to {_p}
                    play sound "BLOCK.NOTE_BLOCK.HARP" with volume 0.5 and pitch 1 to all players
                else:
                    add "&7(%player%) &7Type: &eWarn &7Reason: &6%{_reason::2}%" to {punishHistory::%{_u}%::*}
                    send "&4%player% &chas warned &4You&c for the reason &4%{_reason::2}%" to {_p}
                    send "&4%player% &chas warned &4%{_p}%&c for the reason &4%{_reason::2}%" to all players where [input has permission "permission.punish"]
                    play sound "BLOCK.NOTE_BLOCK.HARP" with volume 0.5 and pitch 1 to player
                close player's inventory
                delete {punishConfirm::reasonMsg::%player%}

            else if {_type::2} is "ban":
                set {_time} to "%{punishConfirm::time::%{_p}%}%" parsed as timespan
                if {punishConfirm::Broadcast::%player%} is "Enabled":
                    broadcast "&7------------------------"
                    broadcast ""
                    broadcast "&4%player% &chas banned &4%{_p}%&c for &4%{_time::2}% &cwith reason &4%{_reason::2}%"
                    broadcast ""
                    broadcast "&7------------------------"
                    play sound "BLOCK.NOTE_BLOCK.HARP" with volume 0.5 and pitch 1 to all players
                    kick {_p} due to "You have been banned by %player% for %{_reason::2}%"
                    ban {_p} due to "You have been banned by %player% for %{_reason::2}%" for {_time}
                    add "&7(%player%) &7Type: &eBan &7Reason: &6%{_reason::2}% &7Time: &e%{_time::2}%" to {punishHistory::%{_u}%::*}
                    delete {punishConfirm::time::%player%}
                    close player's inventory
                else:
                    play sound "BLOCK.NOTE_BLOCK.HARP" with volume 0.5 and pitch 1 to player
                    kick {_p} due to "You have been banned by %player% for %{_reason::2}%"
                    ban {_p} due to "You have been banned by %player% for %{_reason::2}%" for {_time}
                    send "&4%player% &chas banned &4%{_p}%&c for %{_time::2}% &cwith reason &4%{_reason::2}%" to all players where [input has permission "permission.punish"] 
                    add "&7(%player%) &7Type: &eBan &7Reason: &6%{_reason::2}% &7Time: &e%{_time::2}%" to {punishHistory::%{_u}%::*}
                    delete {punishConfirm::time::%player%}
                    close player's inventory

            else if {_type::2} is "mute":
                set {_time} to "%{punishConfirm::time::%player%}%"
                set {_time::*} to {_time} split at " "
                set {_muteSpan} to "%{_time::1}%%{_time::2}%"
                if {punishConfirm::Broadcast::%player%} is "Enabled":
                    broadcast "&7------------------------"
                    broadcast ""
                    broadcast "&4%player% &chas muted &4%{_p}%&c for &4%{_time}% &cwith reason &4%{_reason::2}%"
                    broadcast ""
                    broadcast "&7------------------------"
                    execute player command "/mute %{_p}% %{_muteSpan}%"
                    add "&7(%player%) &7Type: &eMute &7Reason: &6%{_reason::2}% &7Time: &e%{_time::2}%" to {punishHistory::%{_u}%::*}
                    delete {punishConfirm::time::%player%}
                    close player's inventory
                else:
                    play sound "BLOCK.NOTE_BLOCK.HARP" with volume 0.5 and pitch 1 to player
                    execute player command "/mute %{_p}% %{_muteSpan}%"
                    send "&4%player% &chas muted &4%{_p}%&c for %{_time}% &cwith reason &4%{_reason::2}%" to all players where [input has permission "permission.punish"] 
                    add "&7(%player%) &7Type: &eBan &7Reason: &6%{_reason::2}% &7Time: &e%{_time::2}%" to {punishHistory::%{_u}%::*}
                    delete {punishConfirm::time::%player%}
                    close player's inventory

        if index of event-slot is 13:
            close player's inventory
            set {punishConfirm::reason::%player%} to true
            send ""
            send "&4Type the reason you want to set! &7(Type - to cancel)"
            send ""

on chat:
    if {punishConfirm::reason::%player%} is true:
        if message contains "-":
            set {punishConfirm::reason::%player%} to false
            send "&cCanceled reason!"
            open (metadata tag "punishConfirm" of player) to player
            cancel event
        else:
            set {_reason} to message
            cancel event
            set {punishConfirm::reasonMsg::%player%} to {_reason}
            set {punishConfirm::reason::%player%} to false
            open (metadata tag "punishConfirm" of player) to player
            set slot 13 of metadata tag "punishConfirm" of player to book named "&f&lReason: &7%{_reason}%" with lore "&7Click to set a reason!"
            play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player

command /punish [<offlineplayer>]:
    permission: permission.punish
    trigger:
        if arg-1 is not set:
            send "&c&lError! &7Please specify a player! &7&o(Created by ThatOneDevil)"
            play sound "ENTITY.VILLAGER.NO" with volume 0.5 and pitch 1 to player
        else:
            set {_p} to arg-1
            set {_u} to uuid of arg-1
            if {_p} is online:
                set {_status} to "&a&lOnline"
            else:
                set {_status} to "&c&lOffline"
            set {_coords} to {_p}'s location ? "N/A"
            set metadata tag "punishGui" of player to chest inventory with 6 rows named "&cPunish &7• &f%{_p}%"
            set slot 0 and 1,7,8,9,17,36,45,46,44,53,52 of metadata tag "punishGui" of player to black stained glass pane named "&7"
            set slot 2 and 3,4,5,6,11,13,15,18,20,22,24,26,27,28,42,43,47,48,49,50,51,37,38 and (integers between 27 and 35) of metadata tag "punishGui" of player to gray stained glass pane named "&7"
            set slot 19 and 21,23,25 of metadata tag "punishGui" of player to red stained glass pane named "&c"
            set slot 10 of metadata tag "punishGui" of player to redstone named "&f&lWarn" with lore "&f• &7Click here to warn &c%{_p}%" and "&f• &7For a certain reason."
            set slot 12 of metadata tag "punishGui" of player to repeater named "&f&lMute" with lore "&f• &7Click here to mute &c%{_p}%" and "&f• &7For a certain timespan."
            set slot 14 of metadata tag "punishGui" of player to comparator named "&f&lKick" with lore "&f• &7Click here to kick &c%{_p}%" and "&f• &7For a certain reason."
            set slot 16 of metadata tag "punishGui" of player to tnt named "&f&lBan" with lore "&f• &7Click here to ban &c%{_p}%" and "&f• &7For a certain reason & timespan."
            set slot 39 of metadata tag "punishGui" of player to compass named "&f&lTeleport" with lore "&f• &7Coords: &e%{_coords}%"
            set slot 40 of metadata tag "punishGui" of player to {_p}'s skull named "&f&l%{_p}%" with lore "&f• &7Status: %{_status}%" and "&7" and "&f• &7[Left-Click] to see history" and "&f• &7[Right-Click] to see alt accounts"
            set slot 41 of metadata tag "punishGui" of player to chest named "&f&lInventory" with lore "&f• &7[Left-Click] to see inventory" and "&f• &7[Right-Click] to see ender chest"
            open (metadata tag "punishGui" of player) to player
            play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player

on inventory click: 
    if event-inventory = (metadata "punishGui" of player):
        cancel event
        set {_name} to Uncolored name of event-inventory
        set {_player::*} to {_name} split at "• "
        set {_p} to {_player::2} parsed as a offlineplayer
        set {_u} to uuid of {_p}
        if index of event-slot is 41:
            if "%click type%" contains "LEFT":
                if {_p} is not online:
                    send "&cPlayer is not online!"
                    play sound "ENTITY.VILLAGER.NO" with volume 0.5 and pitch 1 to player
                else:
                    close player's inventory
                    wait 2 ticks
                    open inventory of {_p} to player
                    play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player
            if "%click type%" contains "RIGHT":
                if {_p} is not online:
                    send "&cPlayer is not online!"
                    play sound "ENTITY.VILLAGER.NO" with volume 0.5 and pitch 1 to player
                else:
                    close player's inventory
                    wait 2 ticks
                    open ender chest of {_p} to player
                    play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1  to player
        if index of event-slot is 40:
            if "%click type%" contains "LEFT":
                history(player, {_p}, 0)
                play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player
        if index of event-slot is 40:
            if "%click type%" contains "RIGHT":
                execute command "/alts %{_p}%"
                close player's inventory
                play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player
        if index of event-slot is 39:
            teleport player to {_p}
            send "&aSuccessfully teleported to &2%{_p}%"
            play sound "ENTITY_ENDERMAN_TELEPORT" with volume 0.5 and pitch 1 to player
        if index of event-slot is 14:
            punishConfirm(player, {_p}, "Kick")
            play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player
        if index of event-slot is 10:
            punishConfirm(player, {_p}, "Warn")
            play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player
        if index of event-slot is 16:
            if {punishConfirm::time::%player%} is "&cNo time set!":
                punishTime(player, {_p}, "Ban")
            else if {punishConfirm::time::%player%} is not set:
                punishTime(player, {_p}, "Ban")
            else:
                punishConfirm(player, {_p}, "Ban")
                play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player
        if index of event-slot is 12:
            if {punishConfirm::time::%player%} is "&cNo time set!":
                punishTime(player, {_p}, "Mute")
            else if {punishConfirm::time::%player%} is not set:
                punishTime(player, {_p}, "Mute")
            else:
                punishConfirm(player, {_p}, "Mute")
                play sound "ENTITY.EXPERIENCE_ORB.PICKUP" with volume 0.5 and pitch 1 to player

command /clearhistory [<offlineplayer>]:
    permission: permission.clearhistory
    trigger:
        set {_u} to arg-1's uuid
        delete {punishHistory::%{_u}%::*}
        send "&aCleared %arg-1%'s history &7&o(Created by ThatOneDevil)" to player

command /resetVars:
    permission: permission.resetVars
    trigger:
        delete {punishConfirm::*}
        delete {punishConfirm::time::*}
        delete {punishConfirm::Broadcast::*}
        delete {punishConfirm::type::*} 
        delete {punishConfirm::reasonMsg::*}
        delete {punishHistory::*}
        delete {punishTime::type::*}
