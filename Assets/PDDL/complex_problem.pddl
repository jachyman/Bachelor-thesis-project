(define (problem complex_problem)

    (:domain my_domain)
    
    (:objects
        a8 b8 c8 d8 e8 f8 g8 h8 
        a7 b7 c7 d7 e7 f7 g7 h7 
        a6 b6 c6 d6 e6 f6 g6 h6 
        a5 b5 c5 d5 e5 f5 g5 h5 
        a4 b4 c4 d4 e4 f4 g4 h4 
        a3 b3 c3 d3 e3 f3 g3 h3 
        a2 b2 c2 d2 e2 f2 g2 h2 
        a1 b1 c1 d1 e1 f1 g1 h1 
         - location
    )
    
    (:init 
        (at e1)
        
        ; column connections
        (con a8 a7) (con a7 a6) (con a6 a5) (con a5 a4) (con a4 a3) (con a3 a2) (con a2 a1) 
        (con b8 b7) (con b7 b6) (con b6 b5) (con b5 b4) (con b4 b3) (con b3 b2) (con b2 b1) 
        (con c8 c7) (con c7 c6) (con c6 c5) (con c5 c4) (con c4 c3) (con c3 c2) (con c2 c1) 
        (con d8 d7) (con d7 d6) (con d6 d5) (con d5 d4) (con d4 d3) (con d3 d2) (con d2 d1) 
        (con e8 e7) (con e7 e6) (con e6 e5) (con e5 e4) (con e4 e3) (con e3 e2) (con e2 e1) 
        (con f8 f7) (con f7 f6) (con f6 f5) (con f5 f4) (con f4 f3) (con f3 f2) (con f2 f1) 
        (con g8 g7) (con g7 g6) (con g6 g5) (con g5 g4) (con g4 g3) (con g3 g2) (con g2 g1) 
        (con h8 h7) (con h7 h6) (con h6 h5) (con h5 h4) (con h4 h3) (con h3 h2) (con h2 h1) 
        
        
        ; row connections
        (con a8 b8) (con b8 c8) (con c8 d8) (con d8 e8) (con e8 f8) (con f8 g8) (con g8 h8) 
        (con a7 b7) (con b7 c7) (con c7 d7) (con d7 e7) (con e7 f7) (con f7 g7) (con g7 h7) 
        (con a6 b6) (con b6 c6) (con c6 d6) (con d6 e6) (con e6 f6) (con f6 g6) (con g6 h6) 
        (con a5 b5) (con b5 c5) (con c5 d5) (con d5 e5) (con e5 f5) (con f5 g5) (con g5 h5) 
        (con a4 b4) (con b4 c4) (con c4 d4) (con d4 e4) (con e4 f4) (con f4 g4) (con g4 h4) 
        (con a3 b3) (con b3 c3) (con c3 d3) (con d3 e3) (con e3 f3) (con f3 g3) (con g3 h3) 
        (con a2 b2) (con b2 c2) (con c2 d2) (con d2 e2) (con e2 f2) (con f2 g2) (con g2 h2) 
        (con a1 b1) (con b1 c1) (con c1 d1) (con d1 e1) (con e1 f1) (con f1 g1) (con g1 h1) 
        
        
        (blocked a7) (blocked b7) (blocked c7) (blocked d7) (blocked d6) (blocked b5) (blocked g5) (blocked h5) (blocked b4) (blocked e4) (blocked b3) (blocked g3) (blocked b2) (blocked c2) (blocked e2) (blocked g2) 
        
        

        (wall-trigger d2 d5 d4) (wall-trigger d2 f7 f6) (wall-trigger f2 f5 f4) (wall-trigger f2 e7 e6) (wall-trigger g4 f6 f5) (wall-trigger g4 e7 e6) 
    )
    
    (:goal
        (or (at a8) (at b8) (at c8) (at d8) (at e8) (at f8) (at g8) (at h8) )
    )

)