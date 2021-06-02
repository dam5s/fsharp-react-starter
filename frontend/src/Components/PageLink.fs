module Components.PageLink

open Browser.Types
open Feliz
open Feliz.Router
open StateStore

let private navigate (href: string) (event: MouseEvent) =
    event.preventDefault()
    Router.navigatePath href

[<ReactComponent>]
let PageLink (page: Navigation.Page) =
    let href = Navigation.href page

    Html.a [ prop.href href
             prop.text $"%A{page}"
             prop.onClick (navigate href) ]
