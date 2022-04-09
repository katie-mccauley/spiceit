import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService {

  async getAllSteps(id) {
    const res = await api.get("api/recipes/" + id + "/steps")
    logger.log("all of the steps", res.data)
    AppState.steps = res.data
  }
}

export const stepsService = new StepsService()