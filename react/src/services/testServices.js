import axios from "axios";
import * as helpers from "./serviceHelper";

const search = game => {
  game = game.replace(/\s/g, "%20");
  debugger;
  const config = {
    method: "GET",
    url: `${helpers.API_ITAD_PREFIX}/v01/search/search/?key=${
      helpers.API_ITAD_KEY
    }&q=${game}&limit=20`
  };

  return axios(config)
    .then(helpers.onGlobalSuccess)
    .catch(helpers.onGlobalError);
};

export { search };
