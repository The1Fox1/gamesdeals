//import { GAME_TYPES } from "../actions/types";
import { TYPES } from "../actions/xActions";

const initialState = {
  items: [],
  item: {}
};

export default function(state = initialState, action) {
  switch (action.type) {
    case TYPES.ADD_GAME:
      return {
        ...state,
        items: action.payload
      };
    default:
      return state;
  }
}
