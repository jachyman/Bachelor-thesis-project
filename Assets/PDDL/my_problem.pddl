(define (problem my_problem)

    (:domain my_domain)
    
    (:objects
        a4 b4 c4 d4
        a3 b3 c3 d3
        a2 b2 c2 d2
        a1 b1 c1 d1 - location
    )
    
    (:init 
        (at c1)
        
        (con a4 b4) (con b4 c4) (con c4 d4)
        (con a3 b3) (con b3 c3) (con c3 d3)
        (con a2 b2) (con b2 c2) (con c2 d2)
        (con a1 b1) (con b1 c1) (con c1 d1)
        
        (con a4 a3) (con a3 a2) (con a2 a1)
        (con b4 b3) (con b3 b2) (con b2 b1)
        (con c4 c3) (con c3 c2) (con c2 c1)
        (con d4 d3) (con d3 d2) (con d2 d1)
        
        (blocked b2) (blocked d3) (blocked a4)
        
        (wall c1 c2)
        
        (wall-trigger c2 c2 c3)
    )
    
    (:goal
        (or (at a4) (at b4) (at c4) (at d4))
    )

)