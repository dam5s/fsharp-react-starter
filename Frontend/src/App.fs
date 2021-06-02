module Frontend.App

open Feliz
open Browser.Dom
open Fable.Core.JsInterop

open Components.StoreProvider
open Components.Layout
open StateStore.Main

importAll "../assets/app.scss"

[<ReactComponent>]
let AppRoot () = StoreProvider (stateStore()) (Layout())

ReactDOM.render (AppRoot(), document.getElementById "root")
