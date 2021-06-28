options:
    STitem: &bMainpage
    NSitem: &aNextpage
    VSitem: &cPrevious Page
    pagetext: &bPage
 
on pickup:
    set {_p} to player
    if {items.%{_p}%::*} does not contain event-item:
        if {total_items.%{_p}%::*} contains event-item:
            add event-item to {items.%{_p}%::*}
            broadcast "&b%player% has collected the item &3[%event-item%]"
            remove event-item from {total_items.%{_p}%::*}
            add 1 to {total.number.collected::%player%}

command collectedItems:
    trigger:
        Menu(player, 0)


function Menu(p: player, page: number):
    set {_pageStart} to 45*{_page}
    set {_s} to 0
    set metadata tag "Menu" of {_p} to chest inventory with 6 rows named "&bTotal items collected"
    loop {items.%{_p}%::*}:
        (loop-index parsed as integer) > {_pageStart}
        set slot {_s} of metadata tag "Menu" of {_p} to loop-value
        add 1 to {_s}
        if ({_s}) >= 45:
            exit loop
    set slot 49 of metadata tag "Menu" of {_p} to {_p}'s skull named "{@STitem}" with lore "{@pagetext}: &7%{_page}%/%round(size of {GlobalPlayer::*}/(9*6))%"
    if (size of {items.%{_p}%::*}) > {_pageStart} + 45:
        set slot 50 of metadata tag "Menu" of {_p} to ("MHF_ArrowRight" parsed as offline player)'s skull named "{@NSitem}" with lore "&7%{_page}%"
    if {_page} > 0:
        set slot 48 of metadata tag "Menu" of {_p} to ("MHF_ArrowLeft" parsed as offline player)'s skull named "{@VSitem}" with lore "&7%{_page}%"
    open (metadata tag "Menu" of {_p}) to {_p}


inventory click:
    event-inventory != player' inventory:
        if event-inventory = (metadata tag "Menu" of player):
            cancel event
            event-slot != air:
                if index of event-slot = 50:
                    set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)+1
                    Menu(player, {_invname})
                else if index of event-slot = 48:
                    set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)-1
                    Menu(player, {_invname})
                else if index of event-slot = 49:
                    Menu(player, 0)


command ItemsMenu:
    trigger:
        Menu2(player, 0)

 
function Menu2(p: player, page: number):
    set {_pageStart} to 45*{_page}
    set {_s} to 0
    set metadata tag "Menu22" of {_p} to chest inventory with 6 rows named "&n&bTotal Items"
    loop {total_items.%{_p}%::*}:
        (loop-index parsed as integer) > {_pageStart}
        set slot {_s} of metadata tag "Menu22" of {_p} to loop-value
        add 1 to {_s}
        if ({_s}) >= 45:
            exit loop
    set slot 49 of metadata tag "Menu22" of {_p} to {_p}'s skull named "{@STitem}" with lore "{@pagetext}: &7%{_page}%/%round(size of {GlobalPlayer::*}/(9*6))%"
    if (size of {total_items.%{_p}%::*}) > {_pageStart} + 45:
        set slot 50 of metadata tag "Menu22" of {_p} to ("MHF_ArrowRight" parsed as offline player)'s skull named "{@NSitem}" with lore "&7%{_page}%"
    if {_page} > 0:
        set slot 48 of metadata tag "Menu22" of {_p} to ("MHF_ArrowLeft" parsed as offline player)'s skull named "{@VSitem}" with lore "&7%{_page}%"
    open (metadata tag "Menu22" of {_p}) to {_p}


inventory click:
    event-inventory != player' inventory:
        if event-inventory = (metadata tag "Menu22" of player):
            cancel event
            event-slot != air:
                if index of event-slot = 50:
                    set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)+1
                    Menu2(player, {_invname})
                else if index of event-slot = 48:
                    set {_invname} to (uncoloured line 1 of lore of event-slot parsed as integer)-1
                    Menu2(player, {_invname})
                else if index of event-slot = 49:
                    Menu2(player, 0)

command /reset-stuff:
    permission: op
    trigger:
        loop all players:
            delete {items.%loop-player%::*} 
            set {total_items.%loop-player%::*} to all items
            delete {total.number.collected.%loop-player%}

