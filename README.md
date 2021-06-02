# F# React Starter App

This small application is here to demonstrate how I would start a React frontend
application for a customer using [F#](https://fsharp.org) + [Fable](https://fable.io) + [Feliz](https://zaid-ajaj.github.io/Feliz/).

## Things that I look for in a codebase

 * Can be Test Driven (encourages me to write code that is simple to test)
 * Easy to expand without increasing complexity
 * Easy to change
 * Confidence I'm not breaking things

## Techs used

 * [.NET 5](https://dotnet.microsoft.com/download)
 * [F# 5](https://fsharp.org)
 * [Fable](https://fable.io)
 * [Feliz](https://zaid-ajaj.github.io/Feliz/)
 * [Jester](https://shmew.github.io/Fable.Jester/)

## Redux/Elm style Architecture

I really really like Elm and Elmish, but there are a couple things 
that make it a bit more tedious to use on large projects.
When the project grows big, it requires a fair amount of work to "componentize" the codebase.

For instance, if I wanted to have an app made of 10 different pages with their own state plus some shared state,
it requires a lot of wiring by passing down functions to components for wrapping/unwrapping messages...

One things that's nice in Redux, it doesn't enforce the action type that is dispatched. While this removes some type safety,
it also adds a lot of flexibility. In particular, different reducers can have their own Action type and not worry about
wrapping them into an App-Level Action type before dispatching.

Another thing inherent to React is that it's easy to have components that don't care about Redux,
those components can have children that do care about it but they don't have to pass things down.

### Important Redux related files

 * [frontend/src/Prelude/Redux.fs](frontend/src/Prelude/Redux.fs) for the generic `StateStore` implementation
 * [frontend/src/StateStore/Main.fs](frontend/src/StateStore/Main.fs) for the top-level declaration of the application's `stateStore`
 * [frontend/src/Components/StoreProvider.fs](frontend/src/Components/StoreProvider.fs) for the React provider of the application `stateStore`, it depends on the above for extra type-safety.
 * [frontend/src/App.fs](frontend/src/App.fs) where it gets initially configured
 * [frontend/src/Components/JokePage.fs](frontend/src/Components/JokePage.fs) where it's used

## Networking and testing

I believe there is no need to stub the networking layer when testing a webapp. Even for testing.
It is so easy these days to quickly spin up a local HTTP server that I would rather do that instead.

The advantage of doing that is that you can choose whatever implementation you want, the testing remains
the same. It makes refactoring of your networking layer a lot easier!

For the implementation I simply use Javascript's `fetch` function,
during tests I import `whatwg-fetch` as a polyfill for the NodeJS runtime.

### Important Networking related files

 * [frontend/src/Networking/Http.fs](frontend/src/Networking/Http.fs) for the base implementation of sending a request for JSON or not.
 * [frontend-tests/src/TestingSupport/MockWebServer.fs](frontend-tests/src/TestingSupport/MockWebServer.fs) for the test version of an HTTP server.
 * [frontend-tests/src/Components/JokePage.test.fs](frontend-tests/src/Components/JokePage.test.fs) for example tests.

## Building/Deploying to multiple environments

Having worked extensively on backends, I am used to being able to build my application once on CI then deploying
it to different environments by just changing configuration through Environment Variables and/or a configuration server.

External source: [The Twelve-Factor App](https://12factor.net/config)

I find it extremely important that the CI system builds one artifact, runs tests against that artifact
and that same artifact is what is deployed everywhere. In the case of javascript applications that run in the browser,
I want to run my highest level tests against the result of my webpack build.

In this case I made the webpack build produce two separate artifacts, the application and the environment. This way,
when I want to deploy to a different environment, the environment artifact is the only one I need to change.

### Important Environment related files

 * [frontend/webpack.config.js](frontend/webpack.config.js) defines the two separate entries.
 * [frontend/src/env.js](frontend/src/env.js) defines my default environment config.
 * [frontend/src/Env.fs](frontend/src/Env.fs) provides access to the environment.
 * [frontend-tests/src/TestingSupport/TestEnv.fs](frontend-tests/src/TestingSupport/TestEnv.fs) to configure the environment during tests.

## TODO

 * CSS pipeline, maybe typesafe CSS from Feliz?  
 * Backend + Backend rendering?
 * End-to-end testing with Cypress
 * Fake build
 * Use paket for easier dependency version management?
