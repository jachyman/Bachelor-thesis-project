(define (domain no_wall_triggers_domain)

    (:types
        location
        enemy
    )

    (:predicates
        (enemy_loc ?enemy - enemy ?loc - location)
        (con ?from ?to - location)          ; locations are connected
        (wall ?loc1 ?loc2 - location)       ; wall between locations
        (blocked ?loc - location)           ; location is blocked - agent cannot go there
    )

    (:action move
        :parameters (?from ?to - location ?e - enemy)
        :precondition (and (not (blocked ?to)) (or (con ?from ?to) (con ?to ?from)) (enemy_loc ?e ?from) (not (or (wall ?from ?to) (wall ?to ?from))) )
        :effect (and 
            (enemy_loc ?e ?to)
            (not (enemy_loc ?e ?from))
        )
    )
)