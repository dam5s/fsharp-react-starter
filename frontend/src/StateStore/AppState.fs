module StateStore.AppState

type AppState = { Page: Page.State
                  Counter: Counter.State
                  Joke: Joke.State }
