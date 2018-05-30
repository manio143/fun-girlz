(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Jak ty wyglądasz? - Wzorce
======================

Dotychczas używaliśmy wyrażeń warunkowych `if-then-else` żeby rozgałęziać ścieżki w naszym programi w zależności od napotkanych danych. Jednak nie jest to szczególnie wygodne kiedy mamy doczynienia z bardziej złożonymi przypadkami.

Po pierwsze możemy jednym wyrażeniem testować kilka wartości:

*)

let charOfInt i =
    match i with
    | 0 -> 'a'
    | 1 -> 'b'
    | 2 -> 'c'
    | 3 -> 'd'
    | 4 -> 'e'
    | 5 -> 'f'
    //...
    | _ -> '\000'
    
(**

Po drugie możemy we wzorcach umieszczać również zmienne:

*)

let firstTwo l =
    match l with
    | a :: b :: _ -> (a,b)
    | _ -> failwith "Not enough elements"

(**

Ostatecznie pozwala to na robienie różnych skomplikowanych wzorców. Jeśli pierwszy element to 1 to zwróc drugi, a jak nie to pierwszy.

*)

let firstOrSecondIfFirstIsOne k =
    match k with
    | (1, a) -> a
    | (a, _) -> a

(**

Kolejność wzorców ma znaczenie, bo są sprawdzane po kolei. Czyli w poniższej funkcji drugi wzorzec nigdy nie zostanie wykonany.

*)

let always l =
    match l with
    | _ -> 0
    | [x] -> x

(**

O dalszych wzorcach będziemy jeszcze mówić po wprowadzeniu kolejnych typów danych.

*)

(**

Zadania
========

1. Znajdź i-ty element w liście
2. Znajdź rozmiar listy
3. Odwróć listę (względem kolejności)
4. Napisz funkcję `sum l`, która zsumuje elementy listy
4. Napisz funkcję `merge l1 l2` złączającą dwie listy oraz `flatten list`, która bierze listę list i ją "spłaszcza" zwracając elementy w jednej liście
5. Napisz funkcję `maxl (x:int list)`, która zwróci element maksymalny z podanej listy
6. Napisz funkcję `distinctAside list`, która usunie powtarzające się elementy
7. Napisz funkcję `runLength list`, która dla danej listy zwróci listę tupli `int * 'a`, określających częstość występowania w ciągu.
    Np. `runLength [a;a;a;b;b;c;c;c;c;d;a] = [(3, a);(2, b);(4, c);(1, d);(1, a)]` 
8. Napisz funkcję `duplicate list`, która zduplikuje każdy element (np `duplicate [1;2;3] = [1;1;2;2;3;3]`)
9. Napisz funkcję `split list i`, która zwróci parę list, z których pierwsza jest rozmiaru conajwyżej i
10. Napisz funkcję `count predicate list`, która dla danej listy i predykatu `'a -> bool` zwróci liczbę elementów podanej listy, które ten predykat spełniają.

Pytania? Jeśli wszystko jasne, to przechodzimy do [następnego modułu](./tailr.html)

*)