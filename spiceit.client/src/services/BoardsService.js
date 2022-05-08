import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class BoardsService {
  async getUserBoards(id) {
    const res = await api.get('api/profiles/' + id + '/boards')
    logger.log("all boards", res.data)
    AppState.boardsUser = res.data
  }

  async setActive(id) {
    const res = await api.get('api/boards/' + id)
    logger.log('setative board', res.data)
    AppState.acitveBoard = res.data
  }
}

export const boardsService = new BoardsService()