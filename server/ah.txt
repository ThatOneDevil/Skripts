options:
    STitem: &7Mainpage
    NSitem: &6nextpage
    VSitem: &7previous page
    pagetext: &6Page

function formatNum(n: number) :: string:
    set {_l::*} to split "k,m,b,t,Qu,Qui,Sx,Sep,Oc,Non,Dec" at ","
    set {_i} to min(floor(log({_n})/3), size of {_l::*})
    return "%{_n}%" if {_i} <= 0
    set {_i2} to 1000^{_i}
    return "%{_n}/{_i2}%%{_l::%{_i}%}%"

function sellitem(p: player, price: integer):
    set {_u} to uuid of {_p}
    set {_i} to {_p}'s held item
    remove {_i} from {_p}'s inventory
    add "&7 &7 &7 &7 &7 &7 &7 &7 &7" to lore of {_i}
    add "&6Price: &e%{_price}%" to lore of {_i}
    add "&6Seller: &e%{_p}%" to lore of {_i}
    add {_i} to {ahitems::*}
    add {_i} to {ahTotalItems::%{_u}%::*}

function ah(p: player, page: integer):
    set {_u} to uuid of {_p}
    set {_pageStart} to 45*{_page}
    set {_s} to 0
    set metadata tag "ahview" of {_p} to chest inventory with 6 rows named "&7• &6Auction House &7(%{_page}%)"
    loop {ahitems::*}:
        (loop-index parsed as integer) > {_pageStart}
        set slot {_s} of metadata tag "ahview" of {_p} to loop-value
        add 1 to {_s}
        if ({_s}) >= 45:
            exit loop
    if (size of {ahitems::*}) > {_pageStart} + 45:
        set slot 50 of metadata tag "ahview" of {_p} to ("MHF_ArrowRight" parsed as offline player)'s skull named "{@NSitem}" with lore "&7%{_page}%"
    else:
        set slot 50 of metadata tag "ahview" of {_p} to barrier named "&cNo page!"     
    if {_page} > 0:
        set slot 48 of metadata tag "ahview" of {_p} to ("MHF_ArrowLeft" parsed as offline player)'s skull named "{@VSitem}" with lore "&7%{_page}%"
    else:
        set slot 48 of metadata tag "ahview" of {_p} to barrier named "&cNo page!"    
    set slot 45 and 46,47,51,52,53 of metadata tag "ahview" of {_p} to gray stained glass pane named "&7"
    set slot 49 of metadata tag "ahview" of {_p} to {_p}'s skull named "&e%{_p}%" with lore "&eMoney &6%formatNum({_p}'s balance)%"
    delete {_total}
    open (metadata tag "ahview" of {_p}) to {_p}

function ahsellgui(p: player, price: number, seller: player, item: item):
    set {_pricething} to "&aBuy this for &2&l%{_price}%"
    set metadata tag "ahsellgui" of {_p} to chest inventory with 3 rows named "&7• &6Auction House Buy"
    set slot 9 and 10,12,14,16,17 of metadata tag "ahsellgui" of {_p} to orange stained glass pane named "&7"
    set slot (integers between 18 and 26) of metadata tag "ahsellgui" of {_p} to orange stained glass pane named "&7"
    set slot (integers between 0 and 8) of metadata tag "ahsellgui" of {_p} to orange stained glass pane named "&7"
    set slot 11 of metadata tag "ahsellgui" of {_p} to green concrete named formatted "%{_pricething}%⛃"
    set slot 15 of metadata tag "ahsellgui" of {_p} to red concrete named formatted "&cCancel"
    set slot 13 of metadata tag "ahsellgui" of {_p} to {_item}
    set {currentItemAh::%{_p}%} to {_item}
    
    open (metadata tag "ahsellgui" of {_p}) to {_p}

on inventory click:
    if event-inventory = (metadata "ahsellgui" of player):
        cancel event
        if index of event-slot is 15:
            ah(player, 0)
        if index of event-slot is 11:
            set {_lore::*} to uncoloured lore of {currentItemAh::%player%}
            loop {_lore::*}:
                if loop-value contains "Seller":
                    set {_seller::*} to loop-value split at ":"
                if loop-value contains "Price":
                    set {_price::*} to loop-value split at ":"
            
            replace all " " in {_seller::*} with ""
            replace all " " in {_price::*} with ""
                
            if player's balance >= ({_price::2} parsed as a number):
                set {_seller} to ({_seller::2} parsed as a offlineplayer)
                set {_su} to uuid of {_seller}
                set {_price} to ({_price::2} parsed as a number)
                
                remove {currentItemAh::%player%} from {ahitems::*}
                remove {currentItemAh::%player%} from {ahTotalItems::%player's uuid%::*}
                remove {_price} from player's balance
                
                add {_price} to {_seller}'s balance
                set {_bal} to formatNum({_price})
                send "&6Auction House &7| &6%player%&e has bought &6%display name of {currentItemAh::%player%} ? {currentItemAh::%player%}%&e for &6%{_bal}%&e ⛃" to {_seller::2} parsed as a offlineplayer
                close player's inventory
                remove "&7 &7 &7 &7 &7 &7 &7 &7 &7" from lore of {currentItemAh::%player%}
                remove "&6Price: &e%{_price}%" from lore of {currentItemAh::%player%}
                remove "&6Seller: &e%{_seller}%" from lore of {currentItemAh::%player%}
                give player {currentItemAh::%player%}
            else:
                send "&c&lError! &cYou do not have enough money to buy this!"
                ah(player, 0)

inventory click:
    if event-inventory = (metadata "ahview" of player):
        cancel event
        if name of event-slot is "{@NSitem}":
            set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)+1
            ah(player, {_invname})
        else if name of event-slot is "{@VSitem}":
            set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)-1
            ah(player, {_invname})
        
        if index of event-slot is not 46 or 47,51,52,53,49,50,48,45:
            if event-slot is air:
                stop
            else:
                set {_lore::*} to uncoloured lore of event-slot
                loop {_lore::*}:
                    if loop-value contains "Seller":
                        set {_seller::*} to loop-value split at ":"
                    if loop-value contains "Price":
                        set {_price::*} to loop-value split at ":"
                replace all " " in {_seller::*} with ""
                replace all " " in {_price::*} with ""
                ahsellgui(player, ({_price::2} parsed as a number), ({_seller::2} parsed as a offlineplayer), event-slot)

