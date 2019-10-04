import axios from "axios";
import * as helpr from "./serviceHelper";

const getTop20 = () => {
  const config = {
    method: "GET",
    url: `${helpr.API_HOST_PREFIX}/api/default/SteamTop`,
    withCredentials: true,
    crossdomain: true,
    headers: {
      "content-type": "application/json"
    }
  };

  return axios(config)
    .then(helpr.onGlobalSuccess)
    .catch(helpr.onGlobalError);
};

export { getTop20 };
