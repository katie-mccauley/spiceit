import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class PinsService {

  async getPins(id) {
    const res = await api.get('api/boards/' + id + '/recipes')
    logger.log("all of the pins", res.data)
    AppState.pins = res.data
  }
}
export const pinsService = new PinsService()