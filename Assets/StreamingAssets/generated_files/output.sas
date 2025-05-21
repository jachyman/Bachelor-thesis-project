begin_version
3
end_version
begin_metric
1
end_metric
25
begin_variable
var0
-1
2
Atom blocked(a1)
NegatedAtom blocked(a1)
end_variable
begin_variable
var1
-1
2
Atom blocked(e1)
NegatedAtom blocked(e1)
end_variable
begin_variable
var2
-1
2
Atom blocked(b1)
NegatedAtom blocked(b1)
end_variable
begin_variable
var3
-1
2
Atom blocked(b2)
NegatedAtom blocked(b2)
end_variable
begin_variable
var4
-1
2
Atom blocked(a3)
NegatedAtom blocked(a3)
end_variable
begin_variable
var5
-1
2
Atom blocked(a5)
NegatedAtom blocked(a5)
end_variable
begin_variable
var6
-1
2
Atom blocked(e5)
NegatedAtom blocked(e5)
end_variable
begin_variable
var7
-1
2
Atom blocked(c1)
NegatedAtom blocked(c1)
end_variable
begin_variable
var8
-1
2
Atom blocked(a4)
NegatedAtom blocked(a4)
end_variable
begin_variable
var9
-1
2
Atom blocked(e2)
NegatedAtom blocked(e2)
end_variable
begin_variable
var10
-1
2
Atom blocked(b5)
NegatedAtom blocked(b5)
end_variable
begin_variable
var11
-1
2
Atom blocked(d5)
NegatedAtom blocked(d5)
end_variable
begin_variable
var12
-1
2
Atom blocked(c5)
NegatedAtom blocked(c5)
end_variable
begin_variable
var13
-1
2
Atom blocked(e4)
NegatedAtom blocked(e4)
end_variable
begin_variable
var14
-1
2
Atom blocked(e3)
NegatedAtom blocked(e3)
end_variable
begin_variable
var15
-1
2
Atom blocked(d2)
NegatedAtom blocked(d2)
end_variable
begin_variable
var16
-1
2
Atom blocked(c2)
NegatedAtom blocked(c2)
end_variable
begin_variable
var17
-1
2
Atom blocked(b3)
NegatedAtom blocked(b3)
end_variable
begin_variable
var18
-1
2
Atom blocked(b4)
NegatedAtom blocked(b4)
end_variable
begin_variable
var19
-1
2
Atom blocked(d4)
NegatedAtom blocked(d4)
end_variable
begin_variable
var20
-1
2
Atom blocked(c4)
NegatedAtom blocked(c4)
end_variable
begin_variable
var21
-1
2
Atom blocked(d3)
NegatedAtom blocked(d3)
end_variable
begin_variable
var22
-1
2
Atom blocked(c3)
NegatedAtom blocked(c3)
end_variable
begin_variable
var23
-1
25
Atom enemy_loc(en1, a1)
Atom enemy_loc(en1, a2)
Atom enemy_loc(en1, a3)
Atom enemy_loc(en1, a4)
Atom enemy_loc(en1, a5)
Atom enemy_loc(en1, b1)
Atom enemy_loc(en1, b2)
Atom enemy_loc(en1, b3)
Atom enemy_loc(en1, b4)
Atom enemy_loc(en1, b5)
Atom enemy_loc(en1, c1)
Atom enemy_loc(en1, c2)
Atom enemy_loc(en1, c3)
Atom enemy_loc(en1, c4)
Atom enemy_loc(en1, c5)
Atom enemy_loc(en1, d1)
Atom enemy_loc(en1, d2)
Atom enemy_loc(en1, d3)
Atom enemy_loc(en1, d4)
Atom enemy_loc(en1, d5)
Atom enemy_loc(en1, e1)
Atom enemy_loc(en1, e2)
Atom enemy_loc(en1, e3)
Atom enemy_loc(en1, e4)
Atom enemy_loc(en1, e5)
end_variable
begin_variable
var24
-1
2
Atom goal_reached(en1)
NegatedAtom goal_reached(en1)
end_variable
0
begin_state
1
1
1
0
1
1
1
1
1
1
1
1
1
1
1
1
1
1
1
1
1
1
0
6
1
end_state
begin_goal
1
24 0
end_goal
72
begin_operator
move a1 a2 en1 en1
0
3
0 0 -1 1
0 23 0 1
0 24 1 0
1
end_operator
begin_operator
move a1 b1 en1 en1
1
24 1
3
0 0 -1 1
0 2 1 0
0 23 0 5
1
end_operator
begin_operator
move a2 a1 en1 en1
1
24 1
2
0 0 1 0
0 23 1 0
1
end_operator
begin_operator
move a3 a4 en1 en1
1
24 1
3
0 4 -1 1
0 8 1 0
0 23 2 3
1
end_operator
begin_operator
move a3 b3 en1 en1
1
24 1
3
0 4 -1 1
0 17 1 0
0 23 2 7
1
end_operator
begin_operator
move a4 a3 en1 en1
1
24 1
3
0 4 1 0
0 8 -1 1
0 23 3 2
1
end_operator
begin_operator
move a4 a5 en1 en1
1
24 1
3
0 8 -1 1
0 5 1 0
0 23 3 4
1
end_operator
begin_operator
move a4 b4 en1 en1
1
24 1
3
0 8 -1 1
0 18 1 0
0 23 3 8
1
end_operator
begin_operator
move a5 a4 en1 en1
1
24 1
3
0 8 1 0
0 5 -1 1
0 23 4 3
1
end_operator
begin_operator
move a5 b5 en1 en1
1
24 1
3
0 5 -1 1
0 10 1 0
0 23 4 9
1
end_operator
begin_operator
move b1 a1 en1 en1
1
24 1
3
0 0 1 0
0 2 -1 1
0 23 5 0
1
end_operator
begin_operator
move b1 c1 en1 en1
1
24 1
3
0 2 -1 1
0 7 1 0
0 23 5 10
1
end_operator
begin_operator
move b2 b3 en1 en1
1
24 1
3
0 3 -1 1
0 17 1 0
0 23 6 7
1
end_operator
begin_operator
move b2 c2 en1 en1
1
24 1
3
0 3 -1 1
0 16 1 0
0 23 6 11
1
end_operator
begin_operator
move b3 a3 en1 en1
1
24 1
3
0 4 1 0
0 17 -1 1
0 23 7 2
1
end_operator
begin_operator
move b3 b2 en1 en1
1
24 1
3
0 3 1 0
0 17 -1 1
0 23 7 6
1
end_operator
begin_operator
move b3 b4 en1 en1
1
24 1
3
0 17 -1 1
0 18 1 0
0 23 7 8
1
end_operator
begin_operator
move b3 c3 en1 en1
1
24 1
3
0 17 -1 1
0 22 1 0
0 23 7 12
1
end_operator
begin_operator
move b4 a4 en1 en1
1
24 1
3
0 8 1 0
0 18 -1 1
0 23 8 3
1
end_operator
begin_operator
move b4 b3 en1 en1
1
24 1
3
0 17 1 0
0 18 -1 1
0 23 8 7
1
end_operator
begin_operator
move b4 b5 en1 en1
1
24 1
3
0 18 -1 1
0 10 1 0
0 23 8 9
1
end_operator
begin_operator
move b4 c4 en1 en1
1
24 1
3
0 18 -1 1
0 20 1 0
0 23 8 13
1
end_operator
begin_operator
move b5 a5 en1 en1
1
24 1
3
0 5 1 0
0 10 -1 1
0 23 9 4
1
end_operator
begin_operator
move b5 b4 en1 en1
1
24 1
3
0 18 1 0
0 10 -1 1
0 23 9 8
1
end_operator
begin_operator
move b5 c5 en1 en1
1
24 1
3
0 10 -1 1
0 12 1 0
0 23 9 14
1
end_operator
begin_operator
move c1 b1 en1 en1
1
24 1
3
0 2 1 0
0 7 -1 1
0 23 10 5
1
end_operator
begin_operator
move c1 c2 en1 en1
1
24 1
3
0 7 -1 1
0 16 1 0
0 23 10 11
1
end_operator
begin_operator
move c1 d1 en1 en1
0
3
0 7 -1 1
0 23 10 15
0 24 1 0
1
end_operator
begin_operator
move c2 b2 en1 en1
1
24 1
3
0 3 1 0
0 16 -1 1
0 23 11 6
1
end_operator
begin_operator
move c2 c1 en1 en1
1
24 1
3
0 7 1 0
0 16 -1 1
0 23 11 10
1
end_operator
begin_operator
move c2 c3 en1 en1
1
24 1
3
0 16 -1 1
0 22 1 0
0 23 11 12
1
end_operator
begin_operator
move c2 d2 en1 en1
1
24 1
3
0 16 -1 1
0 15 1 0
0 23 11 16
1
end_operator
begin_operator
move c3 b3 en1 en1
1
24 1
3
0 17 1 0
0 22 -1 1
0 23 12 7
1
end_operator
begin_operator
move c3 c2 en1 en1
1
24 1
3
0 16 1 0
0 22 -1 1
0 23 12 11
1
end_operator
begin_operator
move c3 c4 en1 en1
1
24 1
3
0 22 -1 1
0 20 1 0
0 23 12 13
1
end_operator
begin_operator
move c3 d3 en1 en1
1
24 1
3
0 22 -1 1
0 21 1 0
0 23 12 17
1
end_operator
begin_operator
move c4 b4 en1 en1
1
24 1
3
0 18 1 0
0 20 -1 1
0 23 13 8
1
end_operator
begin_operator
move c4 c3 en1 en1
1
24 1
3
0 22 1 0
0 20 -1 1
0 23 13 12
1
end_operator
begin_operator
move c4 c5 en1 en1
1
24 1
3
0 20 -1 1
0 12 1 0
0 23 13 14
1
end_operator
begin_operator
move c4 d4 en1 en1
1
24 1
3
0 20 -1 1
0 19 1 0
0 23 13 18
1
end_operator
begin_operator
move c5 b5 en1 en1
1
24 1
3
0 10 1 0
0 12 -1 1
0 23 14 9
1
end_operator
begin_operator
move c5 c4 en1 en1
1
24 1
3
0 20 1 0
0 12 -1 1
0 23 14 13
1
end_operator
begin_operator
move c5 d5 en1 en1
1
24 1
3
0 12 -1 1
0 11 1 0
0 23 14 19
1
end_operator
begin_operator
move d1 c1 en1 en1
1
24 1
2
0 7 1 0
0 23 15 10
1
end_operator
begin_operator
move d1 e1 en1 en1
1
24 1
2
0 1 1 0
0 23 15 20
1
end_operator
begin_operator
move d2 c2 en1 en1
1
24 1
3
0 16 1 0
0 15 -1 1
0 23 16 11
1
end_operator
begin_operator
move d2 d3 en1 en1
1
24 1
3
0 15 -1 1
0 21 1 0
0 23 16 17
1
end_operator
begin_operator
move d2 e2 en1 en1
1
24 1
3
0 15 -1 1
0 9 1 0
0 23 16 21
1
end_operator
begin_operator
move d3 c3 en1 en1
1
24 1
3
0 22 1 0
0 21 -1 1
0 23 17 12
1
end_operator
begin_operator
move d3 d2 en1 en1
1
24 1
3
0 15 1 0
0 21 -1 1
0 23 17 16
1
end_operator
begin_operator
move d3 d4 en1 en1
1
24 1
3
0 21 -1 1
0 19 1 0
0 23 17 18
1
end_operator
begin_operator
move d3 e3 en1 en1
1
24 1
3
0 21 -1 1
0 14 1 0
0 23 17 22
1
end_operator
begin_operator
move d4 c4 en1 en1
1
24 1
3
0 20 1 0
0 19 -1 1
0 23 18 13
1
end_operator
begin_operator
move d4 d3 en1 en1
1
24 1
3
0 21 1 0
0 19 -1 1
0 23 18 17
1
end_operator
begin_operator
move d4 d5 en1 en1
1
24 1
3
0 19 -1 1
0 11 1 0
0 23 18 19
1
end_operator
begin_operator
move d4 e4 en1 en1
1
24 1
3
0 19 -1 1
0 13 1 0
0 23 18 23
1
end_operator
begin_operator
move d5 c5 en1 en1
1
24 1
3
0 12 1 0
0 11 -1 1
0 23 19 14
1
end_operator
begin_operator
move d5 d4 en1 en1
1
24 1
3
0 19 1 0
0 11 -1 1
0 23 19 18
1
end_operator
begin_operator
move d5 e5 en1 en1
1
24 1
3
0 11 -1 1
0 6 1 0
0 23 19 24
1
end_operator
begin_operator
move e1 d1 en1 en1
0
3
0 1 -1 1
0 23 20 15
0 24 1 0
1
end_operator
begin_operator
move e1 e2 en1 en1
1
24 1
3
0 1 -1 1
0 9 1 0
0 23 20 21
1
end_operator
begin_operator
move e2 d2 en1 en1
1
24 1
3
0 15 1 0
0 9 -1 1
0 23 21 16
1
end_operator
begin_operator
move e2 e1 en1 en1
1
24 1
3
0 1 1 0
0 9 -1 1
0 23 21 20
1
end_operator
begin_operator
move e2 e3 en1 en1
1
24 1
3
0 9 -1 1
0 14 1 0
0 23 21 22
1
end_operator
begin_operator
move e3 d3 en1 en1
1
24 1
3
0 21 1 0
0 14 -1 1
0 23 22 17
1
end_operator
begin_operator
move e3 e2 en1 en1
1
24 1
3
0 9 1 0
0 14 -1 1
0 23 22 21
1
end_operator
begin_operator
move e3 e4 en1 en1
1
24 1
3
0 14 -1 1
0 13 1 0
0 23 22 23
1
end_operator
begin_operator
move e4 d4 en1 en1
1
24 1
3
0 19 1 0
0 13 -1 1
0 23 23 18
1
end_operator
begin_operator
move e4 e3 en1 en1
1
24 1
3
0 14 1 0
0 13 -1 1
0 23 23 22
1
end_operator
begin_operator
move e4 e5 en1 en1
1
24 1
3
0 13 -1 1
0 6 1 0
0 23 23 24
1
end_operator
begin_operator
move e5 d5 en1 en1
1
24 1
3
0 11 1 0
0 6 -1 1
0 23 24 19
1
end_operator
begin_operator
move e5 e4 en1 en1
1
24 1
3
0 13 1 0
0 6 -1 1
0 23 24 23
1
end_operator
0
