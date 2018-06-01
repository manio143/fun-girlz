(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Pokaż mi swój ogon
======================

Rekurencja to wielokrotne wołanie funkcji. Za każdym razem kiedy wołamy jakąś funkcję to odkładamy na stos operacyjny informację, gdzie powinniśmy wrócić po wykonaniu funkcji. Stos ma pewną wielkość i jeśli odłożymy na niego za dużo danych to znajdziemy się w bardzo złej sytuacji. Na platformie .NET dostaniemy wyjątek `StackOverflowException`, którego nie da się obsłużyć i wykonywanie naszego programu zostanie przerwane. Przykładem może być taka oto nieskończona pętla:

*)

let rec infinite i = 1 + infinite (i + 1)

(**

Jednak nie musi to być coś nieskończonego, wystarczy po prostu że ilość danych będzie dostatecznie duża. Co można zrobić?

Otóż nasz kompilator potrafi optymalizować wywołania rekurencyjne i produkować pętlę bez użycia stosu w kodzie wynikowym, ale tylko jeśli funkcja jest rekurencyjna ogonowo.

Ten ogon bierze się stąd, że ostatnim wyrażeniem nierozgałęziającym jest wywołanie tej samej funkcji, ale z odpowiednio innymi parametrami.

Większość rekurencji ogonowej opiera się o wzorzec akumulatora. Powyższą nieskończoną pętlę zapiszemy w poniższy sposób i o ile będzie się ona pętlić w nieskończoność, to nie poskutkuje śmiercią procesu.

*)

let infinite' i =
    let rec inf_ i acc : int = inf_ (i+1) (acc+1)
    inf_ i 0

(**

Będziemy ten wzorzec wykorzystywać do optymalizacji naszego kodu. Zaczniemy od porównania liczenia n-tej liczby Fibonacciego.

*)

let rec fib n =
    if n <= 1 then 1
    else fib (n-1) + fib (n-2)

let fibt n =
    let rec fib_ n a b =
        if n = 0 then a
        else fib_ (n-1) (a + b) a
    fib_ n 1 0

(**

Żeby przetestować optymalizację włączymy licznik czasu w FSI

    #time "on";;

A następnie obliczymy wartość

    fib 40;;
    fibt 40;;

I porównamy czasy ich wykonania.

*)

(**

Funkcje wyższych rzędów
------------------------

Użyjemy zdobytej przez nas dotychczas wiedzy, aby napisać funkcje, które ułatwiają życie podczas pracy z listami.

#### `map`

Przypomnijmy sobie funkcje `plus1` i `succL`. Miały one wspólną funkcjonalność. Brały listę, robiły coś dla każdego jej elementu, niezależnie od pozostałych elementów, i zwracały zmodyfikowaną listę. Jest to funkcjonalność bibliotecznej funkcji `map`, którą właśnie zaimplementujemy.

`map` ma sygnaturę `('a -> 'b) -> 'a list -> 'b list`

Powiemy, że `map` jest funkcją "wyższego rzędu" ponieważ jednym z jej parametrów jest funkcja.

*)

let rec map f l =
    match l with
    | h :: t -> f h :: map f t
    | []     -> []

(**

Teraz waszym zadaniem będzie ogonowa implementacja naszej funkcji map. Wzorcowe rozwiązanie poniżej.

<div>
    <center><button id="map-sp-btn" onclick="document.getElementById('map-sp').style.display = ''; document.getElementById('map-sp-btn').style.display = 'none'">Spoiler (map)</button><center>
    <div id="map-sp" style="display: none;">
*)

let mapt f l =
    let rec reverse l r =
        match l with
        | h :: t -> reverse t (h :: r)
        | [] -> r
    let rec map_ l acc =
        match l with
        | h :: t -> map_ t (f h :: acc)
        | []     -> reverse acc []
    map_ l []

(**
</div>
</div>

Tak jak wcześniej, możemy przetestować działanie naszej implementacji `map` i `mapt` na funkcji `fib` i `fibt` oraz liście `[1..40]`.

Często będziemy stosować `map` z funkcjami anonimowymi:

*)

let plus1 = List.map (fun x -> 1 + x)

(**

Powyżej wykorzystujemy częściową aplikację, żeby nie musieć podawać ostatniego parametru do funkcji `map` (tym razem bibliotecznej) oraz anonimową funkcję dodającą jeden.

#### `fold`
Kolejną przydatną funkcją jest `fold` który służy do zwijania listy (robienie z niej kuli do bałwanka) łącząc kolejne elementy pewną funkcją.

`fold` ma sygnaturę `('a -> 'b -> 'a) -> 'a -> 'b list -> 'a`

Przy jej pomocy zaimplementujemy w prosty sposób funkcję sumującą elementy listy.

*)

let rec fold f v l =
    match l with
    | [] -> v
    | h :: t -> fold f (f v h) t

let sum = fold (+) 0

(**

Jest to tzw. lewostronny `fold`, czyli zaczynając od lewej strony bierzemy wartość początkową `v` i rolujemy ją po liście aplikując funkcję `f`. Jak widać `fold` jest domyślnie ogonowy.

Możemy przy jego pomocy zaimplementować także nasz `map`.

*)

let mapf f l = List.rev (fold (fun acc e -> f e :: acc) [] l)

(**

Zadania
--------

1. Zaimplementuj funkcję `filter` o sygnaturze `('a -> bool) -> 'a list -> 'a list`
2. Używając powyższej oraz ogonowej implementacji funkcji `len` napisz prostą funkcję `count` (można zaimplementować `len` przez `fold`)
3. Napisz prawostronny `foldr`, który pozwoli napisać `mapfr` bez potrzeby `List.rev` (`foldr` nie będzie ogonowy)
4. Napisz ogoną implementację silni (z akumulatorem) oraz wersję korzystającą tylko z funkcji wyższego rzędu

Pytania? Jeśli wszystko jasne, to przechodzimy do [następnego modułu](./funops.html)

*)