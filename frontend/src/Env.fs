module Env

open Fable.Core.JsInterop
open Browser

let private env name: string = window?("env")?(name)

let apiUrl () =  env "apiUrl"
