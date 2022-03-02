function crateDrop(p: player, crate: text):
    play sound "BLOCK_CHEST_OPEN" with volume 10 to {_p}
    loop {crateDrops::%{_crate}%::*}:
        set {_item} to loop-value
        set {_chance} to uncolored (size of lore of loop-value)th line of lore of {_item}
        replace all "Chance" in {_chance} with ""
        replace all " " in {_chance} with ""
        replace all "%%" in {_chance} with ""
        set {_chance} to ({_chance} parsed as number)

        chance of {_chance}%:
            set {_item} to loop-value
            delete (size of lore of loop-value)th line of lore of {_item}
            wait 1 tick
            send "&e&lCrate! &8➙ %display name of {_item} ? {_item}%" to {_p}
            give {_p} 1 of {_item}

command /givekeys [<player>]:
    permission: givekeys
    trigger:
        set {_p} to arg-1 ? player
        give {_p} tripwire hook named "&bLegendary &6Crate Key"
        give {_p} tripwire hook named "&cGodly &6Crate Key"
        give {_p} tripwire hook named "&2Afk &6Crate Key"

on right click:
    if event-block is diamond block:
        if player is holding tripwire hook named "&bLegendary &6Crate Key":
            cancel event
            crateDrop(player, "legendary")
    if event-block is gold block:
        if player is holding tripwire hook named "&cGodly &6Crate Key":
            cancel event
            crateDrop(player, "godly")
    if event-block is emerald block:
        if player is holding tripwire hook named "&2Afk &6Crate Key":
            cancel event
            crateDrop(player, "afk")

on left click:
    if event-block is diamond block:
        execute player command "/crate legendary"
    if event-block is gold block:
        execute player command "/crate godly"
    if event-block is emerald block:
        execute player command "/crate afk"

command /setchance <number> [<number>]:
    permission:op
    trigger:
        if arg-2 is not set:
            set the ((size of (lore of player's tool)) + 1)th line of the player's tool's lore to "&7Chance &e%arg-1%&6%%"
        else:
            set the arg-2th line of the player's tool's lore to "&7Chance &e%arg-1%&6%%"

on inventory click:
    if uncolored name of player's current inventory contains "Crate":
        if player does not have permission "item.make":
            cancel event

on inventory close:
    if uncolored name of player's current inventory contains "Crate":
        if player does not have permission "item.make":
            stop
        set {_crate::*} to uncolored name of player's current inventory split at " "
        replace all " " in {_crate::2} with ""
        replace all "(" in {_crate::2} with "" 
        replace all ")" in {_crate::2} with ""
        loop 45 times:
            add 1 to {_n}
            set {crateDrops::%{_crate::2}%::%{_n}%} to slot ({_n}-1) of player's current inventory if slot ({_n}-1) of player's current inventory is not air
            delete {crateDrops::%{_crate::2}%::%{_n}%} if slot ({_n}-1) of player's current inventory is air

command /crate <text>:
    trigger:
        open chest inventory with 5 rows named "&7Crate &8(&7%arg-1%&8)" to player
        loop {crateDrops::%arg-1%::*}:
            add 1 to {_m}
            set slot ({_m}-1) of player's current inventory to loop-value
