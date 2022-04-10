import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService {

  async getAllSteps(id) {
    const res = await api.get("api/recipes/" + id + "/steps")
    logger.log("all of the steps", res.data)
    AppState.steps = res.data
  }

  async deleteStep(id) {
    const res = await api.delete("api/steps/" + id)
    logger.log("delete step", res.data)
    AppState.steps = AppState.steps.filter(s => s.id != id)
  }
}

export const stepsService = new StepsService()