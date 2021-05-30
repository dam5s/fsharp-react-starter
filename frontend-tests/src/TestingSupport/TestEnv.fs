module TestingSupport.TestEnv

open Fable.Core.JsInterop
open Browser

let private createEnv () =
    if isNullOrUndefined window?("env")
    then window?("env") <- createObj  []
    else ()

let private setEnv name value =
    createEnv ()
    window?("env")?(name) <- value

let setApiUrl newUrl = setEnv "apiUrl" newUrl
