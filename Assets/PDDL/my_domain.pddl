(define (domain my_domain)

    (:types
        location
    )

    (:predicates
        (at ?loc - location)            
        (con ?from ?to - location)          ; locations are connected
        (wall ?loc1 ?loc2 - location)       ; wall between locations
        (blocked ?loc - location)           ; location is blocked - agent cannot go there
        (wall-trigger ?trigger ?loc1 ?loc2) ; location is a trigger that creates wall between loc1 and loc2
    )

    (:action move
        :parameters (?from ?to - location)
        :precondition (and (not (blocked ?to)) (or (con ?from ?to) (con ?to ?from)) (at ?from) (not (or (wall ?from ?to) (wall ?to ?from))) )
        :effect (and 
            (at ?to) 
            (not (at ?from))
            (forall (?loc1 ?loc2 - location)
                (when (or (wall-trigger ?to ?loc1 ?loc2) (wall-trigger ?to ?loc2 ?loc1))
                    (and (wall ?loc1 ?loc2) (wall ?loc2 ?loc1))
                )
            )
        )
    )
)