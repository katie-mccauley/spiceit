import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class IngredientsService {


  async getAllIngredients(id) {
    const res = await api.get('api/recipes/' + id + '/ingredients')
    logger.log("this is all ingredinets", res.data)
    AppState.ingredients = res.data
    logger.log("testing", AppState.ingredients)
  }

  async createIngredient(body) {
    const res = await api.post('api/ingredients', body)
    logger.log("created ingredients", res.data)
    AppState.ingredients.push(res.data)
  }

  async deleteIngredient(id) {
    const res = await api.delete('api/ingredients/' + id)
    logger.log("delete ingredient", res.data)
    AppState.ingredients = AppState.ingredients.filter(i => i.id != id)
  }
}

export const ingredientsService = new IngredientsService()