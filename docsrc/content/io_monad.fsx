(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Wolna monada IO
======================

Co to znaczy, że monada jest wolna? Możemy podzielić monady na dwa typy: takie które wykonują się od razu (np. monada `Result`) oraz takie, które trzeba wykonać specjalną funkcją po zbudowaniu (np. monada `State`).

Wolne monady to drugie. Dokładniej rzecz biorąc jest to dużo bardziej skomplikowane, sam ledwo rozumiem zasady działania kategorii wolnych monad. Cel jest taki, że dopóki nie uruchomimy monady, to możemy ją bezkarnie budować i ani kawałek jej kodu nie zostanie wykonany.

Wolne monady mają więc zalety. Między innymi będzie to możliwość powtórzenia wykonania. Np. monadę stanu budujemy raz, a potem możemy wywoływać wiele razy z różnym stanem.

Teraz zbudujemy sobie wolną monadę `IO`, która w funkcyjny sposób pozwoli nam komunikowac się ze światem.

*)

type IO<'a> =
    | Pure of 'a
    | PutStrLn of string * (unit -> IO<'a>)
    | GetStrLn of (string -> IO<'a>)
    | ReadFile of name: string * (string -> IO<'a>)
    | WriteFile of name: string * contents: string  * (unit -> IO<'a>)

let rec ioBind (f : 'a -> IO<'b>) (m : IO<'a>) =
    match m with
    | Pure x -> f x
    | PutStrLn (str, next) -> PutStrLn (str, next >> ioBind f)
    | GetStrLn (next) -> GetStrLn (next >> ioBind f)
    | ReadFile (name, next) -> ReadFile (name, next >> ioBind f)
    | WriteFile (name, contents, next) -> WriteFile (name, contents, next >> ioBind f)

type IOBuilder() =
    member this.Return(x) = Pure x
    member this.Bind(m, f) = ioBind f m
    member this.Zero(x) = Pure ()

let io = IOBuilder()

(**

Przygotowaliśmy Monadę zawierającą kilka pożądanych przez nas operacji. W zasadzie dopóki nie napiszemy interpretera dla tej monady, to zbudowanie jej nie będzie miało żadnych efektów.

*)

let pureNext = fun x -> Pure x
let putStrLn s = PutStrLn (s, pureNext)
let getStrLn = GetStrLn pureNext
let readFile name = ReadFile (name, pureNext)
let writeFile name contents = WriteFile (name, contents, pureNext)

let program =
    io {
        do! putStrLn "Podaj nazwę pliku:"
        let! fname = getStrLn
        let! contents = readFile fname
        do! writeFile fname (contents + "appended line.\n")
    }

(**

Napisaliśmy program, który wczytuje zawartość pliku na podstawie jego nazwy od użytkownika, a następnie dopisuje linijkę. Teraz utworzymy interpreter naszej monady.

*)

let rec interpret program = 
    match program with
    | Pure x -> x
    | PutStrLn (str, next) ->
        do printfn "%s" str
        interpret (next ())
    | GetStrLn next ->
        let s = System.Console.ReadLine()
        interpret (next s)
    | ReadFile (name, next) ->
        let s = System.IO.File.ReadAllText(name)
        interpret (next s)
    | WriteFile (name, cnt, next) ->
        do System.IO.File.WriteAllText(name, cnt)
        interpret (next ())

(**

Kiedy uruchomimy ten interpreter na naszym programie to wtedy po kolei będziemy wykonywać określone czynności.

`Pure` oznacza po prostu jakąś wartość. `PutStrLn` niesie ze sobą string do wypisania na ekran oraz będzie zwracać `()` do funkcji którą przekazuje do `bind`, a na koniec dostajemy kolejne `IO`, czyli pewną kontynuację do wykonania. Tworzymy taki łańcuch `IO`, który krok po kroku wykonujemy. Pozostałem `GetStrLn`, `ReadFile` i `WriteFile` zachowują się bardzo podobnie.

Nie będziemy już mieli zadań, a zamiast tego porozmawiamy sobie o monadzie `Async`, którą można nieco potraktować jako wolną monadę do asynchronicznych operacji.

*)