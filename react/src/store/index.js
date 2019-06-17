import { createStore, applyMiddleware, compose } from "redux";
import rootReducer from "../reducers/rootReducer.js";
import thunk from "redux-thunk";

const initialState = { x: { items: [], item: "some game" } };

const composeEnhancers =
  window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ &&
  window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__();

const middleware = applyMiddleware(thunk);

export const store = createStore(
  rootReducer,
  initialState,
  compose(
    middleware,
    composeEnhancers
  )
);

export default store;
