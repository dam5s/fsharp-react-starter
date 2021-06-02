module Frontend.Components.PageLink

open Browser.Types
open Feliz
open Feliz.Router
open Frontend.StateStore

let private navigate (href: string) (event: MouseEvent) =
    event.preventDefault()
    Router.navigatePath href

[<ReactComponent>]
let PageLink (fromPage: Navigation.Page option, toPage: Navigation.Page) =
    let href = Navigation.href toPage
    let className = if fromPage = Some toPage then "active" else "inactive"

    Html.a [ prop.href href
             prop.text $"%A{toPage}"
             prop.className className
             prop.onClick (navigate href) ]
