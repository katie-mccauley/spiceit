import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService {

  async getAllSteps(id) {
    const res = await api.get("api/recipes/" + id + "/steps")
    logger.log("all of the steps", res.data)
    AppState.steps = res.data
  }

  async getOne(id) {
    const res = await api.get("api/steps/" + id)
    logger.log("thos is one step", res.data)
    AppState.activeStep = res.data
  }

  async createStep(body) {
    const res = await api.post("api/steps", body)
    logger.log(res.data)
    AppState.steps.push(res.data)
  }

  async deleteStep(id) {
    const res = await api.delete("api/steps/" + id)
    logger.log("delete step", res.data)
    AppState.steps = AppState.steps.filter(s => s.id != id)
  }

  async editStep(body, id) {
    const res = await api.put('api/steps/' + id, body)
    logger.log('edit step', res.data)
    AppState.steps = AppState.steps.splice(id, 1, res.data)
  }
}

export const stepsService = new StepsService()