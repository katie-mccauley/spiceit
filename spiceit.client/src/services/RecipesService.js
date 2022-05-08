import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {


  async getAllRecipes(query = "") {
    const res = await api.get('api/recipes' + query)
    logger.log("All recipes", res.data)
    AppState.recipes = res.data
  }

  async getMine() {
    const res = await api.get('account/recipes')
    logger.log("All mine recipes", res.data)
    AppState.recipes = res.data
  }
  async createRecipe(body) {
    const res = await api.post('api/recipes', body)
    logger.log("this is created dta", res.data)
    AppState.recipes.push(res.data);
  }

  async deleteRecipe(id) {
    const res = await api.delete('api/recipes/' + id)
    logger.log("this is deleting recipe", res.data)
    AppState.recipes = AppState.recipes.filter(r => r.id != id)
  }

  async activeRecipe(id) {
    const res = await api.get('api/recipes/' + id)
    logger.log("get recipe", res.data)
    AppState.activeRecipe = res.data
  }


  async getFavs() {
    const res = await api.get('account/favorites')
    logger.log("this your favs", res.data)
    AppState.favs = res.data
  }

  async getUserRecipes(id) {
    const res = await api.get('api/profiles/' + id + '/recipes')
    logger.log('all of the recipes', res.data)
    AppState.recipes = res.data
  }


}

export const recipesService = new RecipesService()