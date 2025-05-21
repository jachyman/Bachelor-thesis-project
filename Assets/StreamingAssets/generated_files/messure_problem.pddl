(define (problem messure_problem)

    (:domain sim_movement_domain)
    
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
        en1 en2 en3 en4  - enemy
    )
    
    (:init 
        (enemy_loc en1 d2) 
        (enemy_loc en2 e2) 
        (enemy_loc en3 a7) 
        (enemy_loc en4 d5) 
        
        
        (next_turn en1 en2)
        (next_turn en2 en3)
        (next_turn en3 en4)
        (next_turn en4 en1)
        

        (current_turn en1)
        
        ; column connections
        (con a8 a7) (con b8 b7) (con c8 c7) (con d8 d7) (con e8 e7) (con f8 f7) (con g8 g7) (con h8 h7) 
        (con a7 a6) (con b7 b6) (con c7 c6) (con d7 d6) (con e7 e6) (con f7 f6) (con g7 g6) (con h7 h6) 
        (con a6 a5) (con b6 b5) (con c6 c5) (con d6 d5) (con e6 e5) (con f6 f5) (con g6 g5) (con h6 h5) 
        (con a5 a4) (con b5 b4) (con c5 c4) (con d5 d4) (con e5 e4) (con f5 f4) (con g5 g4) (con h5 h4) 
        (con a4 a3) (con b4 b3) (con c4 c3) (con d4 d3) (con e4 e3) (con f4 f3) (con g4 g3) (con h4 h3) 
        (con a3 a2) (con b3 b2) (con c3 c2) (con d3 d2) (con e3 e2) (con f3 f2) (con g3 g2) (con h3 h2) 
        (con a2 a1) (con b2 b1) (con c2 c1) (con d2 d1) (con e2 e1) (con f2 f1) (con g2 g1) (con h2 h1) 
        
        
        
        ; row connections
        (con a8 b8) (con b8 c8) (con c8 d8) (con d8 e8) (con e8 f8) (con f8 g8) (con g8 h8) 
        (con a7 b7) (con b7 c7) (con c7 d7) (con d7 e7) (con e7 f7) (con f7 g7) (con g7 h7) 
        (con a6 b6) (con b6 c6) (con c6 d6) (con d6 e6) (con e6 f6) (con f6 g6) (con g6 h6) 
        (con a5 b5) (con b5 c5) (con c5 d5) (con d5 e5) (con e5 f5) (con f5 g5) (con g5 h5) 
        (con a4 b4) (con b4 c4) (con c4 d4) (con d4 e4) (con e4 f4) (con f4 g4) (con g4 h4) 
        (con a3 b3) (con b3 c3) (con c3 d3) (con d3 e3) (con e3 f3) (con f3 g3) (con g3 h3) 
        (con a2 b2) (con b2 c2) (con c2 d2) (con d2 e2) (con e2 f2) (con f2 g2) (con g2 h2) 
        (con a1 b1) (con b1 c1) (con c1 d1) (con d1 e1) (con e1 f1) (con f1 g1) (con g1 h1) 
        
        
        (blocked d2) (blocked e2) (blocked a7) (blocked d5) 
        
        

        

        (goal_tile g3) 
    )
    
    (:goal
        (and
          (goal_reached en1)
          (goal_reached en2)
          (goal_reached en3)
          (goal_reached en4)
          )
    )

    (:metric minimize (total-cost))
)