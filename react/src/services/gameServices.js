import axios from "axios";
import * as helpers from "./serviceHelper";

const getAll = shops => {
  const config = {
    method: "GET",
    url: `${helpers.API_ITAD_PREFIX}v01/game/plain/list/?key=${
      helpers.API_ITAD_KEY
    }&shops=${shops}`
  };

  return axios(config)
    .then(helpers.onGlobalSuccess)
    .catch(helpers.onGlobalError);
};

const getGameById = id => {
  const config = {
    method: "GET",
    url: `${helpers.API_ITAD_PREFIX}/v02/game/plain/?key=${
      helpers.API_ITAD_KEY
    }&game_id=${id}`
  };

  return axios(config)
    .then(helpers.onGlobalSuccess)
    .catch(helpers.onGlobalError);
};
export { getAll, getGameById };
