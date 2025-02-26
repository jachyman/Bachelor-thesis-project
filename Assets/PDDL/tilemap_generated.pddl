(define (problem tilemap_generated)

    (:domain no_domain)
    
    (:objects
        a5 b5 c5 d5 e5 
        a4 b4 c4 d4 e4 
        a3 b3 c3 d3 e3 
        a2 b2 c2 d2 e2 
        a1 b1 c1 d1 e1 
         - location
    )
    
    (:init 
        (at c1)
        
        ; column connections
        (con a5 a4) (con b5 b4) (con c5 c4) (con d5 d4) (con e5 e4) 
        (con a4 a3) (con b4 b3) (con c4 c3) (con d4 d3) (con e4 e3) 
        (con a3 a2) (con b3 b2) (con c3 c2) (con d3 d2) (con e3 e2) 
        (con a2 a1) (con b2 b1) (con c2 c1) (con d2 d1) (con e2 e1) 
        
        
        
        ; row connections
        (con a5 b5) (con b5 c5) (con c5 d5) (con d5 e5) 
        (con a4 b4) (con b4 c4) (con c4 d4) (con d4 e4) 
        (con a3 b3) (con b3 c3) (con c3 d3) (con d3 e3) 
        (con a2 b2) (con b2 c2) (con c2 d2) (con d2 e2) 
        (con a1 b1) (con b1 c1) (con c1 d1) (con d1 e1) 
        
        
        (blocked b5) (blocked d3) (blocked d2) 
        
        (wall d5 d4) (wall b3 a3) (wall e3 e2) 

        
    )
    
    (:goal
        (or (at a5) (at b5) (at c5) (at d5) (at e5) )
    )

)