(define (problem two_enemies_problem)

    (:domain two_enemies_domain)
    
    (:objects
        a4 b4 c4 d4
        a3 b3 c3 d3
        a2 b2 c2 d2
        a1 b1 c1 d1 - location
        en1 en2 - enemy
    )
    
    (:init 
        (enemy_loc en1 b1)
        (enemy_loc en2 d1)
        
        (con a4 b4) (con b4 c4) (con c4 d4)
        (con a3 b3) (con b3 c3) (con c3 d3)
        (con a2 b2) (con b2 c2) (con c2 d2)
        (con a1 b1) (con b1 c1) (con c1 d1)
        
        (con a4 a3) (con a3 a2) (con a2 a1)
        (con b4 b3) (con b3 b2) (con b2 b1)
        (con c4 c3) (con c3 c2) (con c2 c1)
        (con d4 d3) (con d3 d2) (con d2 d1)
        
        (blocked a2) (blocked b2) (blocked d2)
        
        (wall c1 c2)
        
        (reverse-wall-trigger a1 c1 c2)
    )
    
    (:goal
        (or
            (enemy_loc en1 a4) (enemy_loc en1 b4) (enemy_loc en1 c4) (enemy_loc en1 d4)
            (enemy_loc en2 a4) (enemy_loc en2 b4) (enemy_loc en2 c4) (enemy_loc en2 d4)
        )
    )

)