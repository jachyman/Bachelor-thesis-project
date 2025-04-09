(define (domain sim_movement_domain)

    (:requirements :strips :action-costs)

    (:types
        location
        enemy
    )

    (:predicates
        (enemy_loc ?enemy - enemy ?loc - location)
        (con ?from ?to - location)          ; locations are connected
        (wall ?loc1 ?loc2 - location)       ; wall between locations
        (blocked ?loc - location)           ; location is blocked by enviroment or other enemy
        (current_turn ?enemy - enemy)
        (next_turn ?enemy_1 ?enemy_2)
        (goal_tile ?loc - location)
        (goal_reached ?enemy - enemy)

        ; switch tiles
        (switch_tile ?loc - location)
        (switch_blocked ?loc - location)
        (first_enemy ?enemy - enemy)
        (last_enemy ?enemy - enemy)
        (will_toggle_next_turn)
    )

    (:functions 
        (total-cost) - number
        (switch_counter) - number
        (switch_frequency) - number
    )

    (:action move
        :parameters (?from ?to - location ?enemy ?next_enemy - enemy)
        :precondition (and 
            (or (con ?from ?to) (con ?to ?from)) 
            (enemy_loc ?enemy ?from) 
            (current_turn ?enemy) 
            (not (goal_reached ?enemy))
            (next_turn ?enemy ?next_enemy) 
            (not (blocked ?to)) 
            (not (or (wall ?from ?to) (wall ?to ?from))) 
            )
        :effect (and 
            (enemy_loc ?enemy ?to)
            (not (enemy_loc ?enemy ?from))
            (when (not (goal_tile ?to)) 
                (blocked ?to)
            )
            (when (goal_tile ?to) 
                (goal_reached ?enemy)
            )
            (not (blocked ?from))
            (not (current_turn ?enemy))
            (current_turn ?next_enemy)

            (when (and (last_enemy ?enemy) (not (will_toggle_next_cycle)))
                (increase (switch_counter) 1)
            )

            (increase (total-cost) 1)
        )
    )

    (:action skip_goal_reached_enemy
        :parameters (?enemy ?next_enemy - enemy)
        :precondition (and 
            (next_turn ?enemy ?next_enemy) 
            (goal_reached ?enemy)
            (current_turn ?enemy) 
        )
        :effect (and 
            (not (current_turn ?enemy))
            (current_turn ?next_enemy)
        )
    )

    ; SWITCH
    (:action schedule_toggle
        :parameters (?enemy - enemy)
        :precondition (and 
            (current_turn (?enemy))
            (first_enemy (?enemy))
            (not (will_toggle_next_turn))
            (= (+ (switch_counter) 1) (switch_frequency))
        )
        :effect (and 
            (will_toggle_next_turn)
        )
    )

    (:action execute_toggle
        :parameters (?enemy - enemy)
        :precondition (and 
            (current_turn ?enemy)
            (last_enemy ?enemy)
            (will_toggle_next_turn)
        )
        :effect (and 
            (not (will_toggle_next_turn))
            (forall (?loc - location) 
                when (
                    (switch_tile ?loc)
                    (and
                        (when 
                            (switch_blocked ?loc)
                            (not (switch_blocked ?loc))
                        )
                        (when 
                            (not (switch_blocked ?loc))
                            (switch_blocked ?loc)
                        )
                    )
                )
            )
        )
    )
    
    
)