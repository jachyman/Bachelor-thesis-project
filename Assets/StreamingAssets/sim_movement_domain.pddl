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
        (next_turn ?enemy_1 ?enemy_2 - enemy)
        (goal_tile ?loc - location)
        (goal_reached ?enemy - enemy)
    )

    (:functions (total-cost) - number)

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
)