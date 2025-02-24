begin_version
3
end_version
begin_metric
0
end_metric
4
begin_variable
var0
-1
13
Atom enemy_loc(en1, a1)
Atom enemy_loc(en1, a3)
Atom enemy_loc(en1, a4)
Atom enemy_loc(en1, b1)
Atom enemy_loc(en1, b3)
Atom enemy_loc(en1, b4)
Atom enemy_loc(en1, c1)
Atom enemy_loc(en1, c2)
Atom enemy_loc(en1, c3)
Atom enemy_loc(en1, c4)
Atom enemy_loc(en1, d1)
Atom enemy_loc(en1, d3)
Atom enemy_loc(en1, d4)
end_variable
begin_variable
var1
-1
2
Atom wall(c1, c2)
NegatedAtom wall(c1, c2)
end_variable
begin_variable
var2
-1
13
Atom enemy_loc(en2, a1)
Atom enemy_loc(en2, a3)
Atom enemy_loc(en2, a4)
Atom enemy_loc(en2, b1)
Atom enemy_loc(en2, b3)
Atom enemy_loc(en2, b4)
Atom enemy_loc(en2, c1)
Atom enemy_loc(en2, c2)
Atom enemy_loc(en2, c3)
Atom enemy_loc(en2, c4)
Atom enemy_loc(en2, d1)
Atom enemy_loc(en2, d3)
Atom enemy_loc(en2, d4)
end_variable
begin_variable
var3
0
2
Atom new-axiom@0()
NegatedAtom new-axiom@0()
end_variable
0
begin_state
3
0
10
1
end_state
begin_goal
1
3 0
end_goal
60
begin_operator
move a1 b1 en1
0
1
0 0 0 3
1
end_operator
begin_operator
move a1 b1 en2
0
1
0 2 0 3
1
end_operator
begin_operator
move a3 a4 en1
0
1
0 0 1 2
1
end_operator
begin_operator
move a3 a4 en2
0
1
0 2 1 2
1
end_operator
begin_operator
move a3 b3 en1
0
1
0 0 1 4
1
end_operator
begin_operator
move a3 b3 en2
0
1
0 2 1 4
1
end_operator
begin_operator
move a4 a3 en1
0
1
0 0 2 1
1
end_operator
begin_operator
move a4 a3 en2
0
1
0 2 2 1
1
end_operator
begin_operator
move a4 b4 en1
0
1
0 0 2 5
1
end_operator
begin_operator
move a4 b4 en2
0
1
0 2 2 5
1
end_operator
begin_operator
move b1 a1 en1
0
2
0 0 3 0
0 1 -1 1
1
end_operator
begin_operator
move b1 a1 en2
0
2
0 2 3 0
0 1 -1 1
1
end_operator
begin_operator
move b1 c1 en1
0
1
0 0 3 6
1
end_operator
begin_operator
move b1 c1 en2
0
1
0 2 3 6
1
end_operator
begin_operator
move b3 a3 en1
0
1
0 0 4 1
1
end_operator
begin_operator
move b3 a3 en2
0
1
0 2 4 1
1
end_operator
begin_operator
move b3 b4 en1
0
1
0 0 4 5
1
end_operator
begin_operator
move b3 b4 en2
0
1
0 2 4 5
1
end_operator
begin_operator
move b3 c3 en1
0
1
0 0 4 8
1
end_operator
begin_operator
move b3 c3 en2
0
1
0 2 4 8
1
end_operator
begin_operator
move b4 a4 en1
0
1
0 0 5 2
1
end_operator
begin_operator
move b4 a4 en2
0
1
0 2 5 2
1
end_operator
begin_operator
move b4 b3 en1
0
1
0 0 5 4
1
end_operator
begin_operator
move b4 b3 en2
0
1
0 2 5 4
1
end_operator
begin_operator
move b4 c4 en1
0
1
0 0 5 9
1
end_operator
begin_operator
move b4 c4 en2
0
1
0 2 5 9
1
end_operator
begin_operator
move c1 b1 en1
0
1
0 0 6 3
1
end_operator
begin_operator
move c1 b1 en2
0
1
0 2 6 3
1
end_operator
begin_operator
move c1 c2 en1
1
1 1
1
0 0 6 7
1
end_operator
begin_operator
move c1 c2 en2
1
1 1
1
0 2 6 7
1
end_operator
begin_operator
move c1 d1 en1
0
1
0 0 6 10
1
end_operator
begin_operator
move c1 d1 en2
0
1
0 2 6 10
1
end_operator
begin_operator
move c2 c1 en1
1
1 1
1
0 0 7 6
1
end_operator
begin_operator
move c2 c1 en2
1
1 1
1
0 2 7 6
1
end_operator
begin_operator
move c2 c3 en1
0
1
0 0 7 8
1
end_operator
begin_operator
move c2 c3 en2
0
1
0 2 7 8
1
end_operator
begin_operator
move c3 b3 en1
0
1
0 0 8 4
1
end_operator
begin_operator
move c3 b3 en2
0
1
0 2 8 4
1
end_operator
begin_operator
move c3 c2 en1
0
1
0 0 8 7
1
end_operator
begin_operator
move c3 c2 en2
0
1
0 2 8 7
1
end_operator
begin_operator
move c3 c4 en1
0
1
0 0 8 9
1
end_operator
begin_operator
move c3 c4 en2
0
1
0 2 8 9
1
end_operator
begin_operator
move c3 d3 en1
0
1
0 0 8 11
1
end_operator
begin_operator
move c3 d3 en2
0
1
0 2 8 11
1
end_operator
begin_operator
move c4 b4 en1
0
1
0 0 9 5
1
end_operator
begin_operator
move c4 b4 en2
0
1
0 2 9 5
1
end_operator
begin_operator
move c4 c3 en1
0
1
0 0 9 8
1
end_operator
begin_operator
move c4 c3 en2
0
1
0 2 9 8
1
end_operator
begin_operator
move c4 d4 en1
0
1
0 0 9 12
1
end_operator
begin_operator
move c4 d4 en2
0
1
0 2 9 12
1
end_operator
begin_operator
move d1 c1 en1
0
1
0 0 10 6
1
end_operator
begin_operator
move d1 c1 en2
0
1
0 2 10 6
1
end_operator
begin_operator
move d3 c3 en1
0
1
0 0 11 8
1
end_operator
begin_operator
move d3 c3 en2
0
1
0 2 11 8
1
end_operator
begin_operator
move d3 d4 en1
0
1
0 0 11 12
1
end_operator
begin_operator
move d3 d4 en2
0
1
0 2 11 12
1
end_operator
begin_operator
move d4 c4 en1
0
1
0 0 12 9
1
end_operator
begin_operator
move d4 c4 en2
0
1
0 2 12 9
1
end_operator
begin_operator
move d4 d3 en1
0
1
0 0 12 11
1
end_operator
begin_operator
move d4 d3 en2
0
1
0 2 12 11
1
end_operator
8
begin_rule
1
0 2
3 1 0
end_rule
begin_rule
1
0 5
3 1 0
end_rule
begin_rule
1
0 9
3 1 0
end_rule
begin_rule
1
0 12
3 1 0
end_rule
begin_rule
1
2 2
3 1 0
end_rule
begin_rule
1
2 5
3 1 0
end_rule
begin_rule
1
2 9
3 1 0
end_rule
begin_rule
1
2 12
3 1 0
end_rule
