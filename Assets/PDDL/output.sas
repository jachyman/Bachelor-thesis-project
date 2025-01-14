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
2
Atom wall(c2, c3)
NegatedAtom wall(c2, c3)
end_variable
begin_variable
var1
-1
13
Atom at(a1)
Atom at(a2)
Atom at(a3)
Atom at(b1)
Atom at(b3)
Atom at(b4)
Atom at(c1)
Atom at(c2)
Atom at(c3)
Atom at(c4)
Atom at(d1)
Atom at(d2)
Atom at(d4)
end_variable
begin_variable
var2
-1
2
Atom wall(c3, c2)
NegatedAtom wall(c3, c2)
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
1
6
1
1
end_state
begin_goal
1
3 0
end_goal
28
begin_operator
move a1 a2
0
1
0 1 0 1
1
end_operator
begin_operator
move a1 b1
0
1
0 1 0 3
1
end_operator
begin_operator
move a2 a1
0
1
0 1 1 0
1
end_operator
begin_operator
move a2 a3
0
1
0 1 1 2
1
end_operator
begin_operator
move a3 a2
0
1
0 1 2 1
1
end_operator
begin_operator
move a3 b3
0
1
0 1 2 4
1
end_operator
begin_operator
move b1 a1
0
1
0 1 3 0
1
end_operator
begin_operator
move b1 c1
0
1
0 1 3 6
1
end_operator
begin_operator
move b3 a3
0
1
0 1 4 2
1
end_operator
begin_operator
move b3 b4
0
1
0 1 4 5
1
end_operator
begin_operator
move b3 c3
0
1
0 1 4 8
1
end_operator
begin_operator
move b4 b3
0
1
0 1 5 4
1
end_operator
begin_operator
move b4 c4
0
1
0 1 5 9
1
end_operator
begin_operator
move c1 b1
0
1
0 1 6 3
1
end_operator
begin_operator
move c1 d1
0
1
0 1 6 10
1
end_operator
begin_operator
move c2 c3
2
0 1
2 1
1
0 1 7 8
1
end_operator
begin_operator
move c2 d2
0
1
0 1 7 11
1
end_operator
begin_operator
move c3 b3
0
1
0 1 8 4
1
end_operator
begin_operator
move c3 c2
0
3
0 1 8 7
0 0 1 0
0 2 1 0
1
end_operator
begin_operator
move c3 c4
0
1
0 1 8 9
1
end_operator
begin_operator
move c4 b4
0
1
0 1 9 5
1
end_operator
begin_operator
move c4 c3
0
1
0 1 9 8
1
end_operator
begin_operator
move c4 d4
0
1
0 1 9 12
1
end_operator
begin_operator
move d1 c1
0
1
0 1 10 6
1
end_operator
begin_operator
move d1 d2
0
1
0 1 10 11
1
end_operator
begin_operator
move d2 c2
0
3
0 1 11 7
0 0 -1 0
0 2 -1 0
1
end_operator
begin_operator
move d2 d1
0
1
0 1 11 10
1
end_operator
begin_operator
move d4 c4
0
1
0 1 12 9
1
end_operator
3
begin_rule
1
1 5
3 1 0
end_rule
begin_rule
1
1 9
3 1 0
end_rule
begin_rule
1
1 12
3 1 0
end_rule
