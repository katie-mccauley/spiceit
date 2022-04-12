import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService {

  async getAll() {
    const res = await api.get('account/favorites')
    logger.log(res.data)
    AppState.recipes = res.data
  }
  async createFav(fav) {
    const res = await api.post('api/favorites', fav)
    logger.log("fav", res.data)
    AppState.favorites = res.data
  }

}

export const favoritesService = new FavoritesService()