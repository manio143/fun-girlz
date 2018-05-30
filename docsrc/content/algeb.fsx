(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Dyskryminowane unie
======================

Unie, czy też typy algebraiczne to takie byty których wartości są definiowane przy użyciu pewnego kontruktora. Mogą służyć jako alternatywy bezargumentowe lub alternatywy argumentowane.

Chyba najprościej pokazać na przykładzie.

*)

type Boolean = True | False

type Option<'a> = None | Some of 'a

type Either<'a, 'e> = Left of 'e | Right of 'a

(**

Unie są świetnym sposobem do wyrażania swoich zamiarów w konkretny sposób. Zamiast używać generycznych krotek, możemy nadać więcej kontekstu naszemu typowi danych.

*)

type Pair<'a,'b> = Pair of fst : 'a * snd : 'b

let pair x = Pair x
let rev p = match p with Pair(a, b) -> Pair (fst = b, snd = a)
let fst p = match p with Pair (a,_) -> a

(**

W programowaniu funkcyjnym nie ma czegoś takiego jak `null`. Jeśli chcemy zaznaczyć że funkcja może nie zwrócić wartości to opakujemy tą wartość w typ `option`. W ten sposób rozdzielamy jasno dwa przypadki `None` oraz `Some x`. Dużo funkcji w bibliotece F# będzie na tym typie się opierać. Ma on nawet swoją własną funkcję `map`:

*)

let optionMap f o =
    match o with
    | None -> None
    | Some x -> Some <| f x

(**

Podobnie typ `Either` (tudzież `Choice`) może być używany jako `Right wynik` lub `Left błąd`. Są to pewne wzorce jakich używa się przy programowaniu prawdziwych aplikacji w języku funkcyjnym.

My jednak skupimy się bardziej na standardowym drzewie BST i na nim poćwiczymy pracę z uniami.

*)

type Tree<'a when 'a : comparison> = 
    | Leaf 
    | Node of Left: Tree<'a> * Value: 'a * Right: Tree<'a>

(**

Drzewo BST (Binary Search Tree) to takie drzewo binarne, gdzie wszystkie wierzchołki w lewym poddrzewie mają wartość mniejszą lub równą wartości korzenia, a te w prawym poddrzewie mają wartości większe.

Drzewa BST służą do wyszukiwania elementów w zbiorze w czasie O(log n), gdzie n jest liczbą wierzchołków w drzewie. Przy czym, żeby to faktycznie zachodziło to drzewo BST musi być zbalansowane.

Ale zaczniemy od skupienia się na prostych operacjach.

Zadania
============

1. Napisz funkcję `insertBST x t`, która bierze element, drzewo i zwraca nowe drzewo zawierające ten element zachowując zasady BST
2. Napisz funkcję `fromListBST l`, która bierze listę i zwraca drzewo zawierające wszystkie elementy listy (polecam użyć funkcji wyższego rzędu)
3. Napisz funkcję `infixBST t`, która bierze drzewo i zwraca listę jego elementów w porządku infiksowym
4. Napisz funkcję `sortBST l`, która sortuje listę wykorzystując własności drzewa BST
5. Napisz funkcję `removeBST x t`, która usuwa wszystkie wystąpienia wartości `x` z drzewa `t`
6. Napisz funkcję `mapBST f t`, która aplikuje funkcję `f` do każdego wierzchołka drzewa `t`
7. Napisz funkcję `foldBST f v t`, która przechodzi po drzewie w porządku infixowym i robi to co fold (bez użycia `infixBST`)

Pytania? Jeśli wszystko jasne, to przechodzimy do [następnego modułu](./rec.html) 

*)