function ahTotal(p: offlineplayer):
    set {_u} to uuid of {_p}
    set metadata tag "ahTotalItems" of {_p} to chest inventory with 6 rows named "&7• &6Auction House Item"
    set {_n} to -1
    loop {ahTotalItems::%{_u}%::*}:
        add 1 to {_n}
        set {_currentItem} to loop-value
        add "&7&7&7&7" to lore of {_currentItem}
        add "&cClick to cancel auction and collect item!" to lore of {_currentItem}
        set slot {_n} of metadata tag "ahTotalItems" of {_p} to {_currentItem}
    open (metadata tag "ahTotalItems" of {_p}) to {_p} 

on inventory click:
    if event-inventory = (metadata "ahTotalItems" of player):
        cancel event
        if event-slot is not air:
            set {_lore::*} to uncoloured lore of event-slot
            loop {_lore::*}:
                if loop-value contains "Seller":
                    set {_seller::*} to loop-value split at ":"
                if loop-value contains "Price":
                    set {_price::*} to loop-value split at ":"
            replace all " " in {_seller::*} with ""
            replace all " " in {_price::*} with ""
            set {_seller} to {_seller::2}
            set {_price} to {_price::2}
            if {_seller} is "%player%":
                set {_item} to event-slot
                remove "&7 &7 &7 &7 &7 &7 &7 &7 &7" from lore of {_item}
                remove "&6Price: &e%{_price}%" from lore of {_item}
                remove "&6Seller: &e%{_seller}%" from lore of {_item}
                remove "&7&7&7&7" from lore of {_item}
                remove "&cClick to cancel auction and collect item!" from lore of {_item}
                give player {_item}
                remove {_item} from {ahTotalItems::%{_u}%::*}
                send "&6Auction House &7| &eYou have canceled the auction for &6%display name of {_item} ? {_item}%"
                close player's inventory
                ahTotal(player) 

command /ahreset [<offlineplayer>]:
    permission: op
    trigger:
        set {_p} to player ? arg-1
        set {_u} to uuid of {_p}
        delete {currentItemAh::%{_p}%}
        loop {ahTotalItems::%{_u}%::*}:
            remove loop-value from {ahitems::*}
        send "&6Auction House &7| &eYou have reset &6%arg-1%&e's ah vars"
        delete {ahTotalItems::%{_u}%::*}

command /ah:
    trigger:
        set metadata tag "ahselection" of player to chest inventory with 3 rows named "&7• &6Select an option"
        set slot 9 and 10,16,17,15,11,13 of metadata tag "ahselection" of player to orange stained glass pane named "&7"
        set slot (integers between 18 and 26) of metadata tag "ahselection" of player to orange stained glass pane named "&7"
        set slot (integers between 0 and 8) of metadata tag "ahselection" of player to orange stained glass pane named "&7"
        
        set slot 14 of metadata tag "ahselection" of player to player's skull named "&6Your Auction house items!" with lore "&7" and "&eClick to view all your items you have put on the auction!"
        set slot 12 of metadata tag "ahselection" of player to oak sign named "&6Auction house!" with lore "&7" and "&eClick to view all the items sold on the auction house!"

        open (metadata tag "ahselection" of player) to player

on inventory click:
    if event-inventory = (metadata "ahselection" of player):
        cancel event
        if index of event-slot is 14:
            ahTotal(player)
        if index of event-slot is 12:
            ah(player, 0)

command /ahsell [<integer>]:
    trigger:
        if arg-1 is set:
            if player's held item is air:
                send "&c&lError! Even if you could sell air how would u give it to a player!"
            else:
                send "&6Auction House &7| &eYou have put &6%display name of player's tool ? player's tool%&e on the auction house!"
                sellitem(player, arg-1)
                send "%{ahTotalItems::%uuid of player%::*}%"
        else:
            send "&c&lError! &7Please specify a price!"
