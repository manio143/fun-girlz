(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Zbiory danych
======================

W programowaniu funkcyjnym będziemy stykali się często z pojęciem krotki oraz listy. Krotka to zbiór N elementów dowolnego (określonego) typu, gdzie N jest stałe. Np:

*)

let k = (1,2,'a') // int * int * char

(**

Krotek używamy jeśli potrzebujemy posłużyć się anonimową strukturą danych o stałej wielkości, gdzie kolejność elementów nadaje im znaczenie. Więc jeśli np. mamy funkcję, która ma nam zwrócić dwa pierwsze elementy listy, to zwróci ona parę. Mamy wtedy pewność, że dostaniemy dwa elementy, ni mniej, ni więcej.

*)

(**

Listy natomiast to łańcuchowe struktury do przechowywania dowolnej ilości danych jednego typu. 

*)

let l = [1;2;3;4] // int list
let l2 = 0 :: l   // [0;1;2;3;4]
let l3 = [0..5]   // [0;1;2;3;4;5]

(**

Lista ma głowę oraz ogon. Ogon jest listą pozostałych elementów (oprócz głowy).

Czyli tworząc listę nakładamy kolejne głowy zaczynając od pustej listy (stałej `[]`).


Zapoznamy się teraz z pojęciem rekurencji.
*)

let rec makeList n value =
    if n <= 0 then []
    else value :: makeList (n-1) value

(**

Schemat działania jest następujący: jeśli `n <= 0` to zwróć listę pustą, w.p.p. oblicz rekurencyjnie wartość funkcji dla `(n-1)` i nałóż na nią nową głowę o wartości `value`.


Poniżej zobaczymy jak można użyć dekonstrukcji wartości list i krotek w wyrażeniu `let`
*)

let fst x = let (a,b) = x in a
let snd x = let (a,b) = x in b

let rec plus1 l =
    if l = [] then []
    else let (h::t) = l in
         (h + 1) :: plus1 t

(**

Zadania
--------

1. Napisz funkcję `len` obliczającą rekurencyjnie długość listy.
2. Napisz funkcję `succL` która przyjmuje listę znaków i zwraca listę gdzie do każdego elementu zaaplikowano funkcję `succ` z poprzedniego modułu.
3. Napisz funkcję obliczającą rekurencyjnie wartość silnii dla podanego `n`.
4. Napisz funkcję generującą rekurencyjnie listę elementów od 0 do `n`.

Pytania? Jeśli wszystko jasne, to przechodzimy do [następnego modułu](./patterns.html)

*)