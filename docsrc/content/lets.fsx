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
let e = ()

(**

Powyższe stałe mają następujące typy

    val a : int = 1
    val b : float = 2.0
    val c : char = 'c'
    val d : string = "Ala ma kota"
    val e : unit = ()

W F# `int` to `System.Int32`, `float` to `System.Double`, `char` to `System.Char`, `string` to `System.String`, a `unit` to specjalny typ, który ma jedną wartość `()` i używany jest jako odpowiednik `void` z programów imperatywnych. W programowaniu funkcyjnym, każda funkcja musi zwracać jakąś wartość. Więc kiedy nie ma zbytnio co zwrócić to zwracamy właśnie `unit`.

*)

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

Funkcje
--------------

Stałe mogą określać dowolną wartość, więc również **funkcje**.

*)

let id x = x

(**

Zadeklarowaliśmy właśnie stałą `id`, której wartość to funkcja typu `a -> a` (funkcja identyczności). W zasadzie jest to uproszczenie poniższczego zapisu:

*)

let id' = fun x -> x

(**

Ta postać to tzw. wyrażenie lambda lub funkcja anonimowa. W matematyce używa się symbolu λ. `niech id = λx. x`. Nie będziemy zagłębiali się tutaj zbytnio w rachunek lambda, ale warto zwrócić uwagę na kilka rzeczy:

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

Lepiej zobrazujemy to przy zapisie lambda.

*)

let dodaj' = fun x -> fun y -> x + y
let dodaj5' = dodaj' 5
let dodaj5'' = fun y -> 5 + y

(**

Wyrażenia warunkowe
-------------------

W programowaniu funkcyjnym można bardzo łatwo określić gdzie wyrażenie się zaczyna, a gdzie kończy. W przypadku wyrażenia `if` jego struktura to `if bool-exp then 'a-exp else 'a-exp`, gdzie `'a` jest pewnym typem. To oznacza, że każdy If ma Else oraz, że obie gałęzie muszą zwracać wartość tego samego typu.

Kolejna rzecz, przez którą trzeba się przebić to to, że ciało funkcji to jedno wyrażenie. W przypadku imperatywnego F# nie będzie to prawdą, ale pisząc czysto funkcyjny kod jak najbardziej.

*)

let even x = if x % 2 = 0 then true else false

(**

W F# do porównania używa się znaku `=`, ale jeśli jesteś bardzo przyzwyczajona do `==` to można sobie taki operator samemu zdefiniować.

*)

let (==) = (=)

let discrete x =
    if x == 0 then 0 
    else if x > 0 then 1
         else -1

(**

Powyżej można wstawić nawiasy wokół drugiego warunku, żeby precyzyjniej pokazać strukturę wyrażeń.

*)

let discrete' x =
    if x == 0 then 0 
    else (if x > 0 then 1
          else -1)

(**

Przy okazji poruszymy kwestię białych znaków i wcięć. W F# jeśli przerzucamy część wyrażenia do nowej linii to musi ono być wcięte tak żeby zaczynać się nie wcześniej niż początek tego wyrażenia. Wcięcia robimy spacjami, nie znakiem `\t`.

*)

(**

Zadania
=============

1. Napisz funkcję `succ`, która dla znaku od 'a' do 'y' poda znak następny, a dla 'z' poda 'a'.
2. Użyj wyrażenia warunkowego do deklaracji stałej liczbowej.

Pytania? Jeśli wszystko jasne, to przechodzimy do [następnego modułu](./sets.html)

*)