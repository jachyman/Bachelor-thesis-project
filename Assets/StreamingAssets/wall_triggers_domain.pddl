(define (domain wall_triggers_domain)

    (:types
        location
        enemy
    )

    (:predicates
        (enemy_loc ?enemy - enemy ?loc - location)
        (con ?from ?to - location)          ; locations are connected
        (wall ?loc1 ?loc2 - location)       ; wall between locations
        (blocked ?loc - location)           ; location is blocked - agent cannot go there
        (wall-trigger ?trigger ?loc1 ?loc2) ; location is a trigger that creates wall between loc1 and loc2
        (reverse-wall-trigger ?trigger ?loc1 ?loc2) ; location is a trigger that deactivates wall between loc1 and loc2
    )

    (:action move
        :parameters (?from ?to - location ?e - enemy)
        :precondition (and (not (blocked ?to)) (or (con ?from ?to) (con ?to ?from)) (enemy_loc ?e ?from) (not (or (wall ?from ?to) (wall ?to ?from))) )
        :effect (and 
            (enemy_loc ?e ?to)
            (not (enemy_loc ?e ?from))
            (forall (?loc1 ?loc2 - location)
                (and
                    (when (or (wall-trigger ?to ?loc1 ?loc2) (wall-trigger ?to ?loc2 ?loc1))
                        (and (wall ?loc1 ?loc2) (wall ?loc2 ?loc1))
                    )
                    (when (or (reverse-wall-trigger ?to ?loc1 ?loc2) (reverse-wall-trigger ?to ?loc2 ?loc1))
                        (and (not(wall ?loc1 ?loc2)) (not(wall ?loc2 ?loc1)))
                    )
                )
            )
        )
    )
)