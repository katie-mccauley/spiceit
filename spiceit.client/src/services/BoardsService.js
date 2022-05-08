import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class BoardsService {
  async getUserBoards(id) {
    const res = await api.get('api/profiles/' + id + '/boards')
    logger.log("all boards", res.data)
    AppState.boards = res.data
  }
}

export const boardsService = new BoardsService()