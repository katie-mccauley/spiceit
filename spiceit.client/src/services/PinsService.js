import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class PinsService {

  async getPins(id) {
    const res = await api.get('api/boards/' + id + '/recipes')
    logger.log("all of the pins", res.data)
    AppState.pins = res.data
  }

  async createPin(pinData) {
    const res = await api.post('api/pins', pinData)
    logger.log("creating a pin", res.data)
    AppState.pins.push(res.data)
  }
}
export const pinsService = new PinsService()