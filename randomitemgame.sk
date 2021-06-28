command /game [<text>]:
    trigger:
        if {game.enable} is true:
            if {game.running} is true:
                if arg-1 is "join":
                    send "&bJoined game random item game"
                    set {game.%player%} to true
                    broadcast "&7%player% has joined the game!"
                if arg-1 is "leave":
                    send "&bJoined game random item game"
                    set {game.%player%} to false
                    broadcast "&7%player% has left the game!"
            else:
                send "&bSorry but there is a game currently going on!"
        else:
            send "&bSorry but there is no game currently going on!"

command /randomitemgame [<text>]:
    permission: op
    trigger:
        if arg-1 is "start":
            broadcast "-----------"
            broadcast " "
            broadcast "&3%player% has started the random game. Do /game join to join!"
            broadcast " "
            broadcast "-----------"
            wait 2 seconds
            broadcast "-----------"
            broadcast " "
            broadcast "&bGame will start in &310 seconds do /game join to join!"
            broadcast " "
            broadcast "-----------"
            set {item} to random items out of all items
            set {game.enable} to true
            set {game.running} to true
            set {_time} to 10
            loop 10 times:
                if {game.running} is true:
                    wait 1 second
                    remove 1 from {_time}
                    send action bar "&bTime untill game starts! &3%{_time}% Seconds!" to all players
            execute player command "/rtp"
            wait 5 seconds
            teleport all players to player
            heal all players 
            feed all players 
            wait 1 seconds
            loop all players:
                if {game.%loop-player%} is true: 
                    send title "&bGet the item: %{item}%" to loop-player
                    wait 3 seconds
                    open chest with 1 rows named "&3%{item}%" to loop-player
                    format gui slot 4 of loop-player with {item} named "&3%{item}%" with lore "&3" and "&bYou need to get this item to win!"
                    

        if arg-1 is "stop":
            broadcast "&3Random item game stopped! by &b%player%"
            delete {item}
            delete {game.running}
            delete {game.enable}
            loop all players:
                delete {game.%loop-player%}
            

command /itemgamepick [<text>] [<item>]:
    permission: op
    trigger:
        if arg-1 is "start":
            broadcast "-----------"
            broadcast " "
            broadcast "&3%player% has started the random game. Do /game join to join!"
            broadcast " "
            broadcast "-----------"
            wait 2 seconds
            broadcast "-----------"
            broadcast " "
            broadcast "&bGame will start in &310 seconds do /game join to join!"
            broadcast " "
            broadcast "-----------"
            set {item} to arg-2
            set {game.enable} to true
            set {game.running} to true
            set {_time} to 10
            loop 10 times:
                if {game.running} is true:
                    wait 1 second
                    remove 1 from {_time}
                    send action bar "&bTime untill game starts! &3%{_time}% Seconds!" to all players
            execute player command "/rtp"
            wait 5 seconds
            teleport all players to player
            heal all players 
            feed all players 
            wait 1 seconds
            loop all players:
                if {game.%loop-player%} is true: 
                    send title "&bGet the item: %{item}%" to loop-player
                    wait 3 seconds
                    open chest with 1 rows named "&3%{item}%" to loop-player
                    format gui slot 4 of loop-player with {item} named "&3%{item}%" with lore "&3" and "&bYou need to get this item to win!"
                    

        if arg-1 is "stop":
            broadcast "&3Random item game stopped! by &b%player%"
            delete {item}
            delete {game.running}
            delete {game.enable}
            loop all players:
                delete {game.%loop-player%}

on pick up:
    if {game.running} is true:
        if event-item is {item}:
            loop all players: 
                if {game.%loop-player%} is true:
                    delete {game.%loop-player%}
                    send "&b%player% &3Has won the game by getting &b[%{item}%&b]" to loop-players
                    set {game.running} to false
                    delete {item}

every second:
    if {game.running} is true:
        loop all players:
            if {game.%loop-player%} is true:
                if loop-player's inventory contains {item}:
                    remove {item} from loop-player's inventory
                    delete {game.%loop-player%}
                    set {game.running} to false
                    send "&b%loop-player% &3Has won the game by getting &b[%{item}%&b]" to loop-players
                    delete {item}

