//import {GAME_TYPES} from './types';
import * as gameServices from "../services/gameServices";
import * as testServices from "../services/testServices";

export const TYPES = {
  GET_GAME: "GET_GAME",
  ADD_GAME: "ADD_GAME"
};

export const getGame = id => dispatch => {
  gameServices
    .getGameById(id)
    .then(item => dispatch({ type: TYPES.GET_GAME, payload: item }))
    .catch(xhr => console.log("Get Game Failed: ", xhr));
};

export const addGame = gameStr => dispatch => {
  testServices
    .search(gameStr)
    .then(data => dispatch({ type: TYPES.ADD_GAME, payload: data.list }))
    .catch(xhr => console.log("Backup Call Failed: ", xhr));
};

export const addList = gameList => dispatch => {
  debugger;
  dispatch({ type: TYPES.ADD_GAME, payload: gameList });
};
//for axios use dispatch, example below

// export function getPeople() {
//     _logger("getPeople dispatched and currying");

//     return dispatch => {
//       _logger("getPeople curried fx executing");
//       dispatch(requestPosts());
//       return svc
//         .getPeople()
//         .then(data => data.item.pagedItems)
//         .then(itemsToPassAlong => {
//           //this .then call is 100% here for us to be able to put this log message in
//           _logger(
//             "getPeople success then firing and passing along items via dispatch to receivePosts"
//           );
//           return itemsToPassAlong;
//         })
//         .then(items => dispatch(receivePosts(items)));
//     };
//   }

// ES 6 Syntax
// export const getGame = data => dispatch => {
//     return {
//       type: TYPES.GET_GAME,
//       payload: data
//     };
//   };
