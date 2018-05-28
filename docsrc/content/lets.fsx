(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Niech będzie stała
======================

Pierwszą rzeczą jaką musimy zrozumieć w programowaniu funkcyjnym jest to, że nie przypisujemy wartości do zmiennej, ale raczej nadajemy alias pewnej wartości. Można to też rozumieć jako deklarowanie stałych.

Będziemy się posługiwać słowem kluczowym `let` w celu zadeklarowania stałej.

*)

let a = 1
let b = 2.0
let c = 'c'
let d = "Ala ma kota"

(**

Stałe mogą określać dowolną wartość, więc również **funkcje**.

*)

let id x = x

(**

Zadeklarowaliśmy właśnie stałą `id`, której wartość to funkcja typu `a -> a` (funkcja identyczności). W zasadzie jest to uproszczenie poniższczego zapisu:

*)

let id' = fun x -> x

(**

Ta postać to tzw. wyrażenie lambda. W matematyce używa się symbolu λ. `niech id = λx. x`. Nie będziemy zagłębiali się tutaj zbytnio w rachunek lambda, ale warto zwrócić uwagę na kilka rzeczy:

Każda lambda ma jeden argument. Funkcje wieloargumentowe to w zasadzie funkcje jednoargumentowe, które zwracają kolejną jednoargumentową funkcję.
*)

let multiArg a b c d = ()
let multiArg' = fun a -> fun b -> fun c -> fun d -> ()

(**
Aby obliczyć wyrażenie wewnątrz lambdy musimy zaaplikować do niej wartość, którą zwiążemy z nazwą argumentu.

*)

let a' = id a

(**

Aplikacja to inaczej wywołanie funkcji. Jednak aplikacja może być również częściowa. W takim przypadku funkcja zostanie wywołana dopiero w momencie aplikacji ostatniego argumentu.

*)

let dodaj x y = x + y
let dodaj5 = dodaj 5

(**
F# Interactive
---------

Żeby pobawić się F# będziemy korzystać z narzędzia FSI, które dynamicznie kompiluje kod, który mu podajemy i dokonuje ewaluacji wyrażeń.

Poniżej będę pisał `fsi` odwołując się do tego środowiska, natomiast pod Linuxem binarka nazywa się `fsharpi`.

Możemy uruchomić FSI w pustym środowisku

    fsi

Możemy załadować nasz skrypt

    fsi /path/to/script.fsx

Ale możemy załadować skrypt również w trakcie pracy przez dyrektywę `#l`

    > #load "/path/to/script.fsx"


*)

(**

*exercises*

*)