(define (problem my_new_problem)

    (:domain my_domain)
    
    (:objects
        a6 b6 c6 d6 e6 f6 
        a5 b5 c5 d5 e5 f5 
        a4 b4 c4 d4 e4 f4 
        a3 b3 c3 d3 e3 f3 
        a2 b2 c2 d2 e2 f2 
        a1 b1 c1 d1 e1 f1 
         - location
    )
    
    (:init 
        (at d1)
        
        ; column connections
        (con a6 a5) (con a5 a4) (con a4 a3) (con a3 a2) (con a2 a1) 
        (con b6 b5) (con b5 b4) (con b4 b3) (con b3 b2) (con b2 b1) 
        (con c6 c5) (con c5 c4) (con c4 c3) (con c3 c2) (con c2 c1) 
        (con d6 d5) (con d5 d4) (con d4 d3) (con d3 d2) (con d2 d1) 
        (con e6 e5) (con e5 e4) (con e4 e3) (con e3 e2) (con e2 e1) 
        (con f6 f5) (con f5 f4) (con f4 f3) (con f3 f2) (con f2 f1) 
        
        
        ; row connections
        (con a6 b6) (con b6 c6) (con c6 d6) (con d6 e6) (con e6 f6) 
        (con a5 b5) (con b5 c5) (con c5 d5) (con d5 e5) (con e5 f5) 
        (con a4 b4) (con b4 c4) (con c4 d4) (con d4 e4) (con e4 f4) 
        (con a3 b3) (con b3 c3) (con c3 d3) (con d3 e3) (con e3 f3) 
        (con a2 b2) (con b2 c2) (con c2 d2) (con d2 e2) (con e2 f2) 
        (con a1 b1) (con b1 c1) (con c1 d1) (con d1 e1) (con e1 f1) 
        
        
        (blocked d2) (blocked e2) (blocked c2) 
        
        (wall b5 b4) (wall b5 c5) 
    )
    
    (:goal
        (or (at a6) (at b6) (at c6) (at d6) (at e6) (at f6) )
    )

)