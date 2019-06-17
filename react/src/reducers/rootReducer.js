import { combineReducers } from "redux";
import xReducer from "./xReducer";

export default combineReducers({
  x: xReducer
});
