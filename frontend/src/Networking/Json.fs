[<RequireQualifiedAccess>]
module Networking.Json

open Prelude
open Fable.Core
open Fable.Core.JsInterop

type Decoder<'a> =
    JS.Object -> Result<'a, string>

let private typedArrayToList (arr: JS.TypedArray<'a>): 'a list =
    let mutable l = []
    arr.forEach (fun a -> l <- l @ [a]; true)
    l

let rec private decodeList (decoder: 'b -> Result<'a, string>) (list: 'b list): Result<'a list, string> =
    let (<*>) = Result.apply
    let cons x xs = x::xs

    match list with
    | [] -> Ok []
    | x :: xs -> Ok cons <*> (decoder x) <*> (decodeList decoder xs)

let list (decoder: 'b -> Result<'a, string>) (arr: JS.TypedArray<'b>) =
    arr
    |> typedArrayToList
    |> decodeList decoder

let property<'a> (name: string) (obj: JS.Object): Result<'a, string> =
    tryCast<'a> obj?(name)
    |> Option.toResult $"Cannot parse field '%s{name}'"

let objectProperty (name: string) (decoder: Decoder<'a>) (obj: JS.Object): Result<'a, string> =
    tryCast<JS.Object> obj?(name)
    |> Option.bind (fun value -> if isNullOrUndefined value then None else Some value)
    |> Option.toResult $"Field '%s{name}' is not an object"
    |> Result.bind decoder