command /TotalItems [<player>]:
    trigger:
        if arg-1 is set:
            send "&bTotal items Collected by &b%arg-1%: &3%{total.number.collected::%arg-1%} ? 0%"
        else:
            send "&bTotal items Collected by &byou: &3%{total.number.collected::%player%} ? 0%"

command /itemstop:
	trigger:
		loop {total.number.collected::*}:
			add 1 to {_size}
			if {_low.to.high.list::%loop-value%} is not set:
				set {_low.to.high.list::%loop-value%} to loop-index
			else:
				set {_n} to 0
				loop {_size} times:
					set {_n} to {_n}+1
					{_low.to.high.list::%loop-value-1%.%{_n}%} is not set
					set {_low.to.high.list::%loop-value-1%.%{_n}%} to loop-index
					stop loop
		wait 1 tick
		set {_n} to size of {_low.to.high.list::*}
		loop {_low.to.high.list::*}:
			set {_high.to.low.list::%{_n}%} to loop-value
			set {_n} to {_n}-1
		wait 1 tick
		set {_i} to 0
		send ""
		send "&b&lItems TOP"
		send ""
		set {_wownum} to 2
		loop {_high.to.low.list::*}:
			add 1 to {_topnumber}
			set {_player} to "%loop-value%" parsed as offlineplayer
			set {_b::%loop-value%} to {total.number.collected::%loop-value%}
			set {_b::%loop-value%} to round {_b::%loop-value%}
			set {_m::%loop-value%} to "%{_b::%loop-value%}%"
			if length of "%{_b::%loop-value%}%" is greater than 4:
				set {_m::%loop-value%} to the first 2 characters of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%k"
			if length of "%{_b::%loop-value%}%" is greater than 5:
				set {_m::%loop-value%} to the first 3 characters of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%k"
			if length of "%{_b::%loop-value%}%" is greater than 6:
				set {_m::%loop-value%} to the first character of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%M"
			if length of "%{_b::%loop-value%}%" is greater than 7:
				set {_m::%loop-value%} to the first 2 characters of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%M"
			if length of "%{_b::%loop-value%}%" is greater than 8:
				set {_m::%loop-value%} to the first 3 characters of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%M"
			if length of "%{_b::%loop-value%}%" is greater than 9:
				set {_m::%loop-value%} to the first character of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%B"
			if length of "%{_b::%loop-value%}%" is greater than 10:
				set {_m::%loop-value%} to the first 2 characters of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%B"
			if length of "%{_b::%loop-value%}%" is greater than 11:
				set {_m::%loop-value%} to the first 3 characters of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%B"
			if length of "%{_b::%loop-value%}%" is greater than 12:
				set {_m::%loop-value%} to the first character of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%T"
			if length of "%{_b::%loop-value%}%" is greater than 13:
				set {_m::%loop-value%} to the first 2 characters of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%T"
			if length of "%{_b::%loop-value%}%" is greater than 14:
				set {_m::%loop-value%} to the first 3 characters of "%{_b::%loop-value%}%"
				set {_m::%loop-value%} to "%{_m::%loop-value%}%T"
			if {_topnumber} is equal to 1:
				send "&3%{_topnumber}% &8| &f%{_player}% &8» &a%{_m::%loop-value%}%"
				execute console command "/holo setline baltop %{_wownum}% &3%{_topnumber}% &8| &f%{_player}% &8» &a$%{_m::%loop-value%}%"
			if {_topnumber} is equal to 2:
				send "&3%{_topnumber}% &8| &f%{_player}% &8» &a%{_m::%loop-value%}%"
				execute console command "/holo setline baltop %{_wownum}% &3%{_topnumber}% &8| &f%{_player}% &8» &a$%{_m::%loop-value%}%"
			if {_topnumber} is equal to 3:
				send "&3%{_topnumber}% &8| &f%{_player}% &8» &a%{_m::%loop-value%}%"
				execute console command "/holo setline baltop %{_wownum}% &3%{_topnumber}% &8| &f%{_player}% &8» &a$%{_m::%loop-value%}%"
			if {_topnumber} is greater than 3:
				send "&7%{_topnumber}% &8| &7%{_player}% &8» &a%{_m::%loop-value%}%"
				execute console command "/holo setline baltop %{_wownum}% &7%{_topnumber}% &8| &7%{_player}% &8» &a$%{_m::%loop-value%}%"
			add 1 to {_i}
			add 1 to {_wownum}
			if {_topnumber} is greater than 9: #this is top 10 you can change it
				send ""
				exit
