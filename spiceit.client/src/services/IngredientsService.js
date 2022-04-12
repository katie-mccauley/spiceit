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

  async getOne(id) {
    const res = await api.get('api/ingredients/' + id)
    logger.log("active ingredeint", res.data)
    AppState.activeIngredient = res.data
  }

  async editIngredient(body, id) {
    const res = await api.put('api/ingredients/' + id, body)
    logger.log("edit ingredient", res.data)
    const ingredient = AppState.activeIngredient.id
    AppState.ingredients = AppState.ingredients.splice(ingredient, 1, res.data)
  }
}

export const ingredientsService = new IngredientsService()