import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {


  async getAllRecipes() {
    const res = await api.get('api/recipes')
    logger.log("All recipes", res.data)
    AppState.recipes = res.data
  }

  async createRecipe(body) {
    const res = await api.post('api/recipes', body)
    logger.log("this is created dta", res.data)
  }
}

export const recipesService = new RecipesService()