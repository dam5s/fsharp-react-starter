module App

open Feliz
open Browser.Dom

open Components.StoreProvider
open Components.Layout
open StateStore.Main

[<ReactComponent>]
let AppRoot () = StoreProvider (stateStore()) (Layout())

ReactDOM.render (AppRoot(), document.getElementById "root")
