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
    AppState.activeBoard = res.data
  }

  async deleteBoard(id) {
    const res = await api.delete("api/boards/" + id)
    logger.log("delete board", res.data)
    AppState.pins.filter(b => b.boardId != id)
    AppState.boardsUser.filter(bu => bu.id != id)
  }

  async createBoard(body) {
    const res = await api.post("api/boards", body)
    logger.log("creating this board", res.data)
    AppState.boardsUser.push(res.data)
  }
}

export const boardsService = new BoardsService()