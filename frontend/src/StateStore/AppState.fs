module StateStore.AppState

type AppState = { Navigation: Navigation.State
                  Counter: Counter.State
                  Joke: Joke.State }
