(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Monada stanu
======================

Poświęcimy teraz chwilę na zaimplementowanie trochę bardziej logistycznie skomplikowanej monady, czyli monady stanu. Używamy jej aby propagować stan przez obliczenia.

*)

type State<'TState, 'TResult> = State of ('TState -> 'TResult * 'TState)

(*** hide ***)
type Random = Random of int
let nextRandom (Random s) = let s' = (s * 123456 + 81327) % System.Int32.MaxValue in (s', Random s)
let stateReturn x = State <| fun s -> s,x
let stateRun (State f) s = f s
let stateBind m f = State <| fun s -> let (x, s') = stateRun m s in stateRun (f x) s'
let stateGet = State <| fun s -> s,s
let statePut s = State <| fun _ -> s,()
let stateModify f = stateBind stateGet (fun s -> statePut (f s))
type StateBuilder() =
    member this.Return(x) = stateReturn x
    member this.Bind(m, f) = stateBind m f
let state = StateBuilder()

(**

Nasze obliczenie otrzyma pewien stan, a następnie zwróci rezultat obliczeń i nowy stan. Ale to wszystko będzie ukryte od programisty, który będzie tylko posługiwał się pewnymi prymitywami.

Zadanie
---------

Napisz funkcje:

- `stateReturn : 'a -> State<'t, 'a>`, która nie zmieniając stanu zwraca podaną wartość
- `stateRun    : State<'t, 'a> -> 't -> 'a * 't`, która przyjmuje monadę stanu i pewien stan początkowy i zwraca parę (wynik, nowy stan)
- `stateBind   : State<'t, 'a> -> ('a -> State<'t, 'b>) -> State<'t, 'b>`
- `stateGet    : State<'t, 't>` to jest w zasadzie wartość monadyczna, która w wyniu ma aktualny stan
- `statePut    : 't -> State<'t, unit>`, która ustawia stan na podany
- `stateModify : ('t -> 't) -> State<'t, unit>`, która modyfikuje stan podaną funkcją

A na koniec utwórz środowisko `state { }`, które pozwoli uruchomić poniższy kawałek kodu:

*)

//napisany przez was wcześniej Random
type RandomState<'a> = State<Random, 'a>

let nextRandomM = 
    state {
        let! r = stateGet
        let (x, r') = nextRandom r
        do! statePut r'
        return x
    }

let randomArrayM n = 
    state {
        if n <= 0 then return []
        else
            let! t = randomArrayM (n-1)
            let! r = nextRandomM
            return (r::t)
    }

let runRandom seed =
    let (x, _) = stateRun (randomArrayM 10) seed
    in x

(**

Pytania? Jeśli wszystko jasne, to przechodzimy do [następnego modułu](./io_monad.html)

